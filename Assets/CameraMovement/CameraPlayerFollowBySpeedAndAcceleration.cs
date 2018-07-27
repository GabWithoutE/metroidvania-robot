using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollowBySpeedAndAcceleration : MonoBehaviour {
	/*
	 * This was just a test, but maybe it will be useful later on.
	 * 
	 */ 
	public GameObject player;
	private ICharacterStateObserver playerStateObserver;
	private LayerMask cameraDirectionMask;

	//public float upwardAcceleration;
	public float downwardAcceleration;
	public float horizontalAcceleration;

	private Vector3 newCameraPosition;
	private float cameraHorizontalMaxSpeed;
	private float cameraFallMaxSpeed;
	private float cameraJumpMaxSpeed;
	private float cameraHorizontalCurrentSpeed;
    private float cameraFallCurrentSpeed;
    private float cameraJumpCurrentSpeed;


	private void Awake()
	{
		playerStateObserver = player.GetComponent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		cameraDirectionMask = LayerMask.GetMask("CameraDirections");
		newCameraPosition = transform.position;

		cameraHorizontalCurrentSpeed = 0;
		cameraFallCurrentSpeed = 0;
		cameraJumpCurrentSpeed = 0;
	}

	// Use this for initialization
	void Start () {
		//float[] speedScale = ((float[])playerStateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE));
		//cameraHorizontalMaxSpeed = speedScale[3];
		//cameraFallMaxSpeed = speedScale[4];
		//cameraJumpMaxSpeed = speedScale[5];

		////upwardAcceleration = cameraJumpMaxSpeed * 2;
		//downwardAcceleration = cameraFallMaxSpeed * 1.5f;
		//horizontalAcceleration = cameraHorizontalMaxSpeed * 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		float[] playerVelocity = (float[])playerStateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
		TrackPlayerByMovement();

		RaycastHit2D[] hitCameraDirectors =
            Physics2D.RaycastAll(player.transform.position, Vector2.up, 1, cameraDirectionMask);
        Debug.DrawRay(player.transform.position, Vector2.up, Color.blue);

		if (hitCameraDirectors.Length > 0)
        {
            foreach (RaycastHit2D director in hitCameraDirectors)
            {
                FollowCameraDirection(director.collider.gameObject);
            }
        }

		transform.position = Vector3.Lerp(transform.position, newCameraPosition, 1.2f);
			//newCameraPosition;

	}

	private void FollowCameraDirection(GameObject director)
    {
        switch (director.name)
        {
            case ConstantStrings.CameraDirectionBoxes.STAGE_EDGE_HORIZONTAL_RIGHT:
                DirectCameraStageEdgeHorizontalMovementRight(director.GetComponent<BoxCollider2D>());
                break;
            case ConstantStrings.CameraDirectionBoxes.STAGE_EDGE_HORIZONTAL_LEFT:
                DirectCameraStageEdgeHorizontalMovementLeft(director.GetComponent<BoxCollider2D>());
                break;
            //case ConstantStrings.CameraDirectionBoxes.STAGE_BOTTOM_LINE:
                //DirectCameraBottomLineMovement(director.GetComponent<BoxCollider2D>());
                //topLineStrategy = ConstantStrings.CameraDirectionBoxes.STAGE_BOTTOM_LINE;
                //break;
            case ConstantStrings.CameraDirectionBoxes.FOCUS_ON_AREA:
                DirectCameraFocusOnAreaMovement(director.GetComponent<BoxCollider2D>());
                break;
            default:
                break;
        }
    }

	/*
 * Directs the camera s.t. the camera no longer centers the player at the edge of a stage
 */
    private void DirectCameraStageEdgeHorizontalMovementRight(BoxCollider2D directorCollider)
    {

    }

    /*
     * Directs the camera s.t. the camera no longer centers the player at the edge of a stage
     */
    private void DirectCameraStageEdgeHorizontalMovementLeft(BoxCollider2D directorCollider)
    {
        float desiredXPosition = directorCollider.bounds.max.x;
		if (transform.position.x <= desiredXPosition){
			newCameraPosition.x = desiredXPosition;
		}
    }

    /*
     * Directs the camera s.t. the camera moves to the desired location when the player enters a contained 
     * no camera movement area
     */
    private void DirectCameraFocusOnAreaMovement(BoxCollider2D directorCollider)
    {

    }

	private void TrackPlayerByMovement(){
		float[] speedScale = ((float[])playerStateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE));
        cameraHorizontalMaxSpeed = speedScale[3];
        cameraFallMaxSpeed = speedScale[4];
        cameraJumpMaxSpeed = speedScale[5];

        //upwardAcceleration = cameraJumpMaxSpeed * 2;
        downwardAcceleration = cameraFallMaxSpeed * 1.5f;
        horizontalAcceleration = cameraHorizontalMaxSpeed * 2f;

        float[] playerVelocity = (float[])playerStateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY);
        float[] playerSpeedScale = (float[])playerStateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE);
        if (playerVelocity[1] > 0)
        {
            newCameraPosition.y += Time.deltaTime * playerSpeedScale[1];
        }
        if (playerVelocity[1] < 0)
        {
            cameraFallCurrentSpeed += Time.deltaTime * downwardAcceleration;
            cameraFallCurrentSpeed = Mathf.Clamp(cameraFallCurrentSpeed, 0, cameraFallMaxSpeed);
            newCameraPosition.y -= Time.deltaTime * cameraFallCurrentSpeed;
        }
        else
        {
            cameraFallCurrentSpeed = 0;
        }

        if (playerVelocity[0] > 0)
        {
            if (player.transform.position.x >= newCameraPosition.x)
            {
                cameraHorizontalCurrentSpeed += Time.deltaTime * horizontalAcceleration;
                cameraHorizontalCurrentSpeed = Mathf.Clamp(cameraHorizontalCurrentSpeed, 0, cameraHorizontalMaxSpeed);
                newCameraPosition.x += Time.deltaTime * cameraHorizontalCurrentSpeed;
            }

        }
        else if (playerVelocity[0] < 0)
        {
            if (player.transform.position.x <= newCameraPosition.x)
            {
                cameraHorizontalCurrentSpeed += Time.deltaTime * horizontalAcceleration;
                cameraHorizontalCurrentSpeed = Mathf.Clamp(cameraHorizontalCurrentSpeed, 0, cameraHorizontalMaxSpeed);
                newCameraPosition.x -= Time.deltaTime * cameraHorizontalCurrentSpeed;
            }
        }
        else
        {
            cameraHorizontalCurrentSpeed = 0;
        }
	}
}
