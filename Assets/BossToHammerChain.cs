using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossToHammerChain : MonoBehaviour
{
    private BossHammerStateManager bossHammerStateManager;
    private ICharacterStateManager statesManager;
    private CharacterState hammerThrown;
    private CharacterState hammerPositionState;
    private SpriteRenderer chainSpriteRenderer;
    private float defaultXScale;
    private float defaultYScale;
    private float prevAngle;
    private float nextAngle;

    // Use this for initialization
    void Start () {
        statesManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        bossHammerStateManager = GetComponentInParent<BossHammerStateManager>();
        hammerThrown = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_THROWN);
        hammerPositionState = statesManager.GetExistingCharacterState(ConstantStrings.Enemy.HammerBoss.HAMMER_POSITION);
        chainSpriteRenderer = GetComponent<SpriteRenderer>();
        chainSpriteRenderer.drawMode = SpriteDrawMode.Tiled;
        //Remember original chain scale
        defaultXScale = chainSpriteRenderer.size.x;
        defaultYScale = chainSpriteRenderer.size.y;
        prevAngle = 0;
        nextAngle = 0;
    }
	
	// Update is called once per frame
	void Update () {        
        //Anchor one end to boss.transform.position
        transform.position = transform.root.position;
        //If hammer is thrown
        if((bool)hammerThrown.GetStateValue())
        {
            GenerateChain();
        }
        //If hammer is not thrown yet, set scale to default scale and set chain angle to 0
        else
        {
            chainSpriteRenderer.size.Set(defaultXScale, defaultYScale);
            transform.rotation = Quaternion.identity;
            prevAngle = 0;
            nextAngle = 0;            
        }            
	}

    private void GenerateChain()
    {
        Vector2 hammerPosition = (Vector2)statesManager.GetCharacterStateValue(ConstantStrings.Enemy.HammerBoss.HAMMER_POSITION);
        //Calculate vector from boss to hammer
        Vector2 bossPosition = transform.position;
        Vector2 bossToHammer = hammerPosition - bossPosition;
        nextAngle = Mathf.Atan2(bossToHammer.y, bossToHammer.x) * Mathf.Rad2Deg;
        float distanceToHammer = bossToHammer.magnitude;
        //Point chain in the direction of the hammer
        //Rotate Z rotation to point at hammer
        transform.Rotate(0, 0, nextAngle - prevAngle, 0);
        prevAngle = nextAngle;
        //Scale the length of the chain according to distance from boss to hammer
        chainSpriteRenderer.size = new Vector2(distanceToHammer / defaultXScale, defaultYScale);
        //Offset the chain to make sure the end of the chain starts at the boss
        transform.localPosition = new Vector2(bossToHammer.x / 2, bossToHammer.y / 2);
    }
}
