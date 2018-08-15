using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    private List<GameObject> inventory;

    void Awake()
    {
        inventory = new List<GameObject>();
    }

    public void AddItem(GameObject item)
    {
        inventory.Add(item);
    }

    //Returns index of GameObject item in inventory
    public int FindItemIndex(GameObject item)
    {
        return inventory.IndexOf(item);
    }

    //Returns index of GameObject item with input name in inventory. If not found, return -1
    public int FindItemByName(string name)
    {
        for(int i = 0; i < inventory.Count; ++i)
        {
            if(inventory[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
}
