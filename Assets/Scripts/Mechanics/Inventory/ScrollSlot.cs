using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics.Inventory;

namespace Mechanics.Inventory
{
    public class ScrollSlot : MonoBehaviour
    {
        [SerializeField]
        private Inventory Inventory;
        void Update()
        {
            if(Input.GetAxis("Mouse ScrollWheel") != 0 && Inventory.inventory.Count > 0)
            {
                Inventory.CurrentSlot += (int)(Input.GetAxis("Mouse ScrollWheel")*10);
                if (Inventory.CurrentSlot > Inventory.inventory.Count - 1)Inventory.CurrentSlot = 0;
                else if (Inventory.CurrentSlot < 0) Inventory.CurrentSlot = Inventory.inventory.Count - 1;
            }
        }
    }
}
