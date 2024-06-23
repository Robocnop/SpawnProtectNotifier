using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using MEC;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace SpawnProtectNotifier
{
    public class EventHandlers
    {
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.Role.Type != RoleTypeId.Spectator && ev.Player.Role.Type != RoleTypeId.None)
            {
                // Délai avant que la protection de spawn soit désactivée
                Timing.CallDelayed(10f, () =>
                {
                    if (!ev.Player.IsCuffed && ev.Player.Role.Type != RoleTypeId.Spectator && ev.Player.Role.Type != RoleTypeId.None)
                    {
                        Map.Broadcast(10, $"La protection de spawn pour {ev.Player.Nickname} n'est plus active.");
                    }
                });
            }
        }
    }
}
