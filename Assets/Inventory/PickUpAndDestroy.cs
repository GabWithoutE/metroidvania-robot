using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndDestroy : MonoBehaviour {
    private InventoryController inventoryController;

    void Start()
    {
        GameObject globalGameObject = GameObject.FindGameObjectWithTag("GlobalGameObject");
        inventoryController = globalGameObject.GetComponentInChildren<InventoryController>();
    }

    //Adds this gameobject to inventory and disables this gameobject when player collides into it
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            inventoryController.AddItem(gameObject);
            gameObject.SetActive(false);
        }
    }
}
