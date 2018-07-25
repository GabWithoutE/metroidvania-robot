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

    void Awake()
    {
        stateManager = GetComponentInParent<CharacterStatesManager>();
        meleeAttackCastState = new CharacterState(ConstantStrings.Enemy.HammerBoss.MELEE_ATTACK_CAST_STATE, false);
        stateManager.RegisterCharacterState(ConstantStrings.Enemy.HammerBoss.MELEE_ATTACK_CAST_STATE, meleeAttackCastState);
        hammerThrowCastState = new CharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROW_CAST_STATE, false);
        stateManager.RegisterCharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROW_CAST_STATE, hammerThrowCastState);
        jumpState = new CharacterState(ConstantStrings.Enemy.HammerBoss.JUMP_STATE, false);
        stateManager.RegisterCharacterState(ConstantStrings.Enemy.HammerBoss.JUMP_STATE, jumpState);
        busyState = new CharacterState(ConstantStrings.Enemy.HammerBoss.BUSY_STATE, false);
        stateManager.RegisterCharacterState(ConstantStrings.Enemy.HammerBoss.BUSY_STATE, jumpState);
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
            //Set boss as busy
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
