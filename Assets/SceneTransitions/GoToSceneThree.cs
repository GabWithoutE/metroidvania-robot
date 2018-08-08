using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSceneThree : MonoBehaviour {
    private GameObject loadingScreen;
    private StageManager stageManager;
    void Start()
    {
        loadingScreen = GameObject.Find("Loading Screen");
        stageManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<StageManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            if (col.gameObject.tag == "Player")
            {
                stageManager.SaveAll();
                //GetComponent<GlobalGameObject>().LoadGameScene("BossFight");
                GameObject globalObject = GameObject.Find("GlobalGameObject") as GameObject;
                //globalObject.GetComponentInChildren<GlobalGameObject>().DisplayUIElement(loadingScreen);
                globalObject.GetComponentInChildren<GlobalGameObject>().LoadGameScene("SceneTransitions3").allowSceneActivation = true;
            }
        }        
    }    
}
