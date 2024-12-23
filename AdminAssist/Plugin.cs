using System;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;

namespace AdminAssist;
public class Plugin : Plugin<Config>
{
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