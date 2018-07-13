using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float cameraShake;
    public float duration;
    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }
    
    public void Shake()
    {
        InvokeRepeating("BeginShake", 0, duration);
        Invoke("StopShake", 1);
    }

	void BeginShake()
    {
        float shakeX = Random.value * cameraShake * 2 - cameraShake;
        float shakeY = Random.value * cameraShake * 2 - cameraShake;
        Vector2 shakePosition = new Vector2(shakeX, shakeY);
        transform.position = shakePosition;
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        transform.position = originalPosition;
    }
}
