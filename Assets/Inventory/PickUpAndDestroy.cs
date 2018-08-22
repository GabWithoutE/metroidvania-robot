using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndDestroy : MonoBehaviour {
    private IInventory inventory;

    void Start()
    {
        GameObject inventoryGameObject = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryGameObject.GetComponentInChildren<IInventory>();
    }

    //Adds this gameobject to inventory and disables this gameobject when player collides into it
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Create item object to add to inventory
            Item tempItem = new Item(gameObject.name);
            inventory.AddItem(tempItem);
            gameObject.SetActive(false);
        }
    }
}
