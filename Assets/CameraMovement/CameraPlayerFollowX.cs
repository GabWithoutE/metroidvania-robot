using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollowX : MonoBehaviour {
	private enum Side{
        right,
        left
	}

	public GameObject playerReference;
	private ICharacterStateObserver stateObserver;
	private float playerHorizontalMoveSpeed;
	public float cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio;

	private LayerMask cameraDirectionLayerMask;

	public float maxHorizontalDistanceFromPlayer;
	private float newCameraPositionX;
    
	private bool useDefaultStrat;

	private void Awake()
	{
		stateObserver = playerReference.GetComponent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		cameraDirectionLayerMask = LayerMask.GetMask("CameraDirections");
	}
   
	private bool gottenSpeedScale;

	// Update is called once per frame
	void Update() {
		/*
		 * Wait for speed scale to be instantiated then get the values
		 */ 
		if (!gottenSpeedScale)
		{
			playerHorizontalMoveSpeed = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE))[3];
			gottenSpeedScale = true;
		}
		newCameraPositionX = transform.position.x;

        /*
         * Check if directors available and use if relevant
         */ 
		RaycastHit2D[] directors = CheckForDirector();
		if (directors.Length > 0){
			int directed = 0;
			foreach(RaycastHit2D director in directors){
				directed += ChooseDirectorStrategy(director.collider.GetComponent<BoxCollider2D>());
			}
            /*
             * bool useDefaultStrat is equal to whether or not other strats were used
             */ 
			useDefaultStrat = directed == 0;
		} else {
			useDefaultStrat = true;
		}

        /*
         * Use default strat if no other strats were used
         */ 
		if(useDefaultStrat) {
			UseDefaultStrategy();
		}

		transform.position = new Vector3(newCameraPositionX, transform.position.y, transform.position.z);
	}
    
	/*
     * use raycast to check if player is in director zones
     */ 
	private RaycastHit2D[] CheckForDirector(){
		Debug.DrawRay(playerReference.transform.position, Vector3.up, Color.green);
		return Physics2D.RaycastAll(playerReference.transform.position, Vector2.up, 1, cameraDirectionLayerMask);
	}

    /*
     * Select Director Strategy
     */ 
	private int ChooseDirectorStrategy(BoxCollider2D directorBox){
		int usedStrat;
		switch(directorBox.name){
			case ConstantStrings.CameraDirectionBoxes.STAGE_EDGE_HORIZONTAL_LEFT:
				UseStageEdgeHorizontalEdgeStrategy(directorBox, Side.left);
				usedStrat = 1;
				break;
			case ConstantStrings.CameraDirectionBoxes.STAGE_EDGE_HORIZONTAL_RIGHT:
				UseStageEdgeHorizontalEdgeStrategy(directorBox, Side.right);
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
    * default strategy when no directors
    */
    private void UseDefaultStrategy()
    {
        float speedRatio = CalculateSpeedRatioCurrentDistanceToMaxDistance(playerReference.transform.position.x, 
		                                                                   transform.position.x, 
		                                                                   maxHorizontalDistanceFromPlayer);
        speedRatio = Mathf.Clamp(speedRatio, -cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio, cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio);
        newCameraPositionX += speedRatio * playerHorizontalMoveSpeed * Time.deltaTime;
    }

    /*
     * strategy for horizontal edge directors
     */ 
	private void UseStageEdgeHorizontalEdgeStrategy(BoxCollider2D directorBox, Side side){
		float referenceEdge;
		if(side == Side.left){
			referenceEdge = directorBox.bounds.max.x;
		} else {
			referenceEdge = directorBox.bounds.min.x;
		}
		float speedRatio = CalculateSpeedRatioCurrentDistanceToMaxDistance(referenceEdge, 
		                                                                   transform.position.x, 
		                                                                   maxHorizontalDistanceFromPlayer);
		speedRatio = Mathf.Clamp(speedRatio, -cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio, cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio);
		newCameraPositionX += speedRatio * playerHorizontalMoveSpeed * Time.deltaTime;
	}
        

    /*
     * strategy for locked camera area directors
     */ 
	private void UseLockFocusOnAreaStrategy(BoxCollider2D directorBox){
		float lockFocusAreaCenterXPosition = directorBox.transform.position.x;
		float speedRatio = CalculateSpeedRatioCurrentDistanceToMaxDistance(lockFocusAreaCenterXPosition, 
		                                                                   transform.position.x, 
		                                                                   maxHorizontalDistanceFromPlayer);
		if (Mathf.Abs(speedRatio) > cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio){
			speedRatio = Mathf.Sign(speedRatio)*cameraMaxSpeedScaleToPlayerRunSpeedScaleRatio;
		}
		newCameraPositionX += speedRatio * playerHorizontalMoveSpeed * Time.deltaTime;
        
	}

    /*
     * Calculate distance ratio
     */ 
	private float CalculateSpeedRatioCurrentDistanceToMaxDistance(float referencePosition, float cameraPosition, float distanceToReference){
		return (-cameraPosition + referencePosition) / distanceToReference;
	}
}
