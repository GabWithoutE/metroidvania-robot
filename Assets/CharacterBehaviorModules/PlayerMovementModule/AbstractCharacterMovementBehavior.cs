using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterMovementBehavior : MonoBehaviour {
	protected ICharacterStateObserver stateObserver;
	//protected Rigidbody2D characterRigidbody;
	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		//characterRigidbody = GetComponentInParent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//characterRigidbody.position +=
		//(Vector2)stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY)
		//* Time.deltaTime;
		Vector2 velocity = (Vector2)stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
		transform.root.transform.position += 
			new Vector3(velocity.x, velocity.y, transform.root.transform.position.z)
            * Time.deltaTime
			* (float)stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE);
	}
}
