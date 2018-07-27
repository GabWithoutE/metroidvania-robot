using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationStates : MonoBehaviour {
    private ICharacterStateObserver stateObserver;
    private Animator bossAnimator;
    private int walkingHash;
    private int jumpingHash;
    private int fallingHash;
    private int meleeHash;
    private int hammerHash;

    void Awake()
    {
        stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
        walkingHash = Animator.StringToHash("Walking");
        jumpingHash = Animator.StringToHash("Jumping");
        fallingHash = Animator.StringToHash("Falling");
        meleeHash = Animator.StringToHash("Melee");
        hammerHash = Animator.StringToHash("Hammer");
    }

    // Use this for initialization
    void Start () {
        bossAnimator = GetComponent<Animator>();
        CharacterState.CharacterStateSubscription hammerThrowSub = stateObserver.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.HAMMER_THROW_CAST_STATE);
        hammerThrowSub.OnStateChanged += OnHammerThrow;
        CharacterState.CharacterStateSubscription meleeAttackSub = stateObserver.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.MELEE_ATTACK_CAST_STATE);
        meleeAttackSub.OnStateChanged += OnMeleeAttack;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnHammerThrow(object hammerThrowCastState)
    {
        if((bool)hammerThrowCastState)
        {
            bossAnimator.SetBool(hammerHash, true);
        }
        else
        {
            bossAnimator.SetBool(hammerHash, false);
        }
    }

    private void OnMeleeAttack(object meleeAttackCastState)
    {
        if((bool)meleeAttackCastState)
        {
            bossAnimator.SetBool(meleeHash, true);
        }
        else
        {
            bossAnimator.SetBool(meleeHash, false);
        }
    }
}
