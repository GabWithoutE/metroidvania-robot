using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Testing script. Attach this script to an item and this item will be equipped into the player's
//light item slot when the player picks this up

public class TestEquip : MonoBehaviour {
    private IEquippedSetManager equippedSetManager;
    private TypeOfItem typeOfItem;
    // Use this for initialization
    void Start () {
        GameObject equippedSetManagerGameObject = GameObject.FindGameObjectWithTag("EquippedSetManager");
        equippedSetManager = equippedSetManagerGameObject.GetComponent<IEquippedSetManager>();
        typeOfItem = GetComponent<TypeOfItem>();
    }

    //Add shield to light item slot
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Item tempItem = new Item(gameObject.name, typeOfItem.type);
            equippedSetManager.EquipLightInCurrent(tempItem);
        }
    }
}
