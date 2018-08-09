using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackTrigger : AbstractMeleeAttackTrigger {
	private Vector2 colliderOriginalSize;
	private HeavyMeleeAbilityStats heavyMeleeAbilityStats;
    private ICharacterStateManager stateManager;
    private float maxXScaleDiff;
	private float maxYScaleDiff;
	private float maxDamageScale;
	private float currentDamageScale = 1;
	private float maxChargeTime;
	private float currentChargeTime = 0;
    private FlyingKickMovePlayer flyingKick;
    private float kickDistance;
    
	// Use this for initialization
	private void Awake () {
		base.Awake();
		heavyMeleeAbilityStats = GetComponent<HeavyMeleeAbilityStats>();
		colliderOriginalSize = meleeEffectCollider.size;
        
		maxChargeTime = heavyMeleeAbilityStats.GetHeavyAttackChargeTime();
		maxDamageScale = heavyMeleeAbilityStats.GetDamageMaxScaleFactor();
		maxXScaleDiff = heavyMeleeAbilityStats.GetXMaxScaleFactor() - 1;
		maxYScaleDiff = heavyMeleeAbilityStats.GetYMaxScaleFactor() - 1;
        kickDistance = 1f;
	}

    void Start()
    {
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        flyingKick = GetComponent<FlyingKickMovePlayer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
	public override void TriggerAttack(){
		// Casts and persists for duration then change the scale values back
		base.EnableCollider();
        StartCoroutine(DisableColliderAfterDuration(heavyMeleeAbilityStats.GetAbilityDuration()));
        //If player is in the air, do a flying kick
        if (!((bool[])stateManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[1])
        {
            //Debug.Log("Not Grounded");
            flyingKick.FlyingKickDistance(kickDistance);
        }		
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            float baseDamage = heavyMeleeAbilityStats.GetDamage();
			collision.BroadcastMessage("TakeDamage", baseDamage * currentDamageScale);
        }
    }

	public void ScaleHeavyAttackByHolding(float timeCharged)
    {
		currentChargeTime += timeCharged;
		currentChargeTime = Mathf.Clamp(currentChargeTime, 0, maxChargeTime);
		float portionCharged = currentChargeTime / maxChargeTime;

		meleeEffectCollider.size = 
			new Vector2(colliderOriginalSize.x + (maxXScaleDiff * portionCharged), 
			            colliderOriginalSize.y + (maxYScaleDiff * portionCharged));

		currentDamageScale =
			currentDamageScale * maxDamageScale * portionCharged;
	}

	protected override void AfterDisableCollider()
	{
		currentChargeTime = 0;
        currentDamageScale = 1;
        meleeEffectCollider.size = colliderOriginalSize;
	}

}
