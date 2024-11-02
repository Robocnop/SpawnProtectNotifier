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
        public override Version Version { get; } = new Version(1, 1, 3);
        public override Version RequiredExiledVersion => new Version(8, 13, 1);

        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(Config);
			Player.ReceivingEffect += eventHandlers.OnEffectAdded;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {            
			Player.ReceivingEffect -= eventHandlers.OnEffectAdded;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}
