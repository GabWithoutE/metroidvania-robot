using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;     //Name of item
    public int quantity;    //Amount of this item

    public Item(string nameIn)
    {
        name = nameIn;
        quantity = 1;   //Defaults to creating one item
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
