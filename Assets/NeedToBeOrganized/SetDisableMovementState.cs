using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sets player's disable move state
//Stops player from moving when:
//1. Viewing minimap
//2. Level is fading in
//State to prevent the player from moving while the screen is fading between scenes
//If player moves before the fading is finished, there can be an I/O error with save files
//State is true when screen is fading, false when screen is not fading

public class SetDisableMovementState : MonoBehaviour {
    private ICharacterStateManager stateManager;
    private CharacterState disableMoveState;

    void Awake()
    {
        stateManager = GetComponentInParent<ICharacterStateManager>();
        disableMoveState = new CharacterState(ConstantStrings.DISABLE_MOVE_STATE, true);
        stateManager.RegisterCharacterState(disableMoveState.name, disableMoveState);
    }
}
