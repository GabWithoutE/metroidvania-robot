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
        //Only register if stage manager does not have it
        if(!stageManager.ContainsEnemy(gameObject.name))
        {
            Vector2 enemyPosition = transform.position;
            EnemyState enemyState = new EnemyState(gameObject.name, enemyPosition);
            stageManager.RegisterState(enemyState);
        }        
	}
}
