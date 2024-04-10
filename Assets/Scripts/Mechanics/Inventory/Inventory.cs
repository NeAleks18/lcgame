using UnityEngine;
using System.Collections.Generic;
using Mirror;

namespace Mechanics.Inventory
{
    public class Inventory : NetworkBehaviour
    {
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
            inventory.Add(obj);
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

}