using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHammerMovement : MonoBehaviour {
    private BossHammerStateManager bossHammerStateManager;
    private CharacterStatesManager statesManager;
    private CharacterState hammerThrown;
    private CharacterState hammerHitsGround;
    private CharacterState thrownHammerPosition;
    private GameObject hammerThrow;
    private BossCastingBehavior bcb;
    private SpellStats spellStats;
    private bool hammerMoving;
    private bool hammerPickUp;
    private Vector2 playerDirection;
    private Rigidbody2D rgb;
    private ProjectileStraightMovement psm;
    private Vector2 hammerPosition;
    public float throwDistance;

    void Awake()
    {
        statesManager = GetComponentInParent<CharacterStatesManager>();
        bossHammerStateManager = GetComponent<BossHammerStateManager>();
    }

    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription hammerStateSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.HAMMER_THROW_CAST_STATE);
        hammerStateSubscription.OnStateChanged += CheckHammerState;
        CharacterState.CharacterStateSubscription hammerHitPlayerSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.HAMMER_HIT_PLAYER);
        hammerHitPlayerSubscription.OnStateChanged += CheckHammerHitPlayerState;
        CharacterState.CharacterStateSubscription hammerHitGroundSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.HAMMER_HITS_GROUND);
        hammerHitGroundSubscription.OnStateChanged += CheckHammerHitGround;
        CharacterState.CharacterStateSubscription enemyHitHammerSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.Enemy.HammerBoss.HAMMER_HIT_ENEMY);
        enemyHitHammerSubscription.OnStateChanged += CheckEnemyHitHammerState;

        hammerThrown = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN);
        bcb = GetComponentInParent<BossCastingBehavior>();
        spellStats = GetComponent<SpellStats>();
        hammerMoving = false;
        hammerPickUp = true;
        psm = GetComponent<ProjectileStraightMovement>();
        //Get hammer throw prefab
        hammerThrow = bcb.GetAttack(2);
        rgb = GetComponent<Rigidbody2D>();
        hammerPosition = new Vector2(0, 1.5f);        //Change this value to set how high boss holds hammer while walking
    }
    
    void Update()
    {
        //If hammer is not thrown, keep hammer at original position
        if(!(bool)hammerThrown.GetStateValue())
        {
            //If hammer is not moving
            if(!hammerMoving)
            {
                //If boss is allowed to pick up hammer
                if(hammerPickUp)
                {
                    transform.localPosition = hammerPosition;
                }                
            }            
        }
        //If hammer is thrown, keep updating character state
        else
        {
            Vector2 truncatedPosition = transform.position;
            bossHammerStateManager.SetState(ConstantStrings.Enemy.HammerBoss.HAMMER_POSITION, truncatedPosition);
        }
    }

    //If player is hit by moving hammer, deflect hammer down to ground and player take damage
    private void CheckHammerHitPlayerState(object hammerHitPlayer)
    {
        if((bool)hammerHitPlayer)
        {
            if(hammerMoving)
            {
                //Debug.Log("Player hit by hammer");
                Vector2 downVect = new Vector2(0, -1);
                psm.SetDirection(downVect);
            }            
        }
    }

    //If boss hits stationary hammer, pick up hammer
    private void CheckEnemyHitHammerState(object hammerHitEnemy)
    {
        if ((bool)hammerHitEnemy)
        {
            if (!hammerMoving)
            {
                hammerPickUp = true;
                bossHammerStateManager.SetState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN, false);
            }
        }
    }

    //If hammer attack is triggered, throw hammer at player
    private void CheckHammerState(object hammerAttack)
    {
        if ((bool)hammerAttack)
        {
            if(!(bool)hammerThrown.GetStateValue())
            {
                hammerPickUp = false;
                bossHammerStateManager.SetState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN, true);
                //Find direction player is in
                GameObject player = GameObject.FindGameObjectWithTag("Player");                
                Vector2 playerFootDirection = CastRayDown(player.transform);
                playerDirection = player.transform.position - transform.position;
                Vector2 hammerThrowDirection;
                //If player is to the left of boss
                if (playerDirection.x < 0)
                {
                    //Throw hammer to the left the distance set in inspector
                    hammerThrowDirection = new Vector2(-1 * throwDistance, transform.root.GetComponent<BoxCollider2D>().bounds.min.y);
                }
                //If player is to the right of boss
                else if(playerDirection.x > 0)
                {
                    //Throw hammer to the right the distance set in inspector
                    hammerThrowDirection = new Vector2(throwDistance, transform.root.GetComponent<BoxCollider2D>().bounds.min.y);
                }
                else
                {
                    //Otherwise throw the hammer straight down
                    hammerThrowDirection = new Vector2(0, transform.root.GetComponent<BoxCollider2D>().bounds.min.y);
                }
                //hammerThrowDirection = new Vector2(playerDirection.x, transform.root.GetComponent<BoxCollider2D>().bounds.min.y);
                psm.SetDirection(hammerThrowDirection.normalized);
                hammerMoving = true;
            }            
        }
    }

    //If hammer hits the ground, stop the hammer
    private void CheckHammerHitGround(object hammerHitGround)
    {
        if((bool)hammerHitGround)
        {
            Vector2 zeroVect = new Vector2(0, 0);
            psm.SetDirection(zeroVect);
            hammerMoving = false;
            Vector2 tempSetPosition = transform.position;
            bossHammerStateManager.SetState(ConstantStrings.Enemy.HammerBoss.THROWN_HAMMER_POSITION, tempSetPosition);
        }
    }

    //Cast a ray from player directly downwards to the ground to find position to aim hammer
    private Vector2 CastRayDown(Transform transform)
    {
        LayerMask groundMask;
        RaycastHit2D hitInfoDown;
        groundMask = LayerMask.GetMask("Ground");
        Vector2 startingPosition = transform.position;
        Vector2 direction = new Vector2(0, -1);
        float rayLength = 10;   //Arbitrary ray length, can possibly be shorter
        hitInfoDown = Physics2D.Raycast(startingPosition, direction, rayLength, groundMask);
        if (hitInfoDown)
        {
            return hitInfoDown.point;
        }
        //If need be, throw hammer directly at the ground
        Vector2 down = new Vector2(0, -1);
        return down;
    }
}
