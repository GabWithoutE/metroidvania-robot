using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every enemy will have this script attached to it. This script registers the attached gameobject as an
//enemy in scene's StageManager

public class RegisterEnemy : MonoBehaviour {
    private StageManager stageManager;

    void Awake()
    {
        stageManager = GetComponentInParent<StageManager>();    
    }

	// Use this for initialization
	void Start () {
        Vector2 enemyPosition;
        //Only register if stage manager does not have it
        if (!stageManager.ContainsEnemy(gameObject.name))
        {
            //Place the enemy whereever it was placed in the inspector
            enemyPosition = transform.position;
            EnemyState enemyState = new EnemyState(gameObject.name, enemyPosition);
            stageManager.RegisterState(enemyState);
        }
        //If stage manager contains the enemy, place it where the save file says to
        else
        {
            enemyPosition = stageManager.GetEnemyState(gameObject.name).getPosition();
        }
        //Place the enemy
        transform.position = enemyPosition;
	}
}
