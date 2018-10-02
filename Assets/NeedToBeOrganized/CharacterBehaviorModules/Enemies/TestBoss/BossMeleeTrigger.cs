using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeTrigger : AbstractMeleeAttackTrigger {
    private CharacterStatesManager statesManager;
    private MeleeAbilityStats meleeAbilityStats;

    void Awake()
    {
        base.Awake();
        statesManager = GetComponentInParent<CharacterStatesManager>();
        meleeAbilityStats = GetComponent<MeleeAbilityStats>();
        meleeEffectCollider.enabled = false;
    }

    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription meleeStateSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.MELEE_ATTACK_CAST_STATE);
        meleeStateSubscription.OnStateChanged += CheckMeleeState;        
    }

    private void CheckMeleeState(object meleeAttack)
    {
        if ((bool)meleeAttack)
        {
            //Enable melee collider
            TriggerAttack();
            Debug.Log("Melee collider enabled");
        }
    }

    public override void TriggerAttack()
    {
        /*
		 * Enable collider for the desired duration of the attack.
		 * Trigger the animation. 
		 */
        base.EnableCollider();
        StartCoroutine(DisableColliderAfterDuration(meleeAbilityStats.GetAbilityDuration()));
    }

    protected override void AfterDisableCollider()
    {
        /*
		 * Do nothing
		 * 
		 */
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player hit by melee");
        }
    }
}
