using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretExplosionDamageSender : MonoBehaviour {

	public float explosionPersistanceTime;
	private float explosionExistanceTime;

	private float damageAmount;
	private CircleCollider2D aoe;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		aoe = GetComponent<CircleCollider2D>();
		explosionExistanceTime = 0;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		explosionExistanceTime += Time.deltaTime;
		if (explosionExistanceTime >= explosionPersistanceTime){
			Destroy(gameObject);
		}
	}

	public void BlowUpTurrets(float explosionDamage, float explosionRange){
		SetExplosionRange(explosionRange);
		SetDamageAmount(explosionDamage);
		//spriteRenderer.size = new Vector2(explosionRange, explosionRange);
		spriteRenderer.enabled = true;
		aoe.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy"){
			collision.gameObject.BroadcastMessage("TakeDamage", damageAmount);
            transform.root.BroadcastMessage("EnemyHit", (bool)collision.gameObject.GetComponent<ICharacterStateObserver>().GetCharacterStateValue(ConstantStrings.DEATH_STATE));
		}

	}

	private void SetExplosionRange (float explosionRange){
		aoe.radius = explosionRange;
	}

	private void SetDamageAmount(float explosionDamage){
		damageAmount = explosionDamage;
	}
}
