using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerOneOffTurretHealing : MonoBehaviour {
	private SpellStats spellStats;

	public float effectTime = 4;
	public float timeBetweenHeals = 0.1f;
	private float currentTime = 0;
	public float totalHeals = 0;

	private float totalHealingAmount;
	private float healingTurretRange;
	private float amountPerHeal;

	private bool enableHeal = false;

	private void Awake()
	{
		spellStats = GetComponent<SpellStats>();

		totalHealingAmount = spellStats.GetSpellDamage();
		healingTurretRange = spellStats.GetRange();
		GetComponent<CircleCollider2D>().radius = healingTurretRange;
		amountPerHeal = totalHealingAmount / (effectTime / timeBetweenHeals);
	}
	
	// Update is called once per frame
	void Update () {
		if (totalHealingAmount <= 0)
        {
            Destroy(transform.root.gameObject);
        }
		if (currentTime <= 0){
			enableHeal = true;
			currentTime = timeBetweenHeals;
			totalHealingAmount -= amountPerHeal;
			totalHeals += amountPerHeal;
		} else {
			currentTime -= Time.deltaTime;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (enableHeal){
			print(totalHeals);
			if (collision.tag == "Player"){
				collision.BroadcastMessage("RestoreHealthBy", amountPerHeal);
			}
		}
		enableHeal = false;
	}
    
	//private void OnTriggerEnter2D(Collider2D collision)
  //  {
		////print("hello");
    //    if (enableHeal)
    //    {
    //        if (collision.tag == "Player")
    //        {
    //            collision.BroadcastMessage("RestoreHealthBy", amountPerHeal);
    //        }
    //    }
    //}
}
