using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour {
    private GameObject bossWall;
	// Use this for initialization
	void Start () {
        bossWall = GameObject.Find("Grid").transform.Find("BossWall").gameObject;
        bossWall.SetActive(false);
	}	

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            bossWall.SetActive(true);
        }
    }
}
