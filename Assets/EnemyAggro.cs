using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour {
    public float chaseSpeed;
    public float chaseTime;
    private float originalChaseTime;
    public bool initiated;
    public bool dropped;
    public bool detected = false;
    public Vector2 playerDirection;
    private bool timerRunning = false;
    private GameObject aggroInit;
    private GameObject aggroDrop;
    private AggroInit initScript;
    private AggroDrop dropScript;
    private GameObject player;

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        initiated = false;
        dropped = false;
        originalChaseTime = chaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Reset timer
        timerRunning = true;        
        aggroInit = GameObject.Find("AggroInit");
        aggroDrop = GameObject.Find("AggroDrop");
        initScript = aggroInit.GetComponent<AggroInit>();
        dropScript = aggroDrop.GetComponent<AggroDrop>();        
        //Update initiated and dropped variables
        initiated = initScript.initiated;
        dropped = dropScript.dropped;
        Debug.Log(initiated);
        //Debug.Log(dropped);
        //If both initiated and dropped are true, player is no longer spotted but enemy still chases player
        if (initiated && dropped)
        {
            //Set detected to true
            detected = true;            
            //Chase player for certain amount of time
            if (timerRunning)
            {
                chaseTime -= Time.smoothDeltaTime;
                if (chaseTime >= 0)
                {
                    //Chase player
                    ChasePlayer();
                }
                else
                {        
                    //Once timer runs out, player is no longer detected                    
                    detected = false;
                    initiated = false;
                    initScript.initiated = false;
                    timerRunning = false;
                }
            }
        }
        //If only initiated is true, player is still spotted
        else if(initiated && !dropped)
        {
            chaseTime = originalChaseTime;
            //Set detected to true
            detected = true;
            //Enemy chases player
            ChasePlayer();
        }
        //If only dropped is true, player is not spotted and enemy stops chasing
        else if(!initiated && dropped)
        {
            chaseTime = originalChaseTime;
            //Set initiated to false
            initiated = false;
            initScript.initiated = false;
            //Set detected to false
            detected = false;
        }
    }

    void ChasePlayer()
    {
        //Get the player
        player = GameObject.FindGameObjectWithTag("Player");
        //Set player direction variable
        playerDirection = player.transform.position - transform.position;
        //Make enemy look at player
        transform.parent.parent.LookAt(player.transform);
        //Move enemy forward, since enemy is looking at player from previous line, moving enemy forward
        //results in movement towards player
        transform.parent.parent.position += transform.parent.parent.forward * chaseSpeed * Time.deltaTime;
    }
}
