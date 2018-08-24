using UnityEngine;
//Add any other classfications of items here. Used to determine what animation to use
public enum ItemType
{
    Melee,
    Thrown,
    Fired,
    Held
}

//Everything saved in the inventory will be instances of this class. This class stores the relevant
//information of the items needed for game mechanics. Sprites are not serializable so they cannot
//be included here.

[System.Serializable]
public class Item
{
    public string name;     //Name of item
    public int quantity;    //Amount of this item
    public ItemType itemType;   //Type of item, held/melee/fired/thrown... Used for switching animations

    public Item(string nameIn, ItemType type)
    {
        name = nameIn;
        quantity = 1;   //Defaults to creating one item
        itemType = type;
    }
    public string getName()
    {
        return name;
    }
    public int getQuantity()
    {
        return quantity;
    }
    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
    public void RemoveQuantity(int amount)
    {
        quantity -= amount;
    }
}