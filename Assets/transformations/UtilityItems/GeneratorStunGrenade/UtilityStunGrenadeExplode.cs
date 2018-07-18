using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStunGrenadeExplode : MonoBehaviour {
	public GameObject utilityStunGrenadeExplosion;
	private Vector3 initialPosition;
	private UtilityAbilityStunGrenadeProjectileStats stats;

	// Use this for initialization
	private void Awake()
	{
		initialPosition = transform.position;
		stats = GetComponent<UtilityAbilityStunGrenadeProjectileStats>();

	}
    

	// Update is called once per frame
	void Update () {
		float distance = Mathf.Abs(Vector3.Distance(transform.position, initialPosition));
		if (distance >= stats.GetRange())
        {
            Destroy(gameObject);
        }
	}
}
