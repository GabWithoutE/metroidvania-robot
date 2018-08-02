using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every item will have this script attached to it. This script registers the attached gameobject as an
//item in scene's StageManager

public class RegisterItem : MonoBehaviour {
    private StageManager stageManager;

    void Awake()
    {
        stageManager = GetComponentInParent<StageManager>();
    }

    // Use this for initialization
    void Start()
    {
        //Only register if stage manager does not have it
        if (!stageManager.ContainsItem(gameObject.name))
        {
            Vector2 itemPosition = transform.position;
            ItemState itemState = new ItemState(gameObject.name, itemPosition);
            stageManager.RegisterState(itemState);
        }
    }
}
