using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquippedSetManager {
    void EquipLightInCurrent(Item lightItem);   //Equip input item in light item slot in current set
    void EquipHeavyInCurrent(Item heavyItem);   //Equip input item in heavy item slot in current set
    void EquipUtilityInCurrent(Item utilityItem);   //Equip input item in utility item slot in current set
    void SaveEquippedInventory();       //Save equipped inventory to file
    void LoadEquippedInventory();       //Load equipped inventory from file
    void SwapSet();        //Change current set to other set
}
