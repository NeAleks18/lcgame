using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;


namespace Mechanics.Inventory
{
    public class ScrollSlot : NetworkBehaviour
    {
        [SerializeField]
        Inventory inventory;
        [SerializeField]
        ItemDrop drop;

        int slot;
        public event Action OnSlotScrolled;

        void Update()
        {
            if (!isLocalPlayer) return;
            if (slot != inventory.CurrentSlot)
            {
                slot = inventory.CurrentSlot;
                OnSlotScrolled.Invoke();
            }
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && inventory.inventory.Count > 0)
            {
                if (inventory.GetItem(inventory.CurrentSlot)._size == ItemSize.Big) drop.DropItem();
                inventory.CurrentSlot += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
                if (inventory.CurrentSlot > inventory.inventory.Count - 1 && inventory.inventory.Count > 0)  inventory.CurrentSlot = 0;
                else if (inventory.CurrentSlot < 0 && inventory.inventory.Count > 0)  inventory.CurrentSlot = inventory.inventory.Count - 1;
                OnSlotScrolled.Invoke();
            }
            if (inventory.CurrentSlot > inventory.inventory.Count - 1 && inventory.inventory.Count > 0) { inventory.CurrentSlot = 0; OnSlotScrolled.Invoke(); }
            else if (inventory.CurrentSlot < 0 && inventory.inventory.Count > 0) {inventory.CurrentSlot = inventory.inventory.Count - 1; OnSlotScrolled.Invoke(); }
        }
    }
}
