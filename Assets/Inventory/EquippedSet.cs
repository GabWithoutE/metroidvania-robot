using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds 3 items.
//[0]: Light weapon
//[1]: Heavy weapon
//[2]: Utility
//Collection of 3 items that are equipped for any transformation
[System.Serializable]
public class EquippedSet {
    [SerializeField]
    private Item[] equippedWeapons;
    private string transformationName;      //Name of transformation this equipment set belongs to

    //Constructor creates an equipped set with input name and empty item slots
    public EquippedSet(string setName)
    {
        equippedWeapons = new Item[3];
        transformationName = setName;
    }

    //Returns name of equipment set
    public string getTransformationName()
    {
        return transformationName;
    }

    //Sets name of equipment set
    public void setTransformationName(string inputName)
    {
        transformationName = inputName;
    }

    //Returns light item
    public Item getLightItem()
    {
        return equippedWeapons[0];
    }

    //Returns heavy item
    public Item getHeavyItem()
    {
        return equippedWeapons[1];
    }

    //Returns utility item
    public Item getUtilityItem()
    {
        return equippedWeapons[2];
    }

    //Replaces light item slot with input item
    public void EquipLight(Item lightItem)
    {
        equippedWeapons[0] = lightItem;
    }

    //Replaces heavy item slot with input item
    public void EquipHeavy(Item heavyItem)
    {
        equippedWeapons[1] = heavyItem;
    }

    //Replaces utility item slot with input item
    public void EquipUtility(Item utilityItem)
    {
        equippedWeapons[2] = utilityItem;
    }

    //Removes light item, leaves item slot as null
    public void RemoveLight()
    {
        equippedWeapons[0] = null;
    }

    //Removes heavy item, leaves item slot as null
    public void RemoveHeavy()
    {
        equippedWeapons[1] = null;
    }

    //Removes utlity item, leaves item slot as null
    public void RemoveUtility()
    {
        equippedWeapons[2] = null;
    }
}
