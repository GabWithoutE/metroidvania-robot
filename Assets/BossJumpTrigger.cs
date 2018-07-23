using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpTrigger : MonoBehaviour {
    private CharacterStatesManager statesManager;
    private GameObject player;
    private Vector2 playerDirection;
    private Rigidbody2D rgb;
    private Vector2 jumpDirection;
    private bool ifJump;

    // Use this for initialization
    void Start () {
        statesManager = GetComponentInParent<CharacterStatesManager>();
        CharacterState.CharacterStateSubscription jumpStateSubscription = statesManager.GetCharacterStateSubscription("jumpState");
        //jumpStateSubscription.OnStateChanged += CheckJumpState;
        rgb = transform.root.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDirection = transform.root.position - player.transform.position;
        jumpDirection = new Vector2(0, 0);
        ifJump = false;
    }	
}
