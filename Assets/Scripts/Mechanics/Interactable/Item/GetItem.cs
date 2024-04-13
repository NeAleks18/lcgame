using Mirror;
using UnityEngine;
using Mechanics.Inventory;

namespace Mechanics.Interactable
{
    public class GetItem : BaseInteractable
    {
        public Item item;

        public override void OnStartClient()
        {
            base.OnStartClient();
            Type = "Item";
            TimeToUse = 1f;

        }


        [ClientRpc]
        public override void Interact()
        {
            CmdInteract();
        }

        [Command]
        public void CmdInteract()
        {
            Inventory.Inventory.Instance.addItem(item);
            NetworkServer.Destroy(gameObject);
        }

    }
}
