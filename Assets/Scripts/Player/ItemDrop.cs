using System.Collections;
using UnityEngine;
using Mechanics.Inventory;

    public class ItemDrop : MonoBehaviour
    {
    [SerializeField]
    public Inventory Inventory;

    public float moveDistance;

    private GameObject Object;
    void Update()
    {
        if (Input.GetButtonDown("Drop"))
        {
            Object = Inventory.getItem(Inventory.CurrentSlot);
            if (Object != null)
            {
                Inventory.deleteItem(Inventory.CurrentSlot);
                Object.transform.position = gameObject.transform.position + gameObject.transform.forward * moveDistance;
                Object.SetActive(true);
            }
        }
    }
    }