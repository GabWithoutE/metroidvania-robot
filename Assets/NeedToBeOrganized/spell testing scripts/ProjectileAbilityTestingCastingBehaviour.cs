using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO:
 * Get the augmentation ability set holder component and subscribe to the moveset change event
 * Add the one off ability, and cast the one off ability on moveset changing
 */ 
public class ProjectileAbilityTestingCastingBehaviour : MonoBehaviour {

//	public GameObject projectilePrefab;

//	public double spell1CurrentCooldown;

//	private double spell1CooldownTime;

//	private CharacterStatesManager2D stateManager;
//	private CharacterStatesManager2D.MutualExclusionBinaryStateWindow directionWindow;
//	private CharacterStatesManager2D.BinaryBehaviorState.BinaryBehaviorStateSubscription firingStateSubscription;

//	public Vector2 upSpawnLocation;
//	public Vector2 upRightSpawnLocation;
//	public Vector2 rightSpawnLocation;
//	public Vector2 downRightSpawnLocation;
//	public Vector2 downSpawnLocation;
//	public Vector2 downLeftSpawnLocation;
//	public Vector2 leftSpawnLocation;
//	public Vector2 upLeftSpawnLocation;

//	private bool registered = false;



//	// Use this for initialization
//	void Start () {
//		stateManager = GetComponentInParent<CharacterStatesManager2D> ();
//		spell1CooldownTime = projectilePrefab.GetComponent<SpellStats> ().GetCooldownTime ();
////		OnSpellProjectileChanged += CastSpell;
//	}
	
//	// Update is called once per frame
//	void Update () {
//		if (!registered) {
//            directionWindow = stateManager.GetMutualExclusionBinaryWindow (WindowNames.Direction);
//			firingStateSubscription = stateManager.GetBinaryBehaviorStateSubcription ("CastingProjectile");
//			firingStateSubscription.OnTrue += CastSpell;
//			registered = true;
//		}
//		UpdateCooldown ();

//	}

//	private void UpdateCooldown() {
//		if (spell1CurrentCooldown >= 0) {
//			spell1CurrentCooldown -= Time.fixedDeltaTime;
//		}
//	}

//	void UpdateSpellRestrictions () {
//		projectilePrefab.GetComponent<SpellStats> ().GetCooldownTime ();
//	}

//	private void ResetCooldownTimer(){
//		spell1CurrentCooldown = spell1CooldownTime;
//	}

//	private void CastOneOff(){
//		// TODO: Spawn the one off spell
//	}

//	void CastSpell(){
//		if (spell1CurrentCooldown >= 0) {
//			return;
//		}
//		string direction = directionWindow.GetVisible ();
//		Vector2 spawnLocation = new Vector2(0,0);
////		print (direction);
	//	switch (direction) {
	//	case ("Up"):
	//		spawnLocation = upSpawnLocation;
	//		break;
	//	case("Dur"):
	//		spawnLocation = upRightSpawnLocation;
	//		break;
	//	case ("Right"):
	//		spawnLocation = rightSpawnLocation;
	//		break;
	//	case("Ddr"):
	//		spawnLocation = downRightSpawnLocation;
	//		break;
	//	case("Down"):
	//		spawnLocation = downSpawnLocation;
	//		break;
	//	case("Ddl"):
	//		spawnLocation = downLeftSpawnLocation;
	//		break;
	//	case("Left"):
	//		spawnLocation = leftSpawnLocation;
	//		break;
	//	case("Dul"):
	//		spawnLocation = upLeftSpawnLocation;
	//		break;
	//	}
	//	GameObject projectile = Instantiate (projectilePrefab, spawnLocation, Quaternion.identity);
	//	projectile.GetComponent<ProjectileStraightMovement> ().SetDirection(spawnLocation);
	//	ResetCooldownTimer ();
	//}
}
