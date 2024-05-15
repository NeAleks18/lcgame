using Mirror;
using UnityEngine;
using Mechanics.Inventory;

namespace Mechanics.Interactable
{
    public class GetItem : BaseInteractable
    {
        [SerializeField]
        private Item item;

        public override void OnStartClient()
        {
            base.OnStartClient();
            TimeToUse = 1f;

        }

        public override void Interact(GameObject playerObject)
        {
            NetworkServer.Destroy(gameObject);
            playerObject.GetComponent<Inventory.Inventory>().AddedItemID = (playerObject.GetComponent<Inventory.Inventory>().AddedItemID == item.id) ? 2147483646 : item.id;
        }

    }
}
