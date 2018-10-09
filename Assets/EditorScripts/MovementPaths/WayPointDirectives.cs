using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaypointType{
	End,
	Inbetween,
	Start
}
public class WayPointDirectives : MonoBehaviour {
	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private float waypointRestDuration;
	[SerializeField]
	private WaypointType waypointType;

	public float MovementSpeed{
		get{
			return movementSpeed;
		}
	}

	public float WaypointRestDuration{
		get{
			return waypointRestDuration;
		}
	}

	public WaypointType WaypointType{
		get{
			return waypointType;
		}
	}

}
