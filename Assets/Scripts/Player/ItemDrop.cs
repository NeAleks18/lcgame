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
            if (Inventory.getItem(Inventory.CurrentSlot)._size == ItemSize.Big)
            {
                CmdDropItem();
            }
        }
    }

    [Command]
    private void CmdDropItem()
    {
        item = Inventory.getItem(Inventory.CurrentSlot);
        if (item._model != null)
        {
            Inventory.deleteItem(item);
            GameObject obj = Instantiate(item._model, gameObject.transform.position + gameObject.transform.forward * moveDistance, Quaternion.identity);
            NetworkServer.Spawn(obj);
        }
    }
}
