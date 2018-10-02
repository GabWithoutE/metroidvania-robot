using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    private Vector3 
	originalPosition;

    void Start()
    {
  
    }
    
    public void ShakeForSeconds(float seconds)
    {
		originalPosition = transform.localPosition;
		InvokeRepeating("BeginShake", 0, 0.1f);
		Invoke("StopShake", seconds);
    }

	void BeginShake()
    {
		Vector3 randomPosition = Random.insideUnitCircle;
		transform.localPosition = originalPosition 
			+ new Vector3(randomPosition.x, randomPosition.y, 0) * .3f;
    }
    
    void StopShake()
    {
        CancelInvoke("BeginShake");
		transform.localPosition = originalPosition;
    }
}
