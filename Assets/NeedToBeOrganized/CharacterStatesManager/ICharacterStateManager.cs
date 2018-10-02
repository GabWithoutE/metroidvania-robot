using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterStateManager {
	CharacterState GetExistingCharacterState(string name);
	void RegisterCharacterState(string name, CharacterState characterStateObject);
	bool ExistsState(string name);
	object GetCharacterStateValue(string name);
	CharacterState.CharacterStateSubscription GetCharacterStateSubscription(string name);
	void DeregisterCharacterstate(string name);
}
