using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterStateObserver {
	object GetCharacterStateValue(string name);
	CharacterState.CharacterStateSubscription GetCharacterStateSubscription(string name);
	bool ExistsState(string name);
}
