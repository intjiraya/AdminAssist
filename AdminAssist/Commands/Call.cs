using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace AdminAssist.Commands;
[CommandHandler(typeof(ClientCommandHandler))]
public class Call : ICommand
{
    public string Command { get; } = Plugin.Instance.Config.CommandAliases.First();
    public string[] Aliases { get; } = Plugin.Instance.Config.CommandAliases.Skip(1).ToArray();
    public string Description => "Call the admin for assistance.";

    private const string LastCallCommandUsage = "LastCallCommandUsage";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get(sender);

        if (!CanUseCommand(player, out response)) return false;

        string argumentsStr = FormatArguments(arguments);
        HashSet<Player> receivers = GetReceivers();

        NotifyAdmins(player, argumentsStr, receivers);
        response = "Admins have been notified!";
        return true;
    }

    private bool CanUseCommand(Player player, out string response)
    {
        response = string.Empty;

        if (player == null)
        {
            response = "You need to be a player to call an admin.";
            return false;
        }

        if (!player.TryGetSessionVariable(LastCallCommandUsage, out DateTime lastUsageTime)) return true;

        TimeSpan elapsedTime = DateTime.UtcNow - lastUsageTime;

        if (elapsedTime.TotalSeconds >= Plugin.Instance.Config.Cooldown) return true;

        double timeLeft = Plugin.Instance.Config.Cooldown - elapsedTime.TotalSeconds;
        response = $"You must wait {timeLeft:F1} seconds before using this command again.";
        return false;
    }

    private string FormatArguments(ArraySegment<string> arguments)
    {
        return string.Join(" ", arguments).Replace("<", "[").Replace(">", "]");
    }

    private HashSet<Player> GetReceivers()
    {
        HashSet<Player> receivers = Player.List
            .Where(p => Plugin.Instance.Config.Permissions.Any(permission => p.CheckPermission(permission)))
            .ToHashSet();

        if (Plugin.Instance.Config.ToAllRaAuthorized)
        {
            receivers = receivers.Union(Player.List.Where(p => p.RemoteAdminAccess)).ToHashSet();
        }

        return receivers;
    }

    private void NotifyAdmins(Player player, string argumentsStr, HashSet<Player> receivers)
    {
        player.SessionVariables[LastCallCommandUsage] = DateTime.UtcNow;

        string broadcastMessage = FormatMessage(Plugin.Instance.Config.AdminsBroadcast, player, argumentsStr);
        string consoleMessage = FormatMessage(Plugin.Instance.Config.ConsoleLog, player, argumentsStr);

        foreach (var receiver in receivers)
        {
            if (!string.IsNullOrEmpty(broadcastMessage))
            {
                receiver.Broadcast(Plugin.Instance.Config.AdminsBroadcastDuration, broadcastMessage);
            }

            if (!string.IsNullOrEmpty(consoleMessage))
            {
                receiver.SendConsoleMessage(consoleMessage, "Silver");
            }
        }
    }

    private string FormatMessage(string messageTemplate, Player player, string argumentsStr)
    {
        return string.IsNullOrEmpty(messageTemplate) ? null : messageTemplate
            .Replace("%nickname%", player.Nickname)
            .Replace("%steamid%", player.UserId)
            .Replace("%id%", player.Id.ToString())
            .Replace("%value%", argumentsStr);
    }
}