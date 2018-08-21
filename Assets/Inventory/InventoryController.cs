using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    private Inventory inventory;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
    }

    //Returns index of GameObject item with input name in inventory. If not found, return -1
    public int FindItemIndexByName(string name)
    {
        return inventory.FindItemIndexByName(name);
    }
}
