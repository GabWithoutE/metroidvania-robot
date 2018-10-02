using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach to environment pieces that act over time so that they can be controlled by the
 * 	synchronization script.
 */
public interface ISynchronizableEnvironmentPiece {
	/*
	 * Starts the action of the environment piece.
	 */
	void BeginAction();
	/* 
	 * Returns the time it takes for a cycle of the environment piece's action to occur
	 */
	float ActionCycleTime();
}
