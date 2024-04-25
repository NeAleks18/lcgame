using UnityEngine;
using System.Collections.Generic;
using Mirror;
using System;
using System.Linq;

namespace Mechanics.Inventory
{
    public class Inventory : NetworkBehaviour
    {
        [SerializeField]
        Item[] items;

        [SyncVar(hook = nameof(addItem))]
        public int AddedItemID;
        
        [SerializeField]
        private List<ItemCell> resources = new List<ItemCell>();

        [SerializeField]
        public List<Item> inventory = new List<Item>();

        [SerializeField]
        public int CurrentSlot;

        private void Start()
        {
            items = Resources.LoadAll<Item>("");
        }
        public void addItem(int oldID , int newID)
        {
            Item obj;

            if (newID != 2147483646) { obj = items.FirstOrDefault(i => i.id == newID); }
            else { obj = items.FirstOrDefault(i => i.id == oldID); }
            if (obj._size == ItemSize.Medium || obj._size == ItemSize.Big)
            {
                inventory.Add(obj);
                CurrentSlot = inventory.Count-1;
            }
            else if (obj._size == ItemSize.Small)
            {
                ItemCell addedResource = new ItemCell()
                {
                    item = obj,
                    count = 1
                };
                if (resources.Count == 0) resources.Add(addedResource);
                else
                {
                    for(int i = 0; i< resources.Count; i++)
                    {
                        if(resources[i].item == addedResource.item)
                        {
                            resources[i].count += addedResource.count;
                            break;
                        }
                        else if(i == resources.Count -1 && resources[i].item != addedResource.item)
                        {
                            resources.Add(addedResource);
                            break;
                        }
                    }
                }
            }

        }
        public void deleteItem(Item obj)
        {
            inventory.Remove(obj);
        }
        public Item getItem(int slot)
        {
            if (slot >= 0 && slot <= inventory.Count)
            {
                return inventory[slot];
            }
            else
            {
                Debug.LogError("Index out of range: " + slot);
                return null; 
            }
        }
    }

    [Serializable]
    public class ItemCell
    {
        public Item item;
        public int count;
    }

}