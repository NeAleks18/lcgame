using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics.Inventory;

namespace Mechanics.Inventory
{
    public class ScrollSlot : MonoBehaviour
    {

        void Update()
        {
            if(Input.GetAxis("Mouse ScrollWheel") != 0 && Inventory.Instance.inventory.Count > 0) Inventory.Instance.CurrentSlot += (int)(Input.GetAxis("Mouse ScrollWheel")*10);
            if (Inventory.Instance.CurrentSlot > Inventory.Instance.inventory.Count - 1 && Inventory.Instance.inventory.Count > 0) Inventory.Instance.CurrentSlot = 0;
            else if (Inventory.Instance.CurrentSlot < 0 && Inventory.Instance.inventory.Count > 0) Inventory.Instance.CurrentSlot = Inventory.Instance.inventory.Count - 1;
        }
    }
}
