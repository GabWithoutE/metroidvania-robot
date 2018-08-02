using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Each scene will have this script. This script finds out which enemies on this scene is still alive
//and instantiates enemies in their correct positions if they are alive

public class StageEnemies : MonoBehaviour {
    private StageManager stageManager;
    public GameObject enemy;
    void Awake()
    {
        stageManager = GetComponentInParent<StageManager>();
    }

    // Use this for initialization
    void Start () {
        //Loads from save file. LoadAll() creates a save file if save file does not exist yet
        stageManager.LoadAll();
        //Loop through each enemy in sceneObjectsEnemy in stageManager
        //If enemy is still alive, placeEnemy at a position
    }

    //Instantiates a weenie enemy at input position
    public void placeEnemy(Vector2 position)
    {
        Instantiate(enemy, position, Quaternion.identity);
    }
}
