using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;

namespace SpawnProtectNotifier
{
    public class EventHandlers
    {
        public Config config;

        public EventHandlers(Config config)
        {
            this.config = config;
        }

        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.Role.Type != RoleTypeId.Spectator && ev.Player.Role.Type != RoleTypeId.None && ev.Player.Role.Type != RoleTypeId.Overwatch && ev.Player.Role.Type != RoleTypeId.Filmmaker)
            {
                Timing.CallDelayed(config.SpawnProtectDuration, () =>
                {
                    ev.Player.ShowHint(config.HintMessage.Replace("%playername%", ev.Player.Nickname), config.HintDuration);
                });
            }
        }
    }
}
