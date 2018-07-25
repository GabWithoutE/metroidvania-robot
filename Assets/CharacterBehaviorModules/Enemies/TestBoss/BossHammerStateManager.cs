using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHammerStateManager : MonoBehaviour {
    private CharacterStatesManager statesManager;
    private CharacterState thrownHammerPosition;
    private CharacterState hammerThrown;
    private CharacterState hammerHitsGround;
    private CharacterState hammerHitPlayer;
    private CharacterState hammerHitEnemy;
    private CharacterState hammerPosition;
    LayerMask groundMask;

    void Awake()
    {
        statesManager = GetComponentInParent<CharacterStatesManager>();
        hammerThrown = new CharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN, false);
        statesManager.RegisterCharacterState(hammerThrown.name, hammerThrown);
        hammerHitsGround = new CharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_HITS_GROUND, false);
        statesManager.RegisterCharacterState(hammerHitsGround.name, hammerHitsGround);
        thrownHammerPosition = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.THROWN_HAMMER_POSITION);
        hammerHitPlayer = new CharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_HIT_PLAYER, false);
        statesManager.RegisterCharacterState(hammerHitPlayer.name, hammerHitPlayer);
        hammerHitEnemy = new CharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_HIT_ENEMY, false);
        statesManager.RegisterCharacterState(hammerHitEnemy.name, hammerHitEnemy);
        Vector2 zeroVect = new Vector2(0, 0);        
        hammerPosition = new CharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_POSITION, zeroVect);
        statesManager.RegisterCharacterState(hammerPosition.name, hammerPosition);
    }

    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        //hammerPosition = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_POSITION);
    }

    public void SetState(string stateName, object newState)
    {
        if(stateName == ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN)
        {
            hammerThrown.SetState(newState);
        }
        else if(stateName == ConstantStrings.Enemy.HammerBoss.HAMMER_HITS_GROUND)
        {
            hammerHitsGround.SetState(newState);
        }
        else if(stateName == ConstantStrings.Enemy.HammerBoss.THROWN_HAMMER_POSITION)
        {
            thrownHammerPosition.SetState(newState);
        }
        else if(stateName == ConstantStrings.Enemy.HammerBoss.HAMMER_POSITION)
        {
            hammerPosition.SetState(newState);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hammerHitPlayer.SetState(true);
            hammerHitPlayer.SetState(false);
        }
        else if ((groundMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            hammerHitsGround.SetState(true);
        }
        else if(collision.tag == "Enemy")
        {
            hammerHitEnemy.SetState(true);
            hammerHitEnemy.SetState(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if ((groundMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            hammerHitsGround.SetState(false);
        }
    }
}
