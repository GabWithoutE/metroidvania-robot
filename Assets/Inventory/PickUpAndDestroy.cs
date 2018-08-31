using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds item to player's inventory and disables item

public class PickUpAndDestroy : MonoBehaviour {
    private IInventory inventory;
    private TypeOfItem typeOfItem;
    void Start()
    {
        GameObject inventoryGameObject = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryGameObject.GetComponentInChildren<IInventory>();
        typeOfItem = GetComponent<TypeOfItem>();
    }

    //Adds this gameobject to inventory and disables this gameobject when player collides into it
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Create item object to add to inventory
            Item tempItem = new Item(gameObject.name, typeOfItem.type);
            inventory.AddItem(tempItem);
            gameObject.SetActive(false);
        }
    }
}
