using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AdminAssist
{
    public class Config : IConfig
    {
        [Description("Enables or disables the plugin.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enables or disables debug mode for the plugin. This will log additional information for troubleshooting.")]
        public bool Debug { get; set; } = false;

        [Description("A set of command aliases that can trigger the admin assist functionality.\n" +
                     "Always put the original name of the command first!\n")]
        public HashSet<string> CommandAliases { get; set; } = new HashSet<string> { "call", "admin" };

        [Range(0, float.MaxValue)]
        [Description("Cooldown time (in seconds) for the command to prevent spam. Set to 0 to disable cooldown.")]
        public float Cooldown { get; set; } = 0f;

        [Description("A set of permissions that will receive requests from players.\n" +
                     "These permissions should correspond to RA (Remote Admin) permission levels.")]
        public HashSet<string> Permissions { get; set; } = new HashSet<string> { "CallNotify", "PlayerSensitiveDataAccess" };

        [Description("If true, all players with RA (Remote Admin) permissions will receive requests.")]
        public bool ToAllRaAuthorized { get; set; } = true;

        [Description("Template for logging information to the receiver's console.\n" +
                     "Available keywords: %steamid% - Sender's Steam ID, %nickname% - Sender's Steam nickname, %value% - Arguments, %id% - Sender's player ID, %type% - Prefix type (empty if no prefix is used and not required).\n" +
                     "Set to an empty string to disable.")]
        public string ConsoleLog { get; set; } = "%nickname% => %value%";

        [Description("The message broadcasted to admins when a player calls for assistance.\n" +
                     "Set to an empty string to disable")]
        public string AdminsBroadcast { get; set; } = "<color=#FFA500>(%id%) %nickname% called the admins</color>";

        [Description("The duration (in seconds) of the broadcast message. Set to 0 to disable")]
        public ushort AdminsBroadcastDuration { get; set; } = 5;
    }
}
