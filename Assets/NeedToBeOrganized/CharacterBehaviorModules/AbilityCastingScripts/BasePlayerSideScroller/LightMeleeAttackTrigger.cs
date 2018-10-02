using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackSide
{
    horizontal,
    up,
    down
}
public class LightMeleeAttackTrigger : AbstractMeleeAttackTrigger
{


    public AttackSide attackSide;
	private MeleeAbilityStats meleeAbilityStats;
	private IKnockbackable playerKnockBackControl;
	public Vector2 knockBackStraightDirection;
    public float knockbackPlayerAmount;

    private bool hitAvailable;


    private void Awake()
    {
        base.Awake();
        playerKnockBackControl = transform.root.GetComponentInChildren(typeof(IKnockbackable)) as IKnockbackable;
        meleeAbilityStats = GetComponent<MeleeAbilityStats>();
        meleeEffectCollider.enabled = false;
 
    }

	public void SetKnockBackDirection(Vector2 knockbackDirection){
        knockBackStraightDirection = knockbackDirection;
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
		IDamageable damageable = 
			(IDamageable)collision.transform.root.GetComponentInChildren(typeof(IDamageable)) as IDamageable;
		IKnockbackable knockbackable = 
			(IKnockbackable)collision.transform.root.GetComponentInChildren(typeof(IKnockbackable)) as IKnockbackable;
		if (damageable != null){
			if (collision.tag == "Enemy")
            {
                float baseDamage = meleeAbilityStats.GetDamage();
				damageable.TakeDamage(baseDamage);
            }
		}
		if (knockbackable != null){
			if (collision.tag == "Enemy"){
				
			}
		}
        /* 
         * Player recoil
         */
        if (attackSide == AttackSide.horizontal){
            if (collision.tag == "Ground")
            {

                playerKnockBackControl.KnockbackStraight(-knockBackStraightDirection, knockbackPlayerAmount, AttackSide.horizontal);
                //print(-knockBackStraightDirection);
            }
        }
        if (attackSide == AttackSide.down){
            //if (collision.tag == "Ground"){
            //    playerKnockBackControl.KnockbackStraight(-knockBackStraightDirection, knockbackPlayerAmount, AttackSide.down);

            //}
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
