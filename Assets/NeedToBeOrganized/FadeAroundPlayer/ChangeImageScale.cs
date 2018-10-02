using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImageScale : MonoBehaviour {
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ChangeScale(float finalScale)
    {
        rectTransform.transform.localScale = new Vector3(finalScale, finalScale, 0);
    }
}
