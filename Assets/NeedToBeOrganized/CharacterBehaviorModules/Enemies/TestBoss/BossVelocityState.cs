using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVelocityState : AbstractCharacterVelocityState
{
    public float margin;
    public float jumpDuration;
    public float jumpTimer;
    private float minX;
    private float maxX;
    private float verticalValues;
    private float horizontalAxisValue;
    private RaycastHit2D hitInfoDown;
    private RaycastHit2D hitInfoSide;
    private LayerMask groundMask;    
    private CharacterState thrownHammerPosition;
    private CharacterState hammerThrown;
    private Vector2 playerDirection;
    private bool currentlyJumpingUp;
    private bool currentlyFallingDown;
    private bool hammerOnGround;
    private BaseSpeedScaleState speedScale;

    private void Awake()
    {
        base.Awake();
        statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        groundMask = LayerMask.GetMask("Ground");
    }

    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription groundedSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.GROUNDED);
        CharacterState.CharacterStateSubscription hammerHitGroundSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.HAMMER_HITS_GROUND);
        hammerHitGroundSubscription.OnStateChanged += CheckHammerHitGround;
        
        thrownHammerPosition = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.THROWN_HAMMER_POSITION);
        hammerThrown = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN);
        speedScale = transform.parent.GetComponentInChildren<BaseSpeedScaleState>();
        horizontalAxisValue = -1;
        verticalValues = 0;
        currentlyJumpingUp = false;
        currentlyFallingDown = false;
        hammerOnGround = false;
    }
	
	// Update is called once per frame
	void Update () {
        bool isGrounded = ((bool[])statesManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[1];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDirection = player.transform.position - transform.root.position;
        //If boss is currently jumping up, don't change anything
        if(!currentlyJumpingUp)
        {
            //Disable all motion when throwing hammer and before hammer hits the ground
            if ((bool)statesManager.GetCharacterStateValue(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN) && !hammerOnGround)
            {
                //Stand still
                horizontalAxisValue = 0;
                verticalValues = 0;
            }
            else
            {
                //If boss is grounded
                if (isGrounded)
                {
                    //Turn off gravity since it is not needed
                    verticalValues = 0;
                    //If player is on the right
                    if (playerDirection.x > 0)
                    {
                        horizontalAxisValue = 1;
                    }
                    //If player is on the left
                    else if (playerDirection.x < 0)
                    {
                        horizontalAxisValue = -1;
                    }
                    //Otherwise stay put
                    else
                    {
                        horizontalAxisValue = 0;
                    }
                    CastRaySide();
                }
                //If boss is not grounded
                else
                {
                    //Turn on gravity for boss
                    verticalValues = -1;
                    //Look for bounds of floor under boss
                    CastRayDown();
                }

                //If enemy has reached left bound of floor collider, stop
                if (transform.root.position.x <= minX + margin)
                {
                    //If player is on the right
                    if (playerDirection.x > 0)
                    {
                        horizontalAxisValue = 1;
                    }
                    //Otherwise stand on edge of collider and stare at player
                    else
                    {
                        horizontalAxisValue = 0;
                    }
                }
                //If enemy has reached right bound of floor collider, stop
                else if (transform.root.position.x >= maxX - margin)
                {
                    //If player is on the left, move towards player
                    if (playerDirection.x > 0)
                    {
                        horizontalAxisValue = -1;
                    }
                    //Otherwise stand on edge of collider and stare at player
                    else
                    {
                        horizontalAxisValue = 0;
                    }
                }

                //If hammer was thrown and hammer has landed 
                if ((bool)hammerThrown.GetStateValue() && hammerOnGround)
                {
                    //Boss is not currently jumping up or falling down, jump to hammer
                    if (!currentlyFallingDown)
                    {
                        currentlyJumpingUp = true;
                        //Jump up for half the duration of full jump
                        JumpUpStart(jumpDuration / 2);
                    }                                    
                }
            }
        }
        directionState.SetState(new float[] { horizontalAxisValue, verticalValues });        
    }

    //Cast a ray from enemy directly downwards to get the bounds of the floor it is standing on
    private void CastRayDown()
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(0, -1);
        float rayLength = 10;   //Arbitrary ray length, can possibly be shorter
        hitInfoDown = Physics2D.Raycast(startingPosition, direction, rayLength, groundMask);
        if (hitInfoDown)
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
        if (hitInfoSide)
        {
            horizontalAxisValue *= -1;
        }
    }
    
    private void JumpUpStart(float duration)
    {
        //Set values
        Vector2 hammerPosition = (Vector2)statesManager.GetCharacterStateValue(ConstantStrings.Enemy.HammerBoss.THROWN_HAMMER_POSITION);
        speedScale.IncreaseSpeedByFactorOfForTime(Mathf.Abs(transform.root.position.x - hammerPosition.x) / jumpDuration, jumpDuration);
        //If hammer is on boss' right
        if (hammerPosition.x > transform.position.x)
        {
            horizontalAxisValue = 1;
        }
        //If on the left
        else if (hammerPosition.x < transform.position.x)
        {
            horizontalAxisValue = -1;
        }
        //If at same x value
        else
        {
            horizontalAxisValue = 0;
        }
        //Jump up
        verticalValues = 1;        
        //Start ascent
        StartCoroutine(JumpUpDuration(duration));
    }
    IEnumerator JumpUpDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        //Second half of jump, falling down
        currentlyJumpingUp = false;
        currentlyFallingDown = true;
        JumpDownStart(duration);
    }

    private void JumpDownStart(float duration)
    {
        //Turn on gravity for boss and keep same horizontal value
        verticalValues = -1;
        //Start descent
        StartCoroutine(JumpDownDuration(duration));
    }
    IEnumerator JumpDownDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentlyFallingDown = false;
    }

    //If hammer hits the ground, let boss jump to hammer
    private void CheckHammerHitGround(object hammerHitGround)
    {
        if ((bool)hammerHitGround)
        {
            hammerOnGround = true;
        }
        else
        {
            hammerOnGround = false;
        }
    }
}
