using System;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace AdminAssist
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "Jiraya";
        public override string Name => "AdminAssist";
        public override string Prefix => "AdminAssist";

        public override Version Version => new(1, 0, 0);
        public override Version RequiredExiledVersion => new(8, 13, 2);
        public override PluginPriority Priority => PluginPriority.Default;

        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
        }
    }
}