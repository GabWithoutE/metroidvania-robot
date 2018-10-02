using UnityEngine;
using System.Collections;

public static class UIScalingFunctions 
{
	public enum Screen_Corners {
		BOTTOM_LEFT,
        BOTTOM_RIGHT,
        TOP_LEFT,
        TOP_RIGHT
	}
	public enum Anchor_Type {
		MAX,
        MIN
	}

	public static void ScaleXAnchorsToYAnchorsInnerEdge (
		Screen_Corners corner, RectTransform rectTransform){
		switch (corner)
		{
			case Screen_Corners.BOTTOM_LEFT:
				// Set Min with Min
				rectTransform.anchorMax = new Vector2((rectTransform.anchorMax.y * Screen.height) / Screen.width,
                                                  rectTransform.anchorMax.y);
				break;
			case Screen_Corners.BOTTOM_RIGHT:
				// Set Max with Min
				rectTransform.anchorMin = new Vector2((Screen.width - (rectTransform.anchorMax.y * Screen.height)) / Screen.width,
                                                  rectTransform.anchorMin.y);
				break;
			case Screen_Corners.TOP_LEFT:
				// Set Min with Max
				rectTransform.anchorMax = new Vector2((Screen.height - (rectTransform.anchorMin.y * Screen.height)) / Screen.width,
                                                  rectTransform.anchorMax.y);
				break;
			case Screen_Corners.TOP_RIGHT:
				// Set Max with Max
				rectTransform.anchorMin = new Vector2((Screen.width - (Screen.height - (rectTransform.anchorMin.y * Screen.height))) / Screen.width,
                                                  rectTransform.anchorMin.y);
				break;
		    
		}
	}

	public static void ScaleXAnchorsToYAnchorsOuterEdge (
		Screen_Corners corner, RectTransform rectTransform){
        switch (corner)
        {
            case Screen_Corners.BOTTOM_LEFT:
				// Set Max with Max Max
				rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.y * Screen.height / Screen.width,
                                   rectTransform.anchorMin.y);
                break;
            case Screen_Corners.BOTTOM_RIGHT:
                // Set Min with Max
				rectTransform.anchorMax = new Vector2((Screen.width - (rectTransform.anchorMin.y * Screen.height)) / Screen.width,
                                                  rectTransform.anchorMax.y);
                break;
            case Screen_Corners.TOP_LEFT:
                // Set Max with Min
				rectTransform.anchorMin = new Vector2((Screen.height - (rectTransform.anchorMax.y * Screen.height)) / Screen.width,
                                                  rectTransform.anchorMin.y);
                break;
            case Screen_Corners.TOP_RIGHT:
                // Set Min with Min
				rectTransform.anchorMax = new Vector2((Screen.width - (Screen.height - (rectTransform.anchorMax.y * Screen.height))) / Screen.width,
                                                  rectTransform.anchorMax.y);
                
                break;

        }
    } 
}
