using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSynchronizableEnvironmentPiece : MonoBehaviour, ISynchronizableEnvironmentPiece {
	public EnvironmentPieceSynchronizationController synchronizationController;

	public abstract void BeginAction();

	public abstract float ActionCycleTime();

}
