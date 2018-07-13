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

		//Vector3 colliderWorldCoordinates = transform.TransformPoint(transform.root.GetComponent<BoxCollider2D>().size/2);

		BoxCollider2D rootBoxCollider = transform.root.GetComponent<BoxCollider2D>();
		//minX = colliderWorldCoordinates.x;
		//maxX = colliderWorldCoordinates.x;
		//minY = colliderWorldCoordinates.y;
		//maxY = colliderWorldCoordinates.y;
		minX = -transform.root.position.x + rootBoxCollider.bounds.min.x ;
		maxX = -transform.root.position.x + rootBoxCollider.bounds.max.x;
		minY = -transform.root.position.y + rootBoxCollider.bounds.min.y;
		maxY = -transform.root.position.y + rootBoxCollider.bounds.max.y;
        // first is hit head on ground, second is grounded
		hitGroundTopAndGrounded = new bool[] { false, false };

        
		print(rootBoxCollider.bounds.min.x);
        print(transform.position.x);

		distanceBetweenRays = (maxX - minX) / (numberOfRaysPerSide - 1);
		if (numberOfRaysPerSide == 1){
			distanceBetweenRays = 0;
		}

		print(distanceBetweenRays);

		groundedState = new CharacterState(ConstantStrings.GROUNDED, hitGroundTopAndGrounded);
		stateManager.RegisterCharacterState(groundedState.name, groundedState);
	}

	private void Update()
	{
		IsCollidingWithGroundVertically();
		displayState = (bool[])groundedState.GetStateValue();
       
	}

	private void IsCollidingWithGroundVertically(){

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
			//Vector2 startingPosition = new Vector2(startX + transform.position.x + (spaceBetween * i), yPos + transform.position.y);
			Vector2 startingPosition = new Vector2(startX + transform.position.x + (spaceBetween * i), yPos + transform.position.y);

			Debug.DrawRay(startingPosition, direction * rayLength, Color.red);
			hitInfo = Physics2D.Raycast(startingPosition, direction, rayLength, groundMask);
            if (hitInfo)
            {
                return true;
            }
        }
		return false;
	}
}
