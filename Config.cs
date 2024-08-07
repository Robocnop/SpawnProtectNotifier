﻿using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SpawnProtectNotifier
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("The duration for which the hint message is displayed.")]
        public float HintDuration { get; set; } = 10;

        [Description("Message displayed when spawn protection ends. Use {0} for player name.")]
        public string HintMessage { get; set; } = "<color=yellow>Spawn protection for <color=red>{0}</color> is no longer active.</color>";        
    }
}
