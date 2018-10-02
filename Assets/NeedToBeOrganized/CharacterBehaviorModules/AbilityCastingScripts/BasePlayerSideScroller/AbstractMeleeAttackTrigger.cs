using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMeleeAttackTrigger : MonoBehaviour {
	protected BoxCollider2D meleeEffectCollider;

	protected void Awake()
	{
		meleeEffectCollider = GetComponent<BoxCollider2D>();
		meleeEffectCollider.enabled = false;
	}

	public abstract void TriggerAttack();

	protected void EnableCollider()
    {
        meleeEffectCollider.enabled = true;
    }

	protected IEnumerator DisableColliderAfterDuration(float colliderDuration)
    {
		yield return new WaitForSeconds(colliderDuration);
        meleeEffectCollider.enabled = false;
		AfterDisableCollider();
    }

	protected abstract void AfterDisableCollider();
}
