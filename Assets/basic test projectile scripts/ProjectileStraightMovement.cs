using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellStats))]
public class ProjectileStraightMovement : MonoBehaviour {

	private SpellStats spellStats;
	public float projectileSpeed;
	private Vector2 direction;


	// Use this for initialization
	void Start () {
		spellStats = GetComponent<SpellStats> ();
		projectileSpeed = spellStats.GetProjectileSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (direction * spellStats.GetProjectileSpeed());
	}
		

	public void SetDirection(Vector2 projectileDirection) {
		direction = projectileDirection;
	}
}
