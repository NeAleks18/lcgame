using UnityEngine;
using System.Collections.Generic;
using Mirror;
using System;

namespace Mechanics.Inventory
{
    public class Inventory : NetworkBehaviour
    {
        [SerializeField]
        private List<ItemCell> resources = new List<ItemCell>();

        [SerializeField]
        public List<Item> inventory = new List<Item>();
        [SerializeField]
        public int CurrentSlot;


        public static Inventory Instance;

        private void Start()
        {
            if (isLocalPlayer) Instance = this;
            Debug.Log(inventory.Count);
        }
        public void addItem(Item obj)
        {
            if (obj._size == ItemSize.Medium || obj._size == ItemSize.Big)
            {
                inventory.Add(obj);
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
            return inventory[slot];
        }
    }

    [Serializable]
    public class ItemCell
    {
        public Item item;
        public int count;
    }

}