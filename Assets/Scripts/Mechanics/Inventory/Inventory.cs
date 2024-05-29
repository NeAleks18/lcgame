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

        [SyncVar(hook = nameof(AddItem))]
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
        public void AddItem(int oldID , int newID)
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

        public void DeleteItem(Item obj)
        {
            

            if (obj._size == ItemSize.Medium || obj._size == ItemSize.Big)
            {
                inventory.Remove(obj);
            }
            else if (obj._size == ItemSize.Small)
            {
                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].item == obj)
                    {
                        resources[i].count--;
                        if (resources[i].count <= 0) { resources.Remove(resources[i]); }
                    }
                }

            }
        }

        public Item GetItem(int slot)
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

        public bool CheckItem(Item obj)
        {
            if (obj == null) return false;
            
            if (obj._size == ItemSize.Medium || obj._size == ItemSize.Big)
            {
                return inventory.Contains(obj);
            }
            else if (obj._size == ItemSize.Small)
            {
                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].item == obj)
                    {
                        return true;
                    }
                    else if (i == resources.Count - 1 && resources[i].item != obj)
                    {
                        return false;
                    }
                }
                
            }
            return false;
        }
    }

    [Serializable]
    public class ItemCell
    {
        public Item item;
        public int count;
    }

}