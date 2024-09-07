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

        [Description("Enables or disables debug mode for the plugin.")]
        public bool Debug { get; set; } = false;

        [Description("List of command aliases that can trigger the admin assist functionality.\nAlways put the original name of command first.")]
        public string[] CommandAliases { get; set; } = new string[] { "call", "admin", };

        [Range(0, float.MaxValue)]
        [Description("Cooldown time (in seconds) for the command to prevent spam.")]
        public float Cooldown { get; set; } = 0f;

        [Description("List of permissions that will receive requests from players.")]
        public List<string> Permissions { get; set; } = new List<string> { "PlayersManagement", };

        [Description("Should all players with RA (Remote Admin) permissions receive requests?")]
        public bool ToAllRaAuthorized { get; set; } = true;

        [Description("Specifies whether a prefix is required for commands.")]
        public bool IsPrefixRequired { get; set; } = false;

        [Description("Enables the prefix system for commands.")]
        public bool IsPrefixesEnabled { get; set; } = true;

        [Description("Dictionary of available prefixes for reporting commands.")]
        public IReadOnlyDictionary<string, string> AvailablePrefixes { get; set; } = new Dictionary<string, string>
        {
            { "!", "report" },
            { "?", "question" },
            { "*", "request" },
        };

        [Description("Plugin language. Available languages: en, ru, pl.")]
        public string Language { get; set; } = "en";

        [Description("Available keywords: %name% - Sender's Steam nickname, %value% - Arguments, %id% - Sender's player ID, %type% - Type based on the prefix (empty if no prefix and prefixes are not required).")]
        public string ConsoleLog { get; set; } = "%name% => %value%";

        [Description("Broadcast message sent to admins when a player calls for assistance.")]
        public string AdminsBroadcast { get; set; } = "<color=yellow><b>(%id%) %name% called the admins</b></color>";
    }
}
