using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollowY : MonoBehaviour {

	private Vector3 cameraViewportPosition;
    private Vector3 cameraViewportTopRightCornerPosition;
    private Vector3 cameraViewportBottomLeftCornerPosition;

	private float cameraDistanceToVerticalEdge;
    private float cameraHeight;

	public GameObject playerReference;
	public float cameraMaxSpeedScaleToJumpScaleRatio;
	public float maxVerticalDistanceFromPlayer;


    private ICharacterStateObserver stateObserver;
	private float playerJumpMoveSpeed;
	private float playerFallMoveSpeed;
    
	public float cameraVerticalOffsetFromPlayerPaddingRatio;
	private float paddingDistanceBetweenPlayerAndCamera;


	private LayerMask cameraDirectionLayerMask;
	private float newCameraPositionY;

	private bool useDefaultStrat;

	private void Awake()
	{
		stateObserver = playerReference.GetComponent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
        cameraDirectionLayerMask = LayerMask.GetMask("CameraDirections");
		SetScreenDistances();
	}

	private void SetScreenDistances()
    {
        cameraViewportPosition = new Vector3(0.5f, 0.5f, transform.position.z);
        cameraViewportTopRightCornerPosition = new Vector3(0f, 0f, transform.position.z);
        cameraViewportBottomLeftCornerPosition = new Vector3(1f, 1f, transform.position.z);

        Vector3 topRightCornerPosition = Camera.main.ViewportToWorldPoint(cameraViewportTopRightCornerPosition);
        Vector3 middlePosition = Camera.main.ViewportToWorldPoint(cameraViewportPosition);
        Vector3 bottomRightCornerPosition = Camera.main.ViewportToWorldPoint(cameraViewportBottomLeftCornerPosition);

        cameraDistanceToVerticalEdge = topRightCornerPosition.y - middlePosition.y;
        cameraHeight = topRightCornerPosition.y - bottomRightCornerPosition.y;
		paddingDistanceBetweenPlayerAndCamera = (0.5f - cameraVerticalOffsetFromPlayerPaddingRatio) * cameraHeight;


    }

	private bool gottenSpeedScales;
	
	// Update is called once per frame
	void Update () {
		newCameraPositionY = transform.position.y;

		if (!gottenSpeedScales)
        {
			playerJumpMoveSpeed = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE))[4];
			playerFallMoveSpeed = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE))[5];
			gottenSpeedScales = true;
        }

		RaycastHit2D[] directors = CheckForDirector();
		if(directors.Length > 0){
			int directed = 0;
            foreach (RaycastHit2D director in directors)
            {
                directed += ChooseDirectorStrategy(director.collider.GetComponent<BoxCollider2D>());
            }
            /*
             * bool useDefaultStrat is equal to whether or not other strats were used
             */
            useDefaultStrat = directed == 0;
        }
        else
        {
            useDefaultStrat = true;
        }

		if (useDefaultStrat){
			UseDefaultStrategy();
		}

		transform.position = new Vector3(transform.position.x, newCameraPositionY, transform.position.z);

	}

	private RaycastHit2D[] CheckForDirector()
    {
        Debug.DrawRay(playerReference.transform.position, Vector3.up, Color.green);
        return Physics2D.RaycastAll(playerReference.transform.position, Vector2.up, 1, cameraDirectionLayerMask);
    }

	private int ChooseDirectorStrategy(BoxCollider2D directorBox)
    {
        int usedStrat;
        switch (directorBox.name)
        {
			case ConstantStrings.CameraDirectionBoxes.STAGE_BOTTOM_LINE:
				UseStageBottomLineStrategy(directorBox);
				usedStrat = 1;
				break;
            case ConstantStrings.CameraDirectionBoxes.LOCK_FOCUS_ON_AREA:
                UseLockFocusOnAreaStrategy(directorBox);
                usedStrat = 1;
                break;
            default:
                usedStrat = 0;
                break;
        }
        return usedStrat;
    }
    /*
     * default strategy tracks player directly on jump
     * based on distance from camera - padding for falling
     * based on distance from camera to player + padding for grounded
     */ 
	private void UseDefaultStrategy(){
		float playerYVelocity = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY))[1];
        
		if (playerYVelocity < 0){
			UseDefaultFallStrategy();
		} else if (playerYVelocity > 0) {
			UseDefaultJumpStrategy();
		} else {
			UseDefaultGroundedStrategy();
		}      
	}
	private void UseDefaultFallStrategy(){
		float speedRatio =
                CalculateRatioCurrentDistanceToMaxDistance(
                    playerReference.transform.position.y, 
                    transform.position.y - paddingDistanceBetweenPlayerAndCamera, 
                    maxVerticalDistanceFromPlayer);
            speedRatio = Mathf.Clamp(speedRatio, -cameraMaxSpeedScaleToJumpScaleRatio, cameraMaxSpeedScaleToJumpScaleRatio);
            newCameraPositionY += speedRatio * playerJumpMoveSpeed * Time.deltaTime; 
	}
	private void UseDefaultJumpStrategy(){
		float speedRatio =
                CalculateRatioCurrentDistanceToMaxDistance(
                    playerReference.transform.position.y + paddingDistanceBetweenPlayerAndCamera,
                    transform.position.y,
                    paddingDistanceBetweenPlayerAndCamera);
                
		speedRatio = Mathf.Clamp(speedRatio, -cameraMaxSpeedScaleToJumpScaleRatio, cameraMaxSpeedScaleToJumpScaleRatio);
		newCameraPositionY += speedRatio * playerJumpMoveSpeed * Time.deltaTime;

	}
	private void UseDefaultGroundedStrategy(){
		float speedRatio =
                CalculateRatioCurrentDistanceToMaxDistance(
                    playerReference.transform.position.y + paddingDistanceBetweenPlayerAndCamera,
                    transform.position.y,
                    paddingDistanceBetweenPlayerAndCamera);
        speedRatio = Mathf.Clamp(speedRatio, -.3f, .3f);
        
        newCameraPositionY += speedRatio * playerJumpMoveSpeed * Time.deltaTime;
	}
    /*
     * bottom line strategy tracks player using defaultjump and defaultfall when above line
     * grounded uses camera at center position
     */
	private void UseStageBottomLineStrategy(BoxCollider2D directorBox){
		float playerYVelocity = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY))[1];
		float bottomLineYPosition = directorBox.bounds.min.y;
		float cameraAlignedToBottomHeight = bottomLineYPosition + cameraDistanceToVerticalEdge;
		float cameraFollowHeightLimit = cameraAlignedToBottomHeight - paddingDistanceBetweenPlayerAndCamera;

		Debug.DrawRay(new Vector3(transform.position.x, cameraFollowHeightLimit, 0), Vector3.right * 3, Color.green);

        if (playerYVelocity < 0){
			UseFallStageBottomeLineStrategy(cameraFollowHeightLimit);         
		} else if (playerYVelocity > 0) {
            UseJumpStageBottomeLineStrategy(cameraFollowHeightLimit, cameraAlignedToBottomHeight);         
        } else {
			UseGroundedStageBottomeLineStrategy(cameraAlignedToBottomHeight);
        }
	}
	private void UseGroundedStageBottomeLineStrategy(float cameraAlignedToBottomHeight){
		float speedRatio =
               CalculateRatioCurrentDistanceToMaxDistance(
                   cameraAlignedToBottomHeight,
                   transform.position.y,
                   paddingDistanceBetweenPlayerAndCamera);
        speedRatio = Mathf.Clamp(speedRatio, -.3f, .3f);
        newCameraPositionY += speedRatio * playerJumpMoveSpeed * Time.deltaTime;
	}
	private void UseJumpStageBottomeLineStrategy(float cameraFollowHeightLimit, float cameraAlignedToBottomHeight){
		if (playerReference.transform.position.y >= cameraFollowHeightLimit)
        {
            UseDefaultJumpStrategy();
        }
        if (transform.position.y < cameraAlignedToBottomHeight)
        {
			float speedRatio =
                CalculateRatioCurrentDistanceToMaxDistance(
                    playerReference.transform.position.y,
					transform.position.y,
                    paddingDistanceBetweenPlayerAndCamera);
			if (Mathf.Abs(speedRatio) > 1){
				speedRatio = 1;
			}
            speedRatio = Mathf.Clamp(speedRatio, -cameraMaxSpeedScaleToJumpScaleRatio, cameraMaxSpeedScaleToJumpScaleRatio);
            newCameraPositionY += speedRatio * playerJumpMoveSpeed * Time.deltaTime;
        }
	}
	private void UseFallStageBottomeLineStrategy(float cameraFollowHeightLimit){
		if (playerReference.transform.position.y >= cameraFollowHeightLimit)
        {
            UseDefaultFallStrategy();
        }
	}

	private void UseLockFocusOnAreaStrategy(BoxCollider2D directorBox){
		float playerYVelocity = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.VELOCITY))[1];
		float lockFocusAreaCenterYPosition = directorBox.transform.position.y;
		float speedRatio = CalculateRatioCurrentDistanceToMaxDistance(lockFocusAreaCenterYPosition, 
		                                                              transform.position.y, 
		                                                              maxVerticalDistanceFromPlayer);
		if (Mathf.Abs(speedRatio) > cameraMaxSpeedScaleToJumpScaleRatio* 0.3f)
        {
			speedRatio = Mathf.Sign(speedRatio) * cameraMaxSpeedScaleToJumpScaleRatio * 0.3f;
        }
		newCameraPositionY += speedRatio * playerJumpMoveSpeed * Time.deltaTime;
	}
	/*
     * Calculate distance ratio
     */
    private float CalculateRatioCurrentDistanceToMaxDistance(float referencePosition, float cameraPosition, float distanceToReference)
    {
        return (-cameraPosition + referencePosition) / distanceToReference;
    }
}
