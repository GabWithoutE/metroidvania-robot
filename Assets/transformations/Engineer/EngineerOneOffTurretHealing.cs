using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerOneOffTurretHealing : MonoBehaviour {
	private SpellStats spellStats;
	private CircleCollider2D collider2D;

	public float effectTime = 4;
	public float timeBetweenHeals = 0.1f;
	private float currentTime = 0;
	public float totalHeals = 0;
	public float delayBeforeStart = 0.5f;

	private float totalHealingAmount;
	private float healingTurretRange;
	private float amountPerHeal;

	//private bool enableHeal = false;

	private void Awake()
	{
		spellStats = GetComponent<SpellStats>();
		collider2D = GetComponent<CircleCollider2D>();

		totalHealingAmount = spellStats.GetSpellDamage();
		healingTurretRange = spellStats.GetRange();
		GetComponent<CircleCollider2D>().radius = healingTurretRange;
		amountPerHeal = totalHealingAmount / (effectTime / timeBetweenHeals);
	}
	
	// Update is called once per frame
	void Update () {
		if (delayBeforeStart >= 0){
			delayBeforeStart -= Time.deltaTime;
			return;
		}
		if (totalHealingAmount <= 0)
        {
            Destroy(transform.root.gameObject);
        }
		if (currentTime <= 0){
			collider2D.enabled = true;
			currentTime = timeBetweenHeals;
			totalHealingAmount -= amountPerHeal;
			totalHeals += amountPerHeal;
		} else {
			currentTime -= Time.deltaTime;
			collider2D.enabled = false;
		}
	}

	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//	if (enableHeal){
	//		if (collision.tag == "Player"){
	//			collision.BroadcastMessage("RestoreHealthBy", amountPerHeal);
	//		}
	//	}
	//	enableHeal = false;
	//}
    
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.BroadcastMessage("RestoreHealthBy", amountPerHeal);
        }
    }
}
