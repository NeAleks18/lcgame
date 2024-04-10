using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "GamePlay/Inventory/Item",order =1)]

public class Item : ScriptableObject
{
    public string _name;
    [TextArea] public string _description;
    public ItemType _type;
    public ItemSize _size;
    public Sprite _sprite;
    public GameObject _model;
}

public enum ItemType
{
    Tool,Weapon,Resourse,Scrap
}
public enum ItemSize
{
    Small,Medium,Big
}