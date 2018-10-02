using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpTurretsForDamage : MonoBehaviour {
	public GameObject turretExplosions;
	private SpellStats spellStats;

    /*
     * Eventually make this a coroutine that waits for a response from the turret that it has finished it's death.
     * 
     */ 
	public void BlowTurretsUpForDamage(Queue<GameObject> turrets){
		while (turrets.Count != 0){
			GameObject turret = turrets.Dequeue();
			Vector3 turretlocationRelativeToPlayer = turret.transform.localPosition;
			GameObject explosion = Instantiate(turretExplosions, turretlocationRelativeToPlayer, Quaternion.identity, transform);
			explosion.GetComponent<TurretExplosionDamageSender>().BlowUpTurrets(spellStats.GetSpellDamage(), spellStats.GetRange());
			Destroy(turret);
		}
	}

	private void Awake()
	{
		spellStats = GetComponent<SpellStats>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
