using System;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace AdminAssist;
public class Plugin : Plugin<Config>
{
    public override string Author => "Jiraya";
    public override string Name => "AdminAssist";
    public override string Prefix => "AdminAssist";

    public override Version Version => new(1, 2, 1);
    public override Version RequiredExiledVersion => new(9, 0, 0);
    public override PluginPriority Priority => PluginPriority.Default;

    public static Plugin Instance { get; private set; }

    public override void OnEnabled()
    {
        Instance = this;
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Instance = null;
        base.OnDisabled();
    }
}