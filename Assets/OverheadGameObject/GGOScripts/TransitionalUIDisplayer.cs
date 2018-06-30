using UnityEngine;
using System.Collections;

public class TransitionalUIDisplayer : MonoBehaviour
{
    public GameObject DisplayUIElement(GameObject UIElement){
		/*
         * Instantiates UI Elements and puts them in the UIElementCanvas to be displayed on
         * the screen.
         */
		GameObject UIElementCanvas = GameObject.Find("UIElementCanvas") as GameObject;
		GameObject UIElementInstance = Instantiate(UIElement);
		UIElementInstance.transform.SetParent(UIElementCanvas.transform);
		UIElementInstance.GetComponent<RectTransform>().offsetMin= new Vector2(0, 0);
		UIElementInstance.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

		return UIElementInstance;

    }

}
