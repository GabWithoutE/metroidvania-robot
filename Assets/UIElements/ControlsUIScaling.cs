using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUIScaling : MonoBehaviour {
	public GameObject profilePicPanel;
	public GameObject healthPanel;
	public GameObject joystickPanel;
	public GameObject attackButtonPanel;
	public GameObject scorePanel;
	private RectTransform profilePicPanelRectTransform;
	private RectTransform healthPanelRectTransform;
	private RectTransform joystickPanelRectTransform;
	private RectTransform attackButtonPanelRectTransform;
	private RectTransform scorePanelRectTransform;

	// Use this for initialization
	void Awake () {
		profilePicPanelRectTransform = profilePicPanel.GetComponent<RectTransform>();
		healthPanelRectTransform = healthPanel.GetComponent<RectTransform>();
		joystickPanelRectTransform = joystickPanel.GetComponent<RectTransform>();
    	attackButtonPanelRectTransform = attackButtonPanel.GetComponent<RectTransform>();
		scorePanelRectTransform = scorePanel.GetComponent<RectTransform>();

		UIScalingFunctions
			.ScaleXAnchorsToYAnchorsOuterEdge(
			    UIScalingFunctions.Screen_Corners.BOTTOM_RIGHT, 
				attackButtonPanelRectTransform);
		UIScalingFunctions
			.ScaleXAnchorsToYAnchorsInnerEdge(
                UIScalingFunctions.Screen_Corners.BOTTOM_RIGHT,
                attackButtonPanelRectTransform);

		UIScalingFunctions
            .ScaleXAnchorsToYAnchorsOuterEdge(
				UIScalingFunctions.Screen_Corners.BOTTOM_LEFT,
				joystickPanelRectTransform);
        UIScalingFunctions
            .ScaleXAnchorsToYAnchorsInnerEdge(
				UIScalingFunctions.Screen_Corners.BOTTOM_LEFT,
				joystickPanelRectTransform);

		UIScalingFunctions
            .ScaleXAnchorsToYAnchorsOuterEdge(
				UIScalingFunctions.Screen_Corners.TOP_RIGHT,
				healthPanelRectTransform);
   
		UIScalingFunctions
            .ScaleXAnchorsToYAnchorsOuterEdge(
				UIScalingFunctions.Screen_Corners.TOP_LEFT,
				profilePicPanelRectTransform);
        UIScalingFunctions
            .ScaleXAnchorsToYAnchorsInnerEdge(
				UIScalingFunctions.Screen_Corners.TOP_LEFT,
				profilePicPanelRectTransform);

		UIScalingFunctions
			.ScaleXAnchorsToYAnchorsOuterEdge(
				UIScalingFunctions.Screen_Corners.TOP_RIGHT,
				scorePanelRectTransform);
		UIScalingFunctions
			.ScaleXAnchorsToYAnchorsInnerEdge(
                UIScalingFunctions.Screen_Corners.TOP_RIGHT,
                scorePanelRectTransform);

		float profilePicPanelMaxX = profilePicPanelRectTransform.anchorMax.x;
		float profilePicPanelMinX = profilePicPanelRectTransform.anchorMin.x;
        
		healthPanelRectTransform.anchorMin = 
			new Vector2(profilePicPanelMaxX + profilePicPanelMinX,
			            healthPanelRectTransform.anchorMin.y);
		healthPanelRectTransform.anchorMax =
            new Vector2((float).4, healthPanelRectTransform.anchorMax.y);
	}

	private void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
