using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVelocityState : AbstractCharacterDirectionState
{
    private float horizontalAxisValue;
    private float tempHorizontal;
    public float freezeDuration;
    public bool hit = false;
    private CharacterState hitState;
    private object prevHealth;

    private void Awake()
    {
        base.Awake();
        CharacterState.CharacterStateSubscription groundedSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.GROUNDED);
        statesManager = GetComponentInParent<ICharacterStateManager>();
        hitState = new CharacterState(ConstantStrings.HIT_STATE, hit);
        statesManager.RegisterCharacterState(hitState.name, hitState);
    }
	// Use this for initialization
	void Start () {
        prevHealth = statesManager.GetCharacterStateValue(ConstantStrings.CURRENT_HEALTH);
        CharacterState.CharacterStateSubscription healthStateSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.CURRENT_HEALTH);
        healthStateSubscription.OnStateChanged += CheckHit;
        horizontalAxisValue = 1;
    }
	
	// Update is called once per frame
	void Update () {        
        float verticalValues = 0;        
        bool isGrounded = ((bool[])statesManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[1];
        hit = (bool)statesManager.GetCharacterStateValue(ConstantStrings.HIT_STATE);
        //If enemy is standing on the ground, turn off gravity
        if (isGrounded)
        {
            verticalValues = 0;
        }
        //If enemy is not standing on the ground, turn on gravity
        if (!isGrounded)
        {
            verticalValues = -1;
        }      
        if(hit)
        {
            FreezeStart(freezeDuration);           
        }
        directionState.SetState(new float[] { horizontalAxisValue, verticalValues });
    }

    private void CheckHit(object currentHealth)
    {
        //If health decreased, set hitState to true
        if ((float)prevHealth > (float)currentHealth)
        {
            hitState.SetState(true);
            prevHealth = currentHealth;
        }
    }

    private void FreezeStart(float duration)
    {
        tempHorizontal = horizontalAxisValue;
        horizontalAxisValue = 0;
        //Debug.Log("Freeze started");
        StartCoroutine(FreezeDuration(duration));
        hitState.SetState(false);
    }

    IEnumerator FreezeDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        //Debug.Log("Freeze ended");
        horizontalAxisValue = tempHorizontal;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "PatrolBound")
        {
            horizontalAxisValue *= -1;
            //Debug.Log("Bounds Hit");
        }        
    }
}
