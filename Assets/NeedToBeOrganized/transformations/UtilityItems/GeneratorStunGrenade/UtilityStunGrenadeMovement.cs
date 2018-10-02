using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStunGrenadeMovement : MonoBehaviour {
	private UtilityAbilityStunGrenadeProjectileStats stats;
	private Vector3 projectileDirection;
	private Vector3 initialPosition;
	private void Awake()
	{
		initialPosition = transform.position;

		stats = GetComponent<UtilityAbilityStunGrenadeProjectileStats>();
	}

	public void SetDirection(Vector2 direction){
		projectileDirection = direction;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Abs(Vector3.Distance(transform.position, initialPosition));
		if (distance < stats.GetRange()){
			transform.position = transform.position + projectileDirection * stats.GetProjectileSpeed() * Time.deltaTime;
		}
	}
}
