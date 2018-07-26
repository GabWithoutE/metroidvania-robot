using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour {
	public GameObject player;
    /*
     * Used when the camera is moving independently of the exact movement of the player;
     */ 
	public float cameraLagSpeed;
    /*
     * Used to pan the camera slightly up when jumping and when the vertical input axis is being pressed;
     */ 
	public float defaultTopLineDistance;
    /*
     * Used to offset the player from the center of the screen in default situations.
     */ 
	public float cameraVerticalOffsetFromBottomRatio;
	private LayerMask cameraDirectionMask;
	private Vector3 newCameraPosition;

	private float cameraDistanceToVerticalEdge;
	private float cameraDistanceToHorizontalEdge;
	private float cameraHeight;
	private float cameraWidth;

	private float desiredCameraYPosition;
	private float screenBottom;
	private float characterBellowThisHeight;
	private ICharacterStateObserver playerStateObserver;

	private Vector3 cameraViewportPosition;
	private Vector3 cameraViewportTopRightCornerPosition;
	private Vector3 cameraViewportBottomLeftCornerPosition;


	private bool topLineHit;
	private float playerYVelocityStateObservation;
	private float topLineyPosition;
	private string topLineStrategy;

	public float fallingLagDistance;
	public float fallingLagDistanceIncreaseRate;
	private float currentFallingLagDistance;

	private float bottomLineYPosition;
    /*
     * TODO:
     * if the player exceeds the vertical offset distance, then do the slight camera vertical pan, then when player lands pan back.
     */ 

	private void Awake()
	{
		topLineStrategy = "";
		topLineHit = false;
		playerStateObserver = player.GetComponent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		SetScreenDistances();
		currentFallingLagDistance = 0;
	}

	private void SetScreenDistances(){
		cameraViewportPosition = new Vector3(0.5f, 0.5f, transform.position.z);
        cameraViewportTopRightCornerPosition = new Vector3(0f, 0f, transform.position.z);
        cameraViewportBottomLeftCornerPosition = new Vector3(1f, 1f, transform.position.z);

        cameraDirectionMask = LayerMask.GetMask("CameraDirections");
        newCameraPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 topRightCornerPosition = Camera.main.ViewportToWorldPoint(cameraViewportTopRightCornerPosition);
        Vector3 middlePosition = Camera.main.ViewportToWorldPoint(cameraViewportPosition);
        Vector3 bottomRightCornerPosition = Camera.main.ViewportToWorldPoint(cameraViewportBottomLeftCornerPosition);

        cameraDistanceToVerticalEdge = topRightCornerPosition.y - middlePosition.y;
        cameraDistanceToHorizontalEdge = topRightCornerPosition.x - middlePosition.x;
        cameraHeight = topRightCornerPosition.y - bottomRightCornerPosition.y;
        cameraWidth = topRightCornerPosition.x - bottomRightCornerPosition.x;
	}

	// Use this for initialization
	void Start () {
		playerStateObserver.GetCharacterStateSubscription(ConstantStrings.GROUNDED).OnStateChanged += CalculateTopLineOnGrounded;	
	}
	
	// Update is called once per frame
	void Update () {
		playerYVelocityStateObservation = ((float[])playerStateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY))[1];
		newCameraPosition = transform.position;
		newCameraPosition.x = player.transform.position.x;
		/*
		 * cast raycast and collect all hits
		 * if director is available, use the director's strategy, if not, use the default strategy
		 */ 
		RaycastHit2D[] hitCameraDirectors =
			Physics2D.RaycastAll(player.transform.position, Vector2.up, 1, cameraDirectionMask);
		Debug.DrawRay(player.transform.position, Vector2.up, Color.blue);
		if (hitCameraDirectors.Length > 0){
			foreach(RaycastHit2D director in hitCameraDirectors){
				FollowCameraDirection(director.collider.gameObject);
			}
		} else {
			DefaultCameraDirectionMovement();
		}


		if (playerYVelocityStateObservation == 0)
        {
			currentFallingLagDistance = 0;
		    MoveLaggingCamera();
		}
		transform.position = newCameraPosition;
	}

	private void MoveLaggingCamera(){
            if (newCameraPosition.y < desiredCameraYPosition)
            {
                newCameraPosition.y += Time.deltaTime * cameraLagSpeed;
                newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, float.MinValue, desiredCameraYPosition);
            }
            else if (newCameraPosition.y > desiredCameraYPosition)
            {
                newCameraPosition.y -= Time.deltaTime * cameraLagSpeed;
                newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, desiredCameraYPosition, float.MaxValue);
            }
	}
    
	private void FollowCameraDirection(GameObject director){
		switch (director.name){
			case ConstantStrings.CameraDirectionBoxes.STAGE_EDGE_HORIZONTAL_RIGHT:
				DirectCameraStageEdgeHorizontalMovementRight(director.GetComponent<BoxCollider2D>());
				break;
			case ConstantStrings.CameraDirectionBoxes.STAGE_EDGE_HORIZONTAL_LEFT:
				DirectCameraStageEdgeHorizontalMovementLeft(director.GetComponent<BoxCollider2D>());
                break;
			case ConstantStrings.CameraDirectionBoxes.STAGE_BOTTOM_LINE:
				DirectCameraBottomLineMovement(director.GetComponent<BoxCollider2D>());
				topLineStrategy = ConstantStrings.CameraDirectionBoxes.STAGE_BOTTOM_LINE;
				break;
			case ConstantStrings.CameraDirectionBoxes.FOCUS_ON_AREA:
				DirectCameraFocusOnAreaMovement(director.GetComponent<BoxCollider2D>());
				break;
			default:
				break;
		}
	}

	private void CalculateTopLineOnGrounded(object groundedState){
		bool isGrounded = ((bool[])groundedState)[1];
		switch(topLineStrategy){
			case ConstantStrings.CameraDirectionBoxes.STAGE_BOTTOM_LINE:
				break;
			default:
				DefaultCalculateTopLine();
				break;
		}

	}

	//private void FromBottomLineCalculateTopLine(){
	//	/*
	//	 * use the bottom line to calculate the top line using the ratio
	//	 * don't allow the camera to fall below the bottom line + the fall lag distance
	//	 */ 
	//	topLinePosition = bottomLineYPosition + (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight;
	//	print(topLinePosition);
	//}

    /*
     * Calculates the default top line (slightly above the player);
     */ 
	private void DefaultCalculateTopLine(){
		topLineyPosition = player.transform.position.y + defaultTopLineDistance; 
	}

	/*
     * Directs the camera such that the bottom of the director's collider is used as the bottom line of the screen
     */
    private void DirectCameraBottomLineMovement(BoxCollider2D directorCollider)
    {
        bottomLineYPosition = directorCollider.bounds.min.y;
		topLineyPosition = bottomLineYPosition + (cameraVerticalOffsetFromBottomRatio * cameraHeight);
		//print(((.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight));
		//print(topLinePosition);

		Debug.DrawRay(new Vector3(transform.position.x, topLineyPosition, 0), Vector3.right * 3, Color.green);

		if (playerYVelocityStateObservation != 0){
			if (playerYVelocityStateObservation > 0)
			{   
				if (newCameraPosition.y == desiredCameraYPosition){
					if (player.transform.position.y > topLineyPosition)
                    {
                        newCameraPosition.y = player.transform.position.y + (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight;
                        print(((.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight));

                    }
				} else {
					if(player.transform.position.y >= newCameraPosition.y){
						newCameraPosition.y = player.transform.position.y + (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight;
					}
				}

			} else {
				if (player.transform.position.y > topLineyPosition)
                {
					currentFallingLagDistance += Time.deltaTime * fallingLagDistanceIncreaseRate;
                    currentFallingLagDistance = Mathf.Clamp(currentFallingLagDistance, 0, fallingLagDistance);
                    newCameraPosition.y = player.transform.position.y
                        + (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight + currentFallingLagDistance;
					newCameraPosition.y = Mathf.Clamp(newCameraPosition.y,
					    bottomLineYPosition + (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight + fallingLagDistance, float.MaxValue);
                }
			}
		} else {
			desiredCameraYPosition = bottomLineYPosition + cameraDistanceToVerticalEdge;
		}
    }
	private void DefaultCameraDirectionMovement(){
		if (playerYVelocityStateObservation != 0){
			if (playerYVelocityStateObservation > 0){
				newCameraPosition.y = player.transform.position.y+ (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight;
			} else {
				currentFallingLagDistance += Time.deltaTime * fallingLagDistanceIncreaseRate;
				currentFallingLagDistance = Mathf.Clamp(currentFallingLagDistance, 0, fallingLagDistance);
				newCameraPosition.y = player.transform.position.y 
					+ (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight + currentFallingLagDistance;
			}

		} else{
			desiredCameraYPosition = player.transform.position.y + (.5f - cameraVerticalOffsetFromBottomRatio) * cameraHeight;
		}
	}


    /*
     * Directs the camera s.t. the camera no longer centers the player at the edge of a stage
     */
	private void DirectCameraStageEdgeHorizontalMovementRight(BoxCollider2D directorCollider){
		
	}

	/*
     * Directs the camera s.t. the camera no longer centers the player at the edge of a stage
     */
    private void DirectCameraStageEdgeHorizontalMovementLeft(BoxCollider2D directorCollider)
    {
		float desiredXPosition = directorCollider.bounds.max.x;
		newCameraPosition.x = desiredXPosition;
    }

    /*
     * Directs the camera s.t. the camera moves to the desired location when the player enters a contained 
     * no camera movement area
     */ 
	private void DirectCameraFocusOnAreaMovement(BoxCollider2D directorCollider){
		
	}

}
