using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTwoToOne : MonoBehaviour {
    private GameObject loadingScreen;
    void Start()
    {
        loadingScreen = GameObject.Find("Loading Screen");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            if (col.gameObject.tag == "Player")
            {
                //GetComponent<GlobalGameObject>().LoadGameScene("BossFight");
                GameObject globalObject = GameObject.Find("GlobalGameObject") as GameObject;
                //globalObject.GetComponentInChildren<GlobalGameObject>().DisplayUIElement(loadingScreen);
                globalObject.GetComponentInChildren<GlobalGameObject>().LoadGameScene("SceneTransitions1").allowSceneActivation = true;
                
            }
        }        
    }    
}
