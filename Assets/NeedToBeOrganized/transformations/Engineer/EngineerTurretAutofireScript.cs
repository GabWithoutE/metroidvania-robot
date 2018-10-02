using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerTurretAutofireScript : MonoBehaviour {

	private SpellStats spellStats;
	private CircleCollider2D collider;

	public float turretAutofireCooldown = 2;
	public GameObject turretProjectileSmall;
	public GameObject turretProjectileLarge;
	private int currentShotNumber;

	private float turretAutofireCurrentCooldown;

	/*
     * Remember to move the turret's transform to 0.
     * 
     */

	private void Awake()
	{
		spellStats = GetComponent<SpellStats>();
		collider = GetComponent<CircleCollider2D>();
		collider.radius = spellStats.GetRange();
		collider.enabled = false;
		currentShotNumber = 0;
		turretAutofireCurrentCooldown = 0.5f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (turretAutofireCurrentCooldown <= 0) {
			if (collision.gameObject.tag == "Enemy"){
				Vector3 directionUnNormalized = collision.gameObject.transform.position - transform.position;
                
				Vector2 enemyDirection = new Vector2(directionUnNormalized.x,directionUnNormalized.y);
				enemyDirection.Normalize();

				currentShotNumber++;
                if (currentShotNumber == 5) {
					// Instantiate a big shot shot.
					GameObject turretProjLargeInstance = Instantiate(turretProjectileLarge, transform);
					turretProjLargeInstance.GetComponent<ProjectileStraightMovement>().SetDirection(enemyDirection);
                    currentShotNumber = 0;
            		turretAutofireCurrentCooldown = turretAutofireCooldown;
                } else {
                    // Instantiate a small shot
					GameObject turretProjectileSmallInstance = Instantiate(turretProjectileSmall, transform);
					turretProjectileSmallInstance.GetComponent<ProjectileStraightMovement>().SetDirection(enemyDirection);
            		turretAutofireCurrentCooldown = turretAutofireCooldown;
                }
            }
        }

	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		DecrementCooldown();
		//print(turretAutofireCurrentCooldown);
		EnableAndDisableCollider();
	}

	private void EnableAndDisableCollider(){
		if (turretAutofireCurrentCooldown >= 0 && collider.enabled){
			collider.enabled = false;
		} 
		else if (turretAutofireCurrentCooldown <= 0 && !collider.enabled) {
			collider.enabled = true;
		}
	}

	private void DecrementCooldown (){
		if (turretAutofireCurrentCooldown >=0) {
			turretAutofireCurrentCooldown -= Time.fixedDeltaTime;
		}
	}
}
