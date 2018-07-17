using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVelocityState : AbstractCharacterVelocityState
{
    public float margin;
    private float horizontalAxisValue;
    private float tempHorizontal;
    public float freezeDuration;
    private bool hit = false;
    private CharacterState hitState;
    private object prevHealth;
    private float minX;
    private float maxX;
    private RaycastHit2D hitInfoDown;
    private RaycastHit2D hitInfoSide;
    private LayerMask groundMask;
    public float patrolPauseDuration;
    public float patrolTime;
    public float walkTimer;    //Times how long enemy has been walking

    private void Awake()
    {
        base.Awake();
        CharacterState.CharacterStateSubscription groundedSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.GROUNDED);
        statesManager = GetComponentInParent<ICharacterStateManager>();
        hitState = new CharacterState(ConstantStrings.HIT_STATE, hit);
        statesManager.RegisterCharacterState(hitState.name, hitState);
        groundMask = LayerMask.GetMask("Ground");
    }
	// Use this for initialization
	void Start () {
        prevHealth = statesManager.GetCharacterStateValue(ConstantStrings.CURRENT_HEALTH);
        CharacterState.CharacterStateSubscription healthStateSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.CURRENT_HEALTH);
        healthStateSubscription.OnStateChanged += CheckHit;
        horizontalAxisValue = 1;
        walkTimer = 0;
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
            CastRaySide();
		} else
        //If enemy is not standing on the ground, turn on gravity and find out bounds of floor under it
        if (!isGrounded)
        {
            verticalValues = -1;
            CastRayDown();
        }
        //If enemy has reached left bound of floor collider
        if(transform.root.position.x <= minX + margin)
        {
            horizontalAxisValue = 1;
        }
        //If enemy has reached right bound of floor collider
        else if (transform.root.position.x >= maxX - margin)
        {
            horizontalAxisValue = -1;
        }
        if (hit)
        {
            FreezeStart(freezeDuration);
        }
        //Only add to timer if enemy isn't resting
        if (horizontalAxisValue != 0)
        {
            walkTimer += Time.deltaTime;
        }
        //If enemy has been walking for x seconds, freeze for 1 second        
        if (walkTimer >= patrolTime)
        {
            FreezeStart(patrolPauseDuration);
            walkTimer = 0;
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
        StartCoroutine(FreezeDuration(duration));
        hitState.SetState(false);
    }

    IEnumerator FreezeDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        horizontalAxisValue = tempHorizontal;
    }

    //Cast a ray from enemy directly downwards to get the bounds of the floor it is standing on
    private void CastRayDown()
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(0,-1);
        float rayLength = 10;   //Arbitrary ray length, can possibly be shorter
        hitInfoDown = Physics2D.Raycast(startingPosition, direction, rayLength, groundMask);
        if(hitInfoDown)
        {
            minX = hitInfoDown.collider.bounds.min.x;
            maxX = hitInfoDown.collider.bounds.max.x;
        }        
    }

    //Cast a ray in direction of enemy travel to see if there is anything in front of it, if so turn back
    private void CastRaySide()
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(horizontalAxisValue, 0);
        float rayLength = 1;
        hitInfoSide = Physics2D.Raycast(startingPosition, direction, rayLength, groundMask);
        if(hitInfoSide)
        {
            horizontalAxisValue *= -1;
        }
    }
}
