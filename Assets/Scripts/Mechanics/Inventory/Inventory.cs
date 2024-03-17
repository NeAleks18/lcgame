using UnityEngine;

namespace Mechanics.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public const short slots = 3;
        [SerializeField]
        private GameObject[] list = new GameObject[slots];
        [SerializeField]
        public short CurrentSlot;

        public void addItem(GameObject obj, short slot)
        {
            list[slot] = obj;
        }
        public void deleteItem(short slot)
        {
            list[slot] = null;
        }
        public GameObject getItem(short slot)
        {
            return list[slot];
        }
    }

}