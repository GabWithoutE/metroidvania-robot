using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsMagnet : MonoBehaviour {
    private Rigidbody2D characterRigidBody;
    private IMovementHandler characterMovementHandler;
    private ICharacterStateManager stateManager;
    private CharacterState magnetState;
    public float magnetAttractSpeed;

    // Use this for initialization
    void Start () {
        characterRigidBody = GetComponentInParent<Rigidbody2D>();
        characterMovementHandler =
            transform.root.GetComponentInChildren(typeof(IMovementHandler)) as IMovementHandler;
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        if(stateManager.ExistsState(ConstantStrings.MAGNET_STATE))
        {
            magnetState = stateManager.GetExistingCharacterState(ConstantStrings.MAGNET_STATE);
        }
        else
        {
            magnetState = new CharacterState(ConstantStrings.MAGNET_STATE, false);
            stateManager.RegisterCharacterState(magnetState.name, magnetState);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Moves character upwards if magnet state is true
		if((bool)stateManager.GetCharacterStateValue(ConstantStrings.MAGNET_STATE))
        {
            characterMovementHandler.AddToYPosition(magnetAttractSpeed * Time.deltaTime);
        }
	}
}
