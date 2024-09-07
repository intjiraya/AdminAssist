using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;

namespace AdminAssist.Commands
{
    public class Call : ICommand
    {
        public string Command { get; } = Plugin.Instance.Config.CommandAliases[0];
        // Get all aliases except the primary command
        public string[] Aliases { get; } = Plugin.Instance.Config.CommandAliases.Length > 1 ? Plugin.Instance.Config.CommandAliases.Skip(1).ToArray() : new string[] { };
        public string Description { get; } = "Call the admin.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);
            if (player == null)
            {
                response = "You need to be a player to call an admin.";
                return false;
            }

            response = "Admin not called";
            return false;
        }
    }
}