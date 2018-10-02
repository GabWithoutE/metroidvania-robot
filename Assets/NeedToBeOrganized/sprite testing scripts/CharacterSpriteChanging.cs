using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteChanging : MonoBehaviour {
    private CharacterStatesManager statesManager;
	private SpriteRenderer spriteRenderer;
    private CharacterState.CharacterStateSubscription directionSubscription;

	public Sprite upSpawnSprite;
	public Sprite upRightSpawnSprite;
	public Sprite rightSpawnSprite;
	public Sprite downRightSpawnSprite;
	public Sprite downSpawnSprite;
	public Sprite downLeftSpawnSprite;
	public Sprite leftSpawnSprite;
	public Sprite upLeftSpawnSprite;

	private bool registered = false;

	private void Awake()
	{
        statesManager = GetComponentInParent<CharacterStatesManager>();

	}
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponentInParent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!registered) {
            directionSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.CARDINAL_DIRECTION);
            directionSubscription.OnStateChanged += ChangeSpriteDirection;
			registered = true;
		}
	}

    void ChangeSpriteDirection (object dir){
        CardinalDirections direction = (CardinalDirections)dir;
		switch (direction) {
            case (CardinalDirections.Up):
			if (upSpawnSprite != null) {
				spriteRenderer.sprite = upSpawnSprite;
			}
			break;
            case(CardinalDirections.Dur):
			if (upRightSpawnSprite != null) {
				spriteRenderer.sprite = upRightSpawnSprite;
			}
			break;
            case (CardinalDirections.Right):
			if (rightSpawnSprite != null) {
				spriteRenderer.sprite = rightSpawnSprite;
			}
			break;
            case(CardinalDirections.Ddr):
			if (downRightSpawnSprite != null) {
				spriteRenderer.sprite = downRightSpawnSprite;
			}
			break;
            case(CardinalDirections.Down):
			if (downSpawnSprite != null) {
				spriteRenderer.sprite = downSpawnSprite;
			}
			break;
            case(CardinalDirections.Ddl):
			if (downLeftSpawnSprite != null) {
				spriteRenderer.sprite = downLeftSpawnSprite;
			}
			break;
            case(CardinalDirections.Left):
			if (leftSpawnSprite != null) {
				spriteRenderer.sprite = leftSpawnSprite;
			}
			break;
            case(CardinalDirections.Dul):
			if (upLeftSpawnSprite != null) {
				spriteRenderer.sprite = upLeftSpawnSprite;
			}
			break;
		}
	}
}
