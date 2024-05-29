using Mirror;
using UnityEngine;
using Mechanics.Inventory;

public class ItemDrop : NetworkBehaviour
{
    [SerializeField] private Inventory Inventory;
    public float moveDistance;

    private Item item;

    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetButtonDown("Drop"))
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        if (Inventory.GetItem(Inventory.CurrentSlot)._size == ItemSize.Big)
        {
            item = Inventory.GetItem(Inventory.CurrentSlot);
            CmdDropItem(isServer);
            Inventory.DeleteItem(item);
        }
    }

    [Command]
    private void CmdDropItem(bool Server)
    {
        if (!Server)
        {
            item = Inventory.GetItem(Inventory.CurrentSlot);
            Inventory.DeleteItem(item);
        }
        if (item != null)
        {
            GameObject obj = Instantiate(item._model, gameObject.transform.position + gameObject.transform.forward * moveDistance, Quaternion.identity);
            NetworkServer.Spawn(obj);
            
        }
    }
}


