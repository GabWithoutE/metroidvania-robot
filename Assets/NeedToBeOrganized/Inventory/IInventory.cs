using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory {
    void AddItem(Item item);    //If there are already instances of the item that is to be added, add amount to quantity instead
    void RemoveItem(Item item);     //Remove from quantity of item until quantity equals 0, then remove the item type from inventory
    int FindItemIndexByName(string itemName);   //Returns index of GameObject item with input name in inventory. If not found, return -1
    void SaveInventory();       //Saves inventory as .dat file
    void LoadInventory();       //Load save file into inventory
    int GetItemQuantity(string itemName);       //Returns quantity of GameObject item with input name in inventory. If not found, return 0
}
