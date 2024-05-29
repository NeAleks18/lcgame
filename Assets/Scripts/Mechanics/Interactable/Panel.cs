using Mirror;
using UnityEngine;
using Mechanics.Inventory;
using static UnityEditor.Timeline.Actions.MenuPriority;
using System;
using Unity.VisualScripting;

namespace Mechanics.Interactable
{
    
    
    public class Panel : BaseInteractable
    {

        [SerializeField]
        private Item RequiestItem;

        [SyncVar/*(hook = nameof(ChangeState))*/, SerializeField]
        private stateofSatellite state = stateofSatellite.Broken;



        [SerializeField]
        private TowerManager towerManager;


        private bool IsBroken
        {
            get
            {
                if (state == stateofSatellite.Broken) return true;
                else return false;
            }
        }

        private void Awake()
        {
            TimeToUse = 4f;
        }

        private static void ChangeState()
        {
            
        }

        public bool Repair()
        {
            if (!IsBroken) {
                Debug.Log("No Broken, no needed in repair!");
                return false;
            }
            else
            {
                state = stateofSatellite.None;
                towerManager.IsPanelRepaired = true;
                Debug.Log("Repaired!");
                return true;
            }
        }
        
        public override void Interact(GameObject player)
        {
            base.Interact(player);
            if (player.GetComponent<Inventory.Inventory>().CheckItem(RequiestItem)) {
                if (Repair())
                {
                    player.GetComponent<Inventory.Inventory>().DeleteItem(RequiestItem);
                }
            }
        }
    }
}
