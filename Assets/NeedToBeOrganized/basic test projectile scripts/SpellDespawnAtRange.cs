using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpellStats))]
public class SpellDespawnAtRange: MonoBehaviour {

	private SpellStats spellStats;
	private float despawnRange;
	private Vector2 spellSpawnLocation;

	private void Awake()
	{
		spellSpawnLocation = transform.position;
		spellStats = GetComponent<SpellStats>();
        despawnRange = spellStats.GetRange();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (spellSpawnLocation, transform.position) >= despawnRange) {
			Destroy (gameObject);
		}
	}
}
