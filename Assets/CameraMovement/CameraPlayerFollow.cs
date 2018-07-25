using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour {
	public GameObject player;
	public float horizontalDistanceToMapEdge;
	public float verticalDistanceToMapEdge;
	public float cameraVerticalOffset;
	private LayerMask mapEdgeLayerMask;
	private Vector3 cameraPosition;

	private void Awake()
	{
		mapEdgeLayerMask = LayerMask.GetMask("MapEdge");
		cameraPosition = new Vector3(0, 0, transform.position.z);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

		/*
         * TODO do raycast from player position to check if i'm hittin the edge of the map, stop the camera. 
         */
        
		if(!CastRaysHorizontal()){
			cameraPosition.x = player.transform.position.x;
			//transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraVerticalOffset, transform.position.z);
		} else {
			cameraPosition.x = transform.position.x;
		}

		if (!CastRaysVertical()){
			cameraPosition.y = player.transform.position.y + cameraVerticalOffset;
		} else {
			cameraPosition.y = transform.position.y;
		}
		transform.position = cameraPosition;

        /*
         * TODO:
         * Move camera to certain areas. Do different Camera movement types based on colliders.
         */ 

	}

	private bool CastRaysHorizontal(){
		//RaycastHit2D hitStageEdgeRight = Physics2D.Raycast(player.transform.position, Vector2.right, horizontalDistanceToMapEdge, mapEdgeMask);
		RaycastHit2D hitStageEdgeRight = Physics2D.Raycast(player.transform.position, Vector2.right, horizontalDistanceToMapEdge, mapEdgeLayerMask);

		Debug.DrawRay(player.transform.position, Vector2.right * horizontalDistanceToMapEdge, Color.blue);
		if (hitStageEdgeRight){
			return true;
		}
		RaycastHit2D hitStageEdgeLeft = Physics2D.Raycast(player.transform.position, Vector2.left, horizontalDistanceToMapEdge, mapEdgeLayerMask);

		Debug.DrawRay(player.transform.position, Vector2.left * horizontalDistanceToMapEdge, Color.blue);
		if (hitStageEdgeLeft){
			return true;
		}

		return false;
	}

	private bool CastRaysVertical(){
		RaycastHit2D hitStageUp = Physics2D.Raycast(player.transform.position, Vector2.up, verticalDistanceToMapEdge, mapEdgeLayerMask);
		Debug.DrawRay(player.transform.position, Vector2.up * verticalDistanceToMapEdge, Color.blue);
		if (hitStageUp){
			return true;
		}

		RaycastHit2D hitStageDown = Physics2D.Raycast(player.transform.position, Vector2.down, verticalDistanceToMapEdge, mapEdgeLayerMask);
		Debug.DrawRay(player.transform.position, Vector2.down * verticalDistanceToMapEdge, Color.blue);
		if (hitStageDown){
			return true;
		}
		return false;
	}
}
