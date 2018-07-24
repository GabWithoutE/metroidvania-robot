using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour {
	public GameObject player;
	private int screenEdgeMask;
	private void Awake()
	{
		screenEdgeMask = LayerMask.NameToLayer("ScreenEdge");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		/*
         * TODO do raycast from player position to check if i'm hittin the edge of the map, stop the camera. 
         */
		Debug.DrawRay(player.transform.position, Vector3.right, Color.blue);
		RaycastHit2D hitScreenEdge = Physics2D.Raycast(player.transform.position, Vector2.right, 10, screenEdgeMask);
	}
}
