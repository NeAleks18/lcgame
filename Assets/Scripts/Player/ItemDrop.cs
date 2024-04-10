using Mirror;
using UnityEngine;
using Mechanics.Inventory;

    public class ItemDrop : NetworkBehaviour
{
    [SerializeField]
    public Inventory Inventory;

    public float moveDistance;

    private Item Item;
    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetButtonDown("Drop"))
        {
            if (Inventory.getItem(Inventory.CurrentSlot)._size == ItemSize.Big)
            {
                dropItem();
            }
        }
    }
    [Command]
    public void dropItem()
    {
            Item = Inventory.getItem(Inventory.CurrentSlot);
            if (Item._model != null)
            {
                Inventory.deleteItem(Inventory.getItem(Inventory.CurrentSlot));
                GameObject obj = Instantiate(Item._model, gameObject.transform.position + gameObject.transform.forward * moveDistance, Quaternion.identity);
                NetworkServer.Spawn(obj);
            }
    
    }
}