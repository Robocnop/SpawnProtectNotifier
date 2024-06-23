using Exiled.API.Interfaces;
using System.ComponentModel;
using UnityEngine;

namespace SpawnProtectNotifier
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float HintDuration { get; set; } = 10;

        public string HintMessage { get; set; } = "<color=yellow>La protection de spawn pour <color=red>%playername%</color> n'est plus active.</color>";

        [Description("The duration of the spawn protect")]
        public float SpawnProtectDuration { get; set; } = 10;
    }
}