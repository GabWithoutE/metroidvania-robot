using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : MonoBehaviour {
	public float lengthOfRays;
    public int numberOfRaysPerSide;

	private bool[] hitGroundTopAndGrounded;
	private ICharacterStateManager stateManager;
	private CharacterState groundedState;
	private LayerMask groundMask;
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private float distanceBetweenRays;
	private RaycastHit2D hitInfo;

	public bool[] displayState;
	private void Awake()
	{
		groundMask = LayerMask.GetMask("Ground");
		stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;

		minX = transform.root.GetComponent<BoxCollider2D>().bounds.min.x;
		maxX = transform.root.GetComponent<BoxCollider2D>().bounds.max.x;
		minY = transform.root.GetComponent<BoxCollider2D>().bounds.min.y;
		maxY = transform.root.GetComponent<BoxCollider2D>().bounds.max.y;
        // first is hit ground, second is grounded
		hitGroundTopAndGrounded = new bool[] { false, false };

		distanceBetweenRays = (maxX - minX) / (numberOfRaysPerSide - 1);
		if (numberOfRaysPerSide == 1){
			distanceBetweenRays = 0;
		}


		groundedState = new CharacterState(ConstantStrings.GROUNDED, hitGroundTopAndGrounded);
		stateManager.RegisterCharacterState(groundedState.name, groundedState);
	}

	private void Update()
	{
		IsCollidingWithGroundVertically();
		displayState = (bool[])groundedState.GetStateValue();
       
	}

	private void IsCollidingWithGroundVertically(){
		// cast rays up
		if (CastRays(minX, maxY, Vector2.up, lengthOfRays, distanceBetweenRays)){
			hitGroundTopAndGrounded[0] = true;
		} else {
			hitGroundTopAndGrounded[0] = false;
		}
		if (CastRays(minX, minY, Vector2.down, lengthOfRays, distanceBetweenRays)){
			hitGroundTopAndGrounded[1] = true;
		} else {
			hitGroundTopAndGrounded[1] = false;
		}
		groundedState.SetState(hitGroundTopAndGrounded);
	}

	private bool CastRays(float startX, float yPos, Vector2 direction, float rayLength, float spaceBetween){
		for (int i = 0; i < numberOfRaysPerSide; i++)
        {
			Vector2 startingPosition = new Vector2(startX + transform.position.x + (spaceBetween * i), yPos + transform.position.y);
			Debug.DrawRay(startingPosition, direction * rayLength, Color.yellow);
			hitInfo = Physics2D.Raycast(startingPosition, direction, rayLength, groundMask);
            if (hitInfo)
            {
                return true;
            }
        }
		return false;
	}
}
