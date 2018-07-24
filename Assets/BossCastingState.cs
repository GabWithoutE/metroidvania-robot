using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastingState : MonoBehaviour {
    private CharacterStatesManager stateManager;
    private CharacterState meleeAttackCastState;
    private CharacterState hammerThrowCastState;
    private CharacterState jumpState;
    private CharacterState busyState;
    public float busyDuration;  //One action every busyDuration seconds
    public float attack1Chance;
    public float attack2Chance;
    public float attack3Chance;

    public enum BossCastingStates
    {
        MeleeState,
        HammerThrowState,
        JumpState
    }

    void Awake()
    {
        stateManager = GetComponentInParent<CharacterStatesManager>();
        meleeAttackCastState = new CharacterState("meleeAttackCastState", false);
        stateManager.RegisterCharacterState("meleeAttackCastState", meleeAttackCastState);
        hammerThrowCastState = new CharacterState("hammerThrowCastState", false);
        stateManager.RegisterCharacterState("hammerThrowCastState", hammerThrowCastState);
        jumpState = new CharacterState("jumpState", false);
        stateManager.RegisterCharacterState("jumpState", jumpState);
        busyState = new CharacterState("busyState", false);
        stateManager.RegisterCharacterState("busyState", jumpState);
        /*
        if (!stateManager.ExistsState("meleeAttackCastState"))
        {
            meleeAttackCastState = new CharacterState("meleeAttackCastState", false);
            stateManager.RegisterCharacterState("meleeAttackCastState", meleeAttackCastState);
        }
        else
        {            
            meleeAttackCastState =
                stateManager.GetExistingCharacterState("meleeAttackCastState");
            meleeAttackCastState.SetState(false);
        }
        if (stateManager.ExistsState("hammerThrowCastState"))
        {
            hammerThrowCastState =
                stateManager.GetExistingCharacterState("hammerThrowCastState");
            hammerThrowCastState.SetState(false);
        }
        else
        {
            hammerThrowCastState = new CharacterState("hammerThrowCastState", false);
            stateManager.RegisterCharacterState("hammerThrowCastState", hammerThrowCastState);
        }
        if (stateManager.ExistsState("jumpState"))
        {
            jumpState =
                stateManager.GetExistingCharacterState("jumpState");
            jumpState.SetState(false);
        }
        else
        {
            jumpState = new CharacterState("jumpState", false);
            stateManager.RegisterCharacterState("jumpState", jumpState);
        }
        */
    }

    void Start()
    {
        busyState.SetState(true);
        BusyStart(busyDuration);
    }

	
	// Update is called once per frame
	void Update () {
        //Once boss is no longer busy, figure out what to do next
        if(!(bool)busyState.GetStateValue())
        {
            //Come up with random number
            float randomNum = Random.Range(0.0f, 1.0f);
            //Depending on randomNum, set one of the casting states to true
            if (randomNum < attack1Chance)
            {
                meleeAttackCastState.SetState(true);
                meleeAttackCastState.SetState(false);
            }
            else if (randomNum >= attack1Chance && randomNum < (attack1Chance + attack2Chance))
            {
                hammerThrowCastState.SetState(true);
                hammerThrowCastState.SetState(false);               
            }
            else if (randomNum >= (attack1Chance + attack2Chance) && randomNum < (attack1Chance + attack2Chance + attack3Chance))
            {
                jumpState.SetState(true);
                jumpState.SetState(false);
            }
            busyState.SetState(true);
            BusyStart(busyDuration);
        }        
    }

    private void BusyStart(float duration)
    {
        StartCoroutine(BusyDuration(duration));
    }

    IEnumerator BusyDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        busyState.SetState(false);
    }
}
