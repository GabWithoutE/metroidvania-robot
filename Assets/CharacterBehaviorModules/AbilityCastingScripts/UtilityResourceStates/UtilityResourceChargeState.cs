using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityResourceChargeState : MonoBehaviour {
	private ICharacterStateManager stateManager;
	private CharacterState resourceChargeState;
	private UtilityAbilityStunGrenadeItemStats stats;
	private float resourceAmount;
	private float resourceDecreaseRate; // per sec
	private float resourceChargeRate; // per sec of movement
	private float resourceChargeHitRate; // per hit

	public float displayResourceAmount;

	private void Awake()
	{
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
		stats = GetComponent<UtilityAbilityStunGrenadeItemStats>();
		resourceAmount = 0f;
		resourceDecreaseRate = stats.GetResourceDechargeRate();
        resourceChargeRate = stats.GetResourceChargeRate();
        resourceChargeHitRate = stats.GetResourceChargePerHit();

		if (!stateManager.ExistsState(ConstantStrings.UTILITY_RESOURCE_STATE)){
			stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
            resourceChargeState = new CharacterState(ConstantStrings.UTILITY_RESOURCE_STATE, resourceAmount);
            stateManager.RegisterCharacterState(resourceChargeState.name, resourceChargeState);
		} else {
			resourceChargeState = stateManager.GetExistingCharacterState(ConstantStrings.UTILITY_RESOURCE_STATE);
		}

      
	}

	// Update is called once per frame
	void Update () {
		      
		float[] playerVelocity = (float[])stateManager.GetCharacterStateValue(ConstantStrings.VELOCITY);
		if (Mathf.Abs(playerVelocity[0]) > 0 && playerVelocity[1] == 0){
			resourceAmount += Time.deltaTime * resourceChargeRate;
		} else if (resourceAmount > 0f){
            resourceAmount -= Time.deltaTime * resourceDecreaseRate;
        }
		resourceAmount = Mathf.Clamp(resourceAmount, 0f, 1f);

		resourceChargeState.SetState(resourceAmount);
		displayResourceAmount = resourceAmount;
	}
}
