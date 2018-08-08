using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSceneTwo : MonoBehaviour {
    private GameObject loadingScreen;
    private StageManager stageManager;
    void Start()
    {
        loadingScreen = GameObject.Find("Loading Screen");
        stageManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<StageManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null)
        {
            if (col.gameObject.tag == "Player")
            {
                //Stage manager updates lists and saves to file
                stageManager.BeforeLeavingScene();
                //GetComponent<GlobalGameObject>().LoadGameScene("BossFight");
                GameObject globalObject = GameObject.Find("GlobalGameObject") as GameObject;
                //globalObject.GetComponentInChildren<GlobalGameObject>().DisplayUIElement(loadingScreen);
                globalObject.GetComponentInChildren<GlobalGameObject>().LoadGameScene("SceneTransitions2").allowSceneActivation = true;                
            }
        }        
    }    
}
