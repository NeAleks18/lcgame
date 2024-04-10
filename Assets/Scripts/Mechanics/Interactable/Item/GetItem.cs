using UnityEngine;
namespace Mechanics.Interactable
{
    public class GetItem : BaseInteractable
    {
        public Item item;
        private void Awake()
        {
            Type = "Item";
            TimeToUse = 1f;
        }

        public override void Interact()
        {
            Inventory.Inventory.Instance.addItem(item);
            GameObject.Destroy(gameObject);
        }
    }
}