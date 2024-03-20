using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mechanics.Inventory
{
    public class ScrollSlot : MonoBehaviour
    {
        [SerializeField]
        private Inventory Inventory;
        void Update()
        {
            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                Inventory.CurrentSlot += (short)(Input.GetAxis("Mouse ScrollWheel")*10);
                switch (Inventory.CurrentSlot)
                {
                case > Inventory.slots-1:
                        Inventory.CurrentSlot = 0;
                        break;
                case < 0:
                        Inventory.CurrentSlot = Inventory.slots - 1;
                        break;

                }


            }
        }
    }
}