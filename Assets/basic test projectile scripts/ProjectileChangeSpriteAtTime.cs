using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChangeSpriteAtTime : MonoBehaviour {
	public Sprite changeTo;
	public double afterTime = 0.5;
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		afterTime -= Time.deltaTime;
		if (IsTimeToChange ()) {
			spriteRenderer.sprite = changeTo;
		}
	}

	private bool IsTimeToChange(){
		return afterTime <= 0;
	}
}
