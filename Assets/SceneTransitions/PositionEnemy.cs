using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Each enemy has this script. It looks in the stage manager to see if there is saved data on
//its position. If there is, enemy moves to where its last saved position is

public class PositionEnemy : MonoBehaviour {
    private IStageManager stageManager;
    private ICharacterStateManager stateManager;

    // Use this for initialization
    void Start () {
        stageManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<StageManager>();
        stateManager = GetComponentInParent<CharacterStatesManager>();
        //Loads from save file
        stageManager.LoadAll();
        //If there is no save data, leave it in the position as it is
        if(stageManager.activeSceneSaveFileExists())
        {
            Debug.Log("Dead state:");
            Debug.Log(stageManager.GetEnemyState(gameObject.name).isDead());
            //If enemy is dead
            if (stageManager.GetEnemyState(gameObject.name).isDead())
            {
                //Debug.Log("Here1");
                //Set position
                transform.position = stageManager.GetEnemyState(gameObject.name).getCurrentPosition().getVect2();
            }
            //If not dead, leave it in the position as it is
            else
            {
                //Debug.Log("Here2");
                //Debug.Log(stageManager.GetEnemyState(gameObject.name).getStartingPosition().getVect2());
                transform.position = stageManager.GetEnemyState(gameObject.name).getStartingPosition().getVect2();
            }              
        }
    }
}
