﻿using System;
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
        private static void KillPlayer(GameObject player)
        {
            GameObject.Destroy(player);
        }

        public static Action<GameObject> GetSpikeAction()
        {
            switch(GameController.instance.spikeAction)
            {
                case SpikeAction.KillPlayer:
                    return KillPlayer;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
