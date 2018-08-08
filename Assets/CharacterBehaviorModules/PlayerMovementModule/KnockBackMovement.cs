using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackMovement : MonoBehaviour, IKnockbackable {
	private Rigidbody2D characterRigidBody;
    private IMovementHandler characterMovementHandler;
	public float currentDistanceTraveled;
	public float totalTravelDistance;
	public float knockBackSpeed;
	private Vector2 knockBackDirection;
	private bool knockBack;

    private ICharacterStateManager stateManager;
    private CharacterState recoilState;

	// Use this for initialization
	void Awake () {
		characterRigidBody = GetComponentInParent<Rigidbody2D>();
        characterMovementHandler = 
            transform.root.GetComponentInChildren(typeof(IMovementHandler)) as IMovementHandler;
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        if (stateManager.ExistsState(ConstantStrings.RECOIL_STATE))
        {
            recoilState = stateManager.GetExistingCharacterState(ConstantStrings.RECOIL_STATE);
        }
        else
        {
            recoilState = new CharacterState(ConstantStrings.RECOIL_STATE, new bool[] { false, false });
            stateManager.RegisterCharacterState(recoilState.name, recoilState);
        }
	}

    public void KnockbackAngled(Vector2 direction, float knockbackDistance)
    {
        knockBack = true;
        totalTravelDistance = knockbackDistance;
        knockBackDirection = direction;

    }

    public void KnockbackStraight(Vector2 direction, float knockbackDistance, AttackSide attackSide)
    {
        knockBack = true;
        totalTravelDistance = knockbackDistance;
        knockBackDirection = direction;
        if (attackSide == AttackSide.horizontal)
        {
            recoilState.SetState(new bool[] { true, false });
        }
        else if (attackSide == AttackSide.down)
        {
            recoilState.SetState(new bool[] { false, true });
        }
    }

    void Start (){
        /*
         * TODO register the fact that i am being knocked back to cancel the direction movement from controls
         */ 
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (knockBack){
            characterMovementHandler.AddToXPosition(knockBackDirection.x * knockBackSpeed * Time.deltaTime);
            characterMovementHandler.AddToYPosition(knockBackDirection.y * knockBackSpeed * Time.deltaTime);

            currentDistanceTraveled += 
                knockBackSpeed * Time.deltaTime;
			if (currentDistanceTraveled >= totalTravelDistance){
				knockBack = false;
				currentDistanceTraveled = 0;
                recoilState.SetState(new bool[] { false, false });
			}
		}
	}
}
