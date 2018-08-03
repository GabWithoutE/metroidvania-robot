using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Each enemy has this script. It looks in the stage manager to see if there is saved data on
//its position. If there is, enemy moves to where its last saved position is

public class PositionEnemy : MonoBehaviour {
    private StageManager stageManager;

    // Use this for initialization
    void Start () {
        stageManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<StageManager>();
        //Loads from save file. LoadAll() creates a save file if save file does not exist yet
        stageManager.LoadAll();
        //If enemy is registered, find position
        if(stageManager.ContainsEnemy(gameObject.name))
        {
            Debug.Log(transform.position);
            //Set enemy's position to saved position
            transform.position = stageManager.GetEnemyState(gameObject.name).getPosition();
        }
        //If not registered, leave it in the position as it is
    }
}
