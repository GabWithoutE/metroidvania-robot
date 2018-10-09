using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterMovementBehavior : MonoBehaviour {
	protected ICharacterStateObserver stateObserver;
    protected IMovementHandler movementHandler;
	protected Rigidbody2D characterRigidbody;
	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		characterRigidbody = GetComponentInParent<Rigidbody2D>();
        movementHandler = transform.root.GetComponentInChildren(typeof(IMovementHandler)) as IMovementHandler;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float[] velocity = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
		float[] speedScaleValues = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE);
		float runSpeedScaleValue = speedScaleValues[0];

		characterRigidbody.position +=
			                  new Vector2(velocity[0] * runSpeedScaleValue, 0) * Time.deltaTime;
	}
}
