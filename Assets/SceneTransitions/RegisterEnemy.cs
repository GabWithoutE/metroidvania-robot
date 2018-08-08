using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every enemy will have this script attached to it. This script registers the attached gameobject as an
//enemy in scene's StageManager

public class RegisterEnemy : MonoBehaviour {
    private IStageManager stageManager;
    private ICharacterStateManager stateManager;

    void Awake()
    {
        stateManager = GetComponentInParent<CharacterStatesManager>();
        //Register original position to character state manager
        Vector2 tempPosition = transform.root.position;
        CharacterState originalPosition = new CharacterState("OriginalPosition", tempPosition);
        stateManager.RegisterCharacterState("OriginalPosition", originalPosition);
        //Debug.Log(stateManager.GetCharacterStateValue("OriginalPosition"));
    }

	// Use this for initialization
	void Start () {
        stageManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<StageManager>();        
        //Adds itself to list of enemies if not already on the list
        GameObject go = transform.root.gameObject;
        stageManager.RegisterEnemy(ref go);        
	}
}
