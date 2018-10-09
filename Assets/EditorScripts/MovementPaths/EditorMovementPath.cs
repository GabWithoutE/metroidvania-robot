using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMovementPath : MonoBehaviour {

	public Color rayColor = Color.white;
	public List<Transform> pathWaypointTransforms = new List<Transform>();
	Transform[] array;

	void OnDrawGizmos(){
		Gizmos.color = rayColor;
		array = GetComponentsInChildren<Transform>();
		pathWaypointTransforms.Clear();

		foreach(Transform path_obj in array){
			if (path_obj != this.transform){
				pathWaypointTransforms.Add(path_obj);
			}
		}

		for(int i = 0; i < pathWaypointTransforms.Count; i++){
			Vector3 current = pathWaypointTransforms[i].position;
			if (i > 0) {
				Vector3 previous = pathWaypointTransforms[i-1].position;
				Gizmos.DrawLine(previous, current);
				Gizmos.DrawWireSphere(current,0.3f);
			}
		}
	}

}
