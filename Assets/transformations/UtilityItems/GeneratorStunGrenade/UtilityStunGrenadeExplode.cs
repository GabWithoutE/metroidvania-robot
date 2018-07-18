using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStunGrenadeExplode : MonoBehaviour {
	public GameObject utilityStunGrenadeExplosion;
	private Vector3 initialPosition;
	private UtilityAbilityStunGrenadeProjectileStats stats;
	private CircleCollider2D explosionArea;

	// Use this for initialization
	private void Awake()
	{
		initialPosition = transform.position;
		stats = GetComponent<UtilityAbilityStunGrenadeProjectileStats>();
		explosionArea = GetComponent<CircleCollider2D>();
		explosionArea.enabled = false;
	}
    

	// Update is called once per frame
	void Update () {
		float distance = Mathf.Abs(Vector3.Distance(transform.position, initialPosition));
		if (distance >= stats.GetRange())
        {
			explosionArea.enabled = true;
			StartCoroutine(DestroyAfterTime(stats.GetExplosionPersistTime()));
        }
	}

	IEnumerator DestroyAfterTime(float time){
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		/*
		 * Stun it
		 */ 
	}
}
