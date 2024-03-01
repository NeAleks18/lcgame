using UnityEngine;
using Mirror;

namespace Mechanics
{
    // States of Satellite
    enum stateofSatellite
    {
        None,
        Broken
    }

    // Main Class
    public class Satellite : NetworkBehaviour
    {
        [Header("Satellite Settings")]

        [SerializeField]
        [Tooltip("��������� ��������!")]
        private stateofSatellite state = stateofSatellite.Broken;
        private bool IsBroken
        {
            get
            {
                if (state == stateofSatellite.Broken) return true;
                else return false;
            }
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
    }
}
