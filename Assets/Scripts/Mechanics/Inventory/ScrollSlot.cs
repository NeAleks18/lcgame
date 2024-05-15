using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace Mechanics.Inventory
{
    public class ScrollSlot : NetworkBehaviour
    {
        [SerializeField]
        Inventory inventory;
        [SerializeField]
        ItemDrop drop;
        void Update()
        {
            if (!isLocalPlayer) return;
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && inventory.inventory.Count > 0)
            {
                if (inventory.getItem(inventory.CurrentSlot)._size == ItemSize.Big) drop.DropItem();
                inventory.CurrentSlot += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
            }
            if (inventory.CurrentSlot > inventory.inventory.Count - 1 && inventory.inventory.Count > 0) inventory.CurrentSlot = 0;
            else if (inventory.CurrentSlot < 0 && inventory.inventory.Count > 0) inventory.CurrentSlot = inventory.inventory.Count - 1;
        }
    }
}
