using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Collections.Generic;

namespace SpawnProtectNotifier
{
    public class EventHandlers
    {
        public Config config;
        private readonly Dictionary<Player, CoroutineHandle> spawnProtectedPlayers = new Dictionary<Player, CoroutineHandle>();

        public EventHandlers(Config config)
        {
            this.config = config;
        }

        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.Role.Type == RoleTypeId.Spectator || ev.Player.Role.Type == RoleTypeId.None || ev.Player.Role.Type == RoleTypeId.Overwatch || ev.Player.Role.Type == RoleTypeId.Filmmaker)
                return;

            ActivateSpawnProtection(ev.Player);
        }

        public void OnShooting(ShootingEventArgs ev)
        {
            if (spawnProtectedPlayers.ContainsKey(ev.Player))
            {
                RemoveSpawnProtection(ev.Player, "player fired a weapon");
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Reason == SpawnReason.ForceClass && spawnProtectedPlayers.ContainsKey(ev.Player))
            {
                RemoveSpawnProtection(ev.Player, "player changed class");

                if (config.Debug)
                    Log.Debug($"{ev.Player.Nickname} changed class. Resetting spawn protection timer.");

                ActivateSpawnProtection(ev.Player);
            }
        }

        private void ActivateSpawnProtection(Player player)
        {
            if (config.Debug)
                Log.Debug($"{player.Nickname} has spawned. Activating spawn protection for {config.SpawnProtectDuration} seconds.");

            var coroutine = Timing.CallDelayed(config.SpawnProtectDuration, () =>
            {
                if (spawnProtectedPlayers.ContainsKey(player))
                {
                    RemoveSpawnProtection(player, "spawn protection duration ended");
                }
            });

            if (spawnProtectedPlayers.ContainsKey(player))
            {
                Timing.KillCoroutines(spawnProtectedPlayers[player]);
                spawnProtectedPlayers.Remove(player);
            }

            spawnProtectedPlayers[player] = coroutine;
        }

        private void RemoveSpawnProtection(Player player, string reason)
        {
            if (spawnProtectedPlayers.ContainsKey(player))
            {
                Timing.KillCoroutines(spawnProtectedPlayers[player]);
                spawnProtectedPlayers.Remove(player);

                player.ShowHint(string.Format(config.HintMessage, player.Nickname, reason), config.HintDuration);

                if (config.Debug)
                    Log.Debug($"{player.Nickname}'s spawn protection was removed because {reason}.");
            }
        }
    }
}
