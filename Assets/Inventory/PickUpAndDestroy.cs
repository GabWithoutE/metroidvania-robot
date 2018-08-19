using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndDestroy : MonoBehaviour {
    //private InventoryController inventoryController;
    private Inventory inventory;
    public Item item;

    void Start()
    {
        GameObject globalGameObject = GameObject.FindGameObjectWithTag("GlobalGameObject");
        //inventoryController = globalGameObject.GetComponentInChildren<InventoryController>();
        inventory = FindObjectOfType<Inventory>();
    }

    //Adds this gameobject to inventory and disables this gameobject when player collides into it
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
