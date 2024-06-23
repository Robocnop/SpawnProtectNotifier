using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;

namespace SpawnProtectNotifier
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SpawnProtectNotifier";
        public override string Prefix { get; } = "spawn_protect_notifier";
        public override string Author { get; } = "Robocnop";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 4, 3);

        public static Plugin Instance { get; private set; }

        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();

            Player.Spawning += eventHandlers.OnSpawning;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Spawning -= eventHandlers.OnSpawning;

            eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
