using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every item will have this script attached to it. This script registers the attached gameobject as an
//item in scene's StageManager

public class RegisterItem : MonoBehaviour {
    private StageManager stageManager;

    // Use this for initialization
    void Start()
    {
        stageManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<StageManager>();
        //Adds itself to list of items if not already on the list
        stageManager.RegisterItem(transform.root.gameObject);
        /*
        Vector2 itemPosition;
        //Only register if stage manager does not have it
        if (!stageManager.ContainsItem(gameObject.name))
        {
            itemPosition = transform.position;
            ItemState itemState = new ItemState(gameObject.name, itemPosition);
            stageManager.RegisterState(itemState);
        }
        else
        {
            itemPosition = stageManager.GetItemState(gameObject.name).getPosition().getVect2();
        }
        //Place the item
        transform.position = itemPosition;
        */
    }
}
