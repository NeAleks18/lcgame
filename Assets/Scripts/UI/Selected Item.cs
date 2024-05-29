using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mechanics.Inventory;
using Mirror;
public class SelectedItem : NetworkBehaviour
{
    public Inventory inventory;
    public ScrollSlot scrollSlot;
    public Image image;
    public TextMeshProUGUI discription;
    public TextMeshProUGUI Name;
    
    
    private Item item;

    void Start()
    {
        if (!isLocalPlayer) return;
        
            scrollSlot.OnSlotScrolled += SetItem;
            SetItem();
        
    }


    private void SetItem()
    {
        item = inventory.GetItem(inventory.CurrentSlot);
        if (item != null)
        {
            image.sprite = item._sprite;
            discription.text = item._description;
            Name.text = item._name;
        }
    }

    
}
