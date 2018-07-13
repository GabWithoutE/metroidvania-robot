using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMeleeAttackTrigger : AbstractMeleeAttackTrigger
{
	private MeleeAbilityStats meleeAbilityStats;

	private void Awake()
	{
		base.Awake();
		meleeAbilityStats = GetComponent<MeleeAbilityStats>();
		meleeEffectCollider.enabled = false;

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
    

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy"){
			float baseDamage = meleeAbilityStats.GetDamage();

		}
	}

	protected override void AfterDisableCollider()
	{
		/*
		 * Do nothing
		 * 
		 */ 
	}

}
