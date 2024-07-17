using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using CustomPlayerEffects;
using PlayerRoles;
using System.ComponentModel;
using Exiled.API.Extensions;
using System.Collections.Generic;

namespace SpawnProtectNotifier
{
    public class EventHandlers
    {
        public Config config;        

        public EventHandlers(Config config)
        {
            this.config = config;
        }      
		
		public void OnEffectAdded(ReceivingEffectEventArgs ev)
		{
			if (!ev.Effect.TryGetEffectType(out EffectType effect))
                Log.Error($"EffectType not found please report to : {statusEffectBase}");            
			if (effect.Type == EffectType.SpawnProtected)
			{
				if (config.Debug)
					Log.Debug($"{player.Nickname} has spawned. Activating spawn protection for {ev.Effect.Duration} seconds.");
				Timing.CallDelayed(ev.Effect.Duration, () => {
						ev.Player.ShowHint(string.Format(config.HintMessage, ev.Player.Nickname), config.HintDuration);
				});							
			}
		}			
    }
}
