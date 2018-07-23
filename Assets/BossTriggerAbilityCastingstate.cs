using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossTriggerAbilityCastingstate : AbstractAbilityCastingState
{
    private CharacterStatesManager statesManager;
    private CharacterState playerWithinMeleeRange;
    public bool withinMeleeRange;
    private float meleeChance = 0.5f;   //Chance that boss will melee if player is within melee range
    private float hammerChance = 0.05f;  //Chance that boss will throw hammer if player is not within melee range
    private GameObject player;

    private void Awake()
    {
        base.Awake();
    }

    void Start()
    {        
        //Only needs to know where player is for hammer throw
        player = GameObject.FindGameObjectWithTag("Player");
        statesManager = GetComponentInParent<CharacterStatesManager>();
        if (stateManager.ExistsState("PlayerWithinMeleeRange"))
        {
            playerWithinMeleeRange =
                stateManager.GetExistingCharacterState("PlayerWithinMeleeRange");
            playerWithinMeleeRange.SetState(false);
        }
        else {
            playerWithinMeleeRange = new CharacterState("PlayerWithinMeleeRange", false);
            statesManager.RegisterCharacterState("PlayerWithinMeleeRange", playerWithinMeleeRange);
        }
        
        //playerWithinMeleeRange = statesManager.GetExistingCharacterState("PlayerWithinMeleeRange");
        //statesManager.RegisterCharacterState(playerWithinMeleeRange.name, playerWithinMeleeRange);

        CharacterState.CharacterStateSubscription inMeleeRangeStateSubscription = statesManager.GetCharacterStateSubscription("PlayerWithinMeleeRange");
        inMeleeRangeStateSubscription.OnStateChanged += CheckInMeleeRangeState;
    }

    protected override void Update()
    {
        //withinMeleeRange = (bool)playerWithinMeleeRange.GetStateValue();
        float randomNum = Random.Range(0.0f, 1.0f);
        if(inputName == "InputLightAttack")
        {
            //If player is within melee range, 
            if (withinMeleeRange)
            {
                if (randomNum < meleeChance)
                {
                    //Melee attack immediately
                    abilityCastingState.SetState(true);
                    abilityCastingState.SetState(false);
                    //Debug.Log("Melee");
                }
                else
                {
                    //Walk for two seconds before melee
                }
            }
        }
        else if(inputName == "InputHeavyAttack")
        {
            if (randomNum < hammerChance)
            {
                //Throw hammer
                abilityCastingState.SetState(true);
                abilityCastingState.SetState(false);
                //Debug.Log("Hammer");
            }
            else
            {
                //Keep walking
            }
        }
    }

    private void CheckInMeleeRangeState(object playerWithinMeleeRangeObj)
    {
        withinMeleeRange = (bool)playerWithinMeleeRange.GetStateValue();        
    }
}
