using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVelocityState : AbstractCharacterVelocityState
{
    public float margin;
    public float jumpDuration;
    public float jumpTimer;    
    private RaycastHit2D hitInfoDown;
    private RaycastHit2D hitInfoSide;
    private LayerMask groundMask;
    private float minX;
    private float maxX;
    private CharacterState thrownHammerPosition;
    private CharacterState hammerThrown;
    private Vector2 playerDirection;
    private bool isJumping;
    private float verticalValues;
    private float horizontalAxisValue;
    private bool firstTime;

    private BaseSpeedScaleState speedScale;

    private void Awake()
    {
        base.Awake();
        statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        groundMask = LayerMask.GetMask("Ground");
        jumpTimer = jumpDuration;
    }

    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription groundedSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.GROUNDED);
        CharacterState.CharacterStateSubscription jumpStateSubscription = statesManager.GetCharacterStateSubscription("jumpState");
        //jumpStateSubscription.OnStateChanged += CheckJumpState;
        thrownHammerPosition = statesManager.GetExistingCharacterState("thrownHammerPosition");
        hammerThrown = statesManager.GetExistingCharacterState("hammerThrown");
        speedScale = transform.parent.GetComponentInChildren<BaseSpeedScaleState>();
        horizontalAxisValue = -1;
        verticalValues = 0;
        firstTime = true;
    }
	
	// Update is called once per frame
	void Update () {
        bool isGrounded = ((bool[])statesManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[1];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDirection = player.transform.position - transform.root.position;
        //Disable all motion when throwing hammer and before hammer hits the ground
        if((bool)statesManager.GetCharacterStateValue("hammerThrown") && !(bool)statesManager.GetCharacterStateValue("hammerHitsGround"))
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
                jumpTimer = 0;
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
                //Otherwise stay
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
                else
                {
                    horizontalAxisValue = 0;
                }
            }
            //If enemy has reached right bound of floor collider, stop
            else if (transform.root.position.x >= maxX - margin)
            {
                //If player is on the left
                if (playerDirection.x > 0)
                {
                    horizontalAxisValue = -1;
                }
                else
                {
                    horizontalAxisValue = 0;
                }
            }

            //If hammer was thrown, jump to hammer
            if ((bool)hammerThrown.GetStateValue())
            {
                horizontalAxisValue = 0;
                Vector2 hammerPosition = (Vector2)statesManager.GetCharacterStateValue("thrownHammerPosition");
                if (firstTime)
                {
                    //Debug.Log(Mathf.Abs(transform.root.position.x - hammerPosition.x));
                    speedScale.IncreaseSpeedByFactorOfForTime(Mathf.Abs(transform.root.position.x - hammerPosition.x) / jumpDuration, jumpDuration);
                    firstTime = false;
                }
                if (jumpTimer < jumpDuration)
                {
                    //If hammer is on boss' right
                    if (hammerPosition.x > transform.position.x)
                    {
                        horizontalAxisValue = 1;
                    }
                    else if (hammerPosition.x < transform.position.x)
                    {
                        horizontalAxisValue = -1;
                    }
                    else
                    {
                        horizontalAxisValue = 0;
                    }
                    //If on ascending part of jump
                    if (jumpTimer <= (jumpDuration / 2))
                    {
                        verticalValues = 1;
                    }
                    //If on descending part of jump
                    else
                    {
                        verticalValues = -1;
                    }
                    jumpTimer += Time.deltaTime;
                }
                else
                {
                    jumpTimer = 0;
                }
                //BossJump(jumpDuration, hammerPosition);
            }
            else
            {
                firstTime = true;
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
    
    private void JumpStart(float duration)
    {
        StartCoroutine(JumpDuration(duration));
    }

    IEnumerator JumpDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        //inTheAirState.SetState(false);
    }
    /*    
    private void CheckJumpState(object jumpState)
    {
        if (!(bool)jumpState)
        {
            jumpTimer = 0;
            //firstTime = true;
            //Set isJumping to true if subscription changes to true
            //isJumping = true;      
        }
    }
    */
}
