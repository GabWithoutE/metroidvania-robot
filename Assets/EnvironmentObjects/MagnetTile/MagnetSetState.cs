using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sets player state when player walks under magnet without an aluminum shield

public class MagnetSetState : MonoBehaviour {
    private ICharacterStateManager stateManager;
    private CharacterState magnetState;
    private IInventory inventory;

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        stateManager = go.GetComponent<ICharacterStateManager>();
        if (stateManager.ExistsState(ConstantStrings.MAGNET_STATE))
        {
            magnetState = stateManager.GetExistingCharacterState(ConstantStrings.MAGNET_STATE);
        }
        else
        {
            magnetState = new CharacterState(ConstantStrings.MAGNET_STATE, false);
            stateManager.RegisterCharacterState(magnetState.name, magnetState);
        }        
        GameObject inventoryGameObject = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryGameObject.GetComponentInChildren<IInventory>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //If player does not have an aluminum shield, set to true
            if(inventory.FindItemIndexByName("AluminumShield") == -1)
            {
                magnetState.SetState(true);
            }            
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            magnetState.SetState(false);
        }
    }
}
