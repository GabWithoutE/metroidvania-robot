using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossAggro : MonoBehaviour {
    private CharacterStatesManager statesManager;
    private CharacterState playerWithinMeleeRange;

    // Use this for initialization
    void Start () {
        statesManager = GetComponentInParent<CharacterStatesManager>();
        if (statesManager.ExistsState("PlayerWithinMeleeRange"))
        {
            playerWithinMeleeRange =
                statesManager.GetExistingCharacterState("PlayerWithinMeleeRange");
            playerWithinMeleeRange.SetState(false);
        }
        else
        {
            playerWithinMeleeRange = new CharacterState("PlayerWithinMeleeRange", false);
            statesManager.RegisterCharacterState("PlayerWithinMeleeRange", playerWithinMeleeRange);
        }
        //playerWithinMeleeRange = new CharacterState("PlayerWithinMeleeRange", false);
        //statesManager.RegisterCharacterState(playerWithinMeleeRange.name, playerWithinMeleeRange);
        //CharacterState.CharacterStateSubscription inMeleeRangeStateSubscription = statesManager.GetCharacterStateSubscription("PlayerWithinMeleeRange");
        //inMeleeRangeStateSubscription.OnStateChanged += CheckInMeleeRangeState;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject != null)
        {
            if (col.gameObject.tag == "Player")
            {
                playerWithinMeleeRange.SetState(true);
            }
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject != null)
        {
            if (col.gameObject.tag == "Player")
            {
                playerWithinMeleeRange.SetState(false);
            }
        }
    }
}
