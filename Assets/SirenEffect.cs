using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenEffect : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public float rotateSpeed;
    public float lightAmplitude;
    private float intensity;
    public float fadeFrequency;
    private Color color;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
        //Spins siren
        transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed, Space.World);
        //Calculates opacity of light
        intensity = 0.6f + lightAmplitude * Mathf.Sin(Time.time * fadeFrequency);
        //Sets opacity
        spriteRenderer.color = new Color(color.r, color.g, color.b, intensity);
	}
}
