using UnityEngine;

namespace Mechanics.Interactable
{
    // States of Satellite
    enum stateofSatellite
    {
        None,
        Broken
    }
    
    

    // Main Class
    public class Satellite : BaseInteractable
    {
        [Header("Satellite Settings")]

        [SerializeField]
        [Tooltip("Состояние спутника!")]
        private stateofSatellite state = stateofSatellite.Broken;
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
            TimeToUse = 10f;
        }

        public void Repair()
        {
            if (!IsBroken) Debug.Log("No Broken, no needed in repair!");
            else
            {
                state = stateofSatellite.None;
                Debug.Log("Repaired!");
            }
        }

        public override void Interact(GameObject player)
        {
            base.Interact(player);
            Repair();
        }
    }
}
