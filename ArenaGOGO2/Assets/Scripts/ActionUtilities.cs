using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum SpikeAction
    {
        KillPlayer,
    }
    class ActionUtilities
    {
        private static void KillPlayer(PlayerDeath player)
        {
            player.PlayerDie();
        }

        public static Action<PlayerDeath> GetSpikeAction()
        {
            switch(GameController.instance.spikeAction)
            {
                case SpikeAction.KillPlayer:
                    return KillPlayer;
                default:
                    throw new NotImplementedException();
            }
        }

		public static Action<PlayerDeath> GetBombAction()
		{
			switch (GameController.instance.spikeAction)
			{
				case SpikeAction.KillPlayer:
					return KillPlayer;
				default:
					throw new NotImplementedException();
			}
		}

        public static Action<PlayerDeath> GetDeadlyPlatformAction()
        {
            switch (GameController.instance.spikeAction)
            {
                case SpikeAction.KillPlayer:
                    return KillPlayer;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
