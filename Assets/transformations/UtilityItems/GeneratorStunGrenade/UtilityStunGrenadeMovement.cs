using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStunGrenadeMovement : MonoBehaviour {
	private UtilityAbilityStunGrenadeProjectileStats stats;
	private Vector3 projectileDirection;

	private void Awake()
	{
		stats = GetComponent<UtilityAbilityStunGrenadeProjectileStats>();
	}

	public void SetDirection(Vector2 direction){
		projectileDirection = direction;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + projectileDirection * stats.GetProjectileSpeed() * Time.deltaTime;
	}
}
