using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace AdminAssist.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Call : ICommand
    {
        public string Command { get; } = Plugin.Instance.Config.CommandAliases.First();
        public string[] Aliases { get; } = Plugin.Instance.Config.CommandAliases.Skip(1).ToArray();
        public string Description => "Call the admin for assistance.";

        private static readonly Dictionary<string, DateTime> LastCommandUsage = new();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var callingPlayer = Player.Get(sender);
            if (callingPlayer == null)
            {
                response = "You need to be a player to call an admin.";
                return false;
            }

            if (!CanUseCommand(callingPlayer.UserId, Plugin.Instance.Config.Cooldown, out response))
                return false;

            var argumentsStr = FormatArguments(arguments);
            var receivers = GetReceivers();

            NotifyAdmins(callingPlayer, argumentsStr, receivers);
            response = "Admins have been notified!";
            return true;
        }

        private bool CanUseCommand(string playerId, float cooldown, out string response)
        {
            if (cooldown <= 0)
            {
                response = null;
                return true;
            }

            var currentTime = DateTime.Now;

            if (LastCommandUsage.TryGetValue(playerId, out var lastUsageTime))
            {
                var elapsedTime = currentTime - lastUsageTime;

                if (elapsedTime.TotalSeconds < cooldown)
                {
                    var timeLeft = cooldown - elapsedTime.TotalSeconds;
                    response = $"You must wait {timeLeft:F1} seconds before using this command again.";
                    return false;
                }
            }

            LastCommandUsage[playerId] = currentTime;
            response = null;
            return true;
        }

        private string FormatArguments(ArraySegment<string> arguments)
        {
            return string.Join(" ", arguments).Replace("<", "[").Replace(">", "]");
        }

        private HashSet<Player> GetReceivers()
        {
            var receivers = Player.List
                .Where(p => Plugin.Instance.Config.Permissions.Any(permission => p.CheckPermission(permission)))
                .ToHashSet();

            if (Plugin.Instance.Config.ToAllRaAuthorized)
            {
                receivers = receivers.Union(Player.List.Where(p => p.RemoteAdminAccess)).ToHashSet();
            }

            return receivers;
        }

        private void NotifyAdmins(Player callingPlayer, string argumentsStr, HashSet<Player> receivers)
        {
            var broadcastMessage = FormatMessage(Plugin.Instance.Config.AdminsBroadcast, callingPlayer, argumentsStr);
            var consoleMessage = FormatMessage(Plugin.Instance.Config.ConsoleLog, callingPlayer, argumentsStr);

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

        private string FormatMessage(string messageTemplate, Player callingPlayer, string argumentsStr)
        {
            return string.IsNullOrEmpty(messageTemplate) ? null : messageTemplate
                .Replace("%nickname%", callingPlayer.Nickname)
                .Replace("%steamid%", callingPlayer.UserId)
                .Replace("%id%", callingPlayer.Id.ToString())
                .Replace("%value%", argumentsStr);
        }
    }
}
