using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to exit tiles. Type in name of scene to go to when player collides with *this* in destinationSceneName

public class ExitTileActivate : MonoBehaviour {
    private GameObject loadingScreen;
    public string destinationSceneName;
    private SceneController sceneController;

    void Start()
    {
        loadingScreen = GameObject.Find("Loading Screen");
        GameObject sceneControllerGameObject = GameObject.FindGameObjectWithTag("SceneController");
        sceneController = sceneControllerGameObject.GetComponent<SceneController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            if (col.gameObject.tag == "Player")
            {
                sceneController.FadeAndLoadScene(destinationSceneName);
            }
        }        
    }    
}
