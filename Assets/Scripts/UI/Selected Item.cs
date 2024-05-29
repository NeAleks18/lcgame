using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mechanics.Inventory;

public class SelectedItem : MonoBehaviour
{
    public Inventory inventory;
    public Image image;
    public TextMeshProUGUI discription;
    public TextMeshProUGUI Name;
    

    private Item item;

    void Start()
    {
        SetItem();
    }

    private void OnEnable()
    {
        
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
