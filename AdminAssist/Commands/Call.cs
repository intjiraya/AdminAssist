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
        public string[] Aliases { get; } = Plugin.Instance.Config.CommandAliases.Count > 1
            ? Plugin.Instance.Config.CommandAliases.Skip(1).ToArray()
            : new string[] { };
        public string Description { get; } = "Call the admin for assistance.";

        private static readonly Dictionary<string, DateTime> LastCommandUsage = new Dictionary<string, DateTime>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var callingPlayer = Player.Get(sender);
            if (callingPlayer == null)
            {
                response = "You need to be a player to call an admin.";
                return false;
            }

            string playerId = callingPlayer.UserId;
            float cooldown = Plugin.Instance.Config.Cooldown;

            if (cooldown > 0)
            {
                DateTime currentTime = DateTime.Now;

                if (LastCommandUsage.TryGetValue(playerId, out DateTime lastUsageTime))
                {
                    TimeSpan elapsedTime = currentTime - lastUsageTime;

                    if (elapsedTime.TotalSeconds < cooldown)
                    {
                        double timeLeft = cooldown - elapsedTime.TotalSeconds;
                        response = $"You must wait {timeLeft:F1} seconds before using this command again.";
                        return false;
                    }
                }

                LastCommandUsage[playerId] = currentTime;
            }

            string argumentsStr = string.Join(" ", arguments).Replace("<", "[").Replace(">", "]");

            HashSet<Player> receivers = Player.List
                .Where(p => Plugin.Instance.Config.Permissions.Any(permission => p.CheckPermission(permission)))
                .ToHashSet();

            if (Plugin.Instance.Config.ToAllRaAuthorized)
            {
                receivers.UnionWith(Player.List.Where(p => p.RemoteAdminAccess));
            }

            if (!string.IsNullOrEmpty(Plugin.Instance.Config.AdminsBroadcast))
            {
                string broadcastMessage = Plugin.Instance.Config.AdminsBroadcast
                    .Replace("%nickname%", callingPlayer.Nickname)
                    .Replace("%steamid%", callingPlayer.UserId)
                    .Replace("%id%", callingPlayer.Id.ToString())
                    .Replace("%value%", argumentsStr);

                foreach (var receiver in receivers)
                {
                    receiver.Broadcast(Plugin.Instance.Config.AdminsBroadcastDuration, broadcastMessage);
                }
            }

            if (!string.IsNullOrEmpty(Plugin.Instance.Config.ConsoleLog))
            {
                string consoleMessage = Plugin.Instance.Config.ConsoleLog
                    .Replace("%nickname%", callingPlayer.Nickname)
                    .Replace("%steamid%", callingPlayer.UserId)
                    .Replace("%id%", callingPlayer.Id.ToString())
                    .Replace("%value%", argumentsStr);

                foreach (var receiver in receivers)
                {
                    receiver.SendConsoleMessage(consoleMessage, "Silver");
                }
            }

            response = "Admins have been notified!";

            return true;
        }
    }
}