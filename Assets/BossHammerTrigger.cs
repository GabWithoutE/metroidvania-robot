using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHammerTrigger : MonoBehaviour {
    private CharacterStatesManager statesManager;
    private CharacterState hammerThrown;
    private CharacterState hammerHitsGround;
    private GameObject hammerThrow;
    private BossCastingBehavior bcb;
    private SpellStats spellStats;
    private bool hammerMoving;
    private Vector2 playerDirection;
    private Rigidbody2D rgb;
    private ProjectileStraightMovement psm;
    private Vector2 hammerPosition;
    private CharacterState thrownHammerPosition;
    LayerMask groundMask;
    

    void Awake()
    {
        statesManager = GetComponentInParent<CharacterStatesManager>();
        //thrownHammerPosition = new CharacterState("thrownHammerPosition", new Vector2(0, 0));
        thrownHammerPosition = statesManager.GetExistingCharacterState("thrownHammerPosition");
        //statesManager.RegisterCharacterState(thrownHammerPosition.name, thrownHammerPosition);
        hammerThrown = new CharacterState("hammerThrown", false);
        statesManager.RegisterCharacterState(hammerThrown.name, hammerThrown);
        hammerHitsGround = new CharacterState("hammerHitsGround", false);
        statesManager.RegisterCharacterState(hammerHitsGround.name, hammerHitsGround);
    }

    // Use this for initialization
    void Start () {
        CharacterState.CharacterStateSubscription hammerStateSubscription = statesManager.GetCharacterStateSubscription("hammerThrowCastState");
        hammerStateSubscription.OnStateChanged += CheckHammerState;           
        bcb = GetComponentInParent<BossCastingBehavior>();
        spellStats = GetComponent<SpellStats>();
        hammerMoving = false;
        psm = GetComponent<ProjectileStraightMovement>();
        //Get hammer throw prefab
        hammerThrow = bcb.GetAttack(2);
        rgb = GetComponent<Rigidbody2D>();
        hammerPosition = new Vector2(0, 1.5f);
        groundMask = LayerMask.GetMask("Ground");
    }
    
    void Update()
    {
        //If hammer is not thrown, keep hammer at original position
        if(!(bool)hammerThrown.GetStateValue())
        {
            //If hammer is not moving
            if(!hammerMoving)
            {
                transform.localPosition = hammerPosition;
            }            
        }
    }

    private void CheckHammerState(object hammerAttack)
    {
        if ((bool)hammerAttack)
        {
            if(!(bool)hammerThrown.GetStateValue())
            {
                hammerThrown.SetState(true);
                //Find direction player is in
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector2 playerFootDirection = CastRayDown(player.transform);
                playerDirection = player.transform.position - transform.position;
                Debug.Log(transform.position);
                Debug.Log(playerDirection);
                Vector2 hammerThrowDirection = new Vector2(playerDirection.x, transform.root.GetComponent<BoxCollider2D>().bounds.min.y);
                Debug.Log(hammerThrowDirection);
                psm.SetDirection(hammerThrowDirection.normalized);
                hammerMoving = true;
            }            
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(hammerMoving)
            {
                //Debug.Log("Player hit by hammer");
                Vector2 downVect = new Vector2(0, -1);
                psm.SetDirection(downVect);
            }            
        }
        else if(collision.gameObject.layer == 9)
        {
            Vector2 zeroVect = new Vector2(0, 0);
            psm.SetDirection(zeroVect);
            hammerMoving = false;
            Vector2 tempSetPosition = transform.position;
            thrownHammerPosition.SetState(tempSetPosition);
            hammerHitsGround.SetState(true);
        }
        //If boss comes within range of hammer
        else if (collision.gameObject.tag == "Enemy")
        {
            //And hammer is thrown
            if ((bool)hammerThrown.GetStateValue())
            {
                //And hammer is moving
                if(!hammerMoving)
                {
                    hammerThrown.SetState(false);
                    hammerHitsGround.SetState(false);
                }                
            }
        }
    }
}
