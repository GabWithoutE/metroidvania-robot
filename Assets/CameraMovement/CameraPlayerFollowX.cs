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
	private float horizontalMaxMoveSpeed;

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
			horizontalMaxMoveSpeed = ((float[])stateObserver.GetCharacterStateValue(ConstantStrings.SPEED_SCALE))[3];
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
			default:
				usedStrat = 0;
				break;
		}
		return usedStrat;
	}

	private void UseStageEdgeHorizontalEdgeStrategy(BoxCollider2D directorBox, Side side){
		float referenceEdge;
		if(side == Side.left){
			referenceEdge = directorBox.bounds.max.x;
		} else {
			referenceEdge = directorBox.bounds.min.x;
		}
		float speedRatio = CalculateRatioCurrentDistanceToMaxDistance(referenceEdge, transform.position.x);
		newCameraPositionX += speedRatio * horizontalMaxMoveSpeed * Time.deltaTime;

		if (side == Side.left){
			newCameraPositionX = Mathf.Clamp(newCameraPositionX, referenceEdge, float.MaxValue);
		} else {
			newCameraPositionX = Mathf.Clamp(newCameraPositionX, float.MinValue, referenceEdge);
		}
	}
        
	/*
     * default strategy when no directors
     */
    private void UseDefaultStrategy()
    {
        float speedRatio = CalculateRatioCurrentDistanceToMaxDistance(playerReference.transform.position.x, transform.position.x);
        newCameraPositionX += speedRatio * horizontalMaxMoveSpeed * Time.deltaTime;
    }

    /*
     * Calculate distance ratio
     */ 
	private float CalculateRatioCurrentDistanceToMaxDistance(float referencePosition, float cameraPosition){
		return Mathf.Clamp((-cameraPosition + referencePosition) / maxHorizontalDistanceFromPlayer, -1, 1);
	}
}
