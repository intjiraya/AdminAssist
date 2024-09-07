using System;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace AdminAssist
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Jiraya";
        public override string Name { get; } = "AdminAssist";
        public override string Prefix { get; } = "AdminAssist";

        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 9, 11);
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
        }
    }
}