using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecoil : MonoBehaviour {
    private ICharacterStateManager stateManager;
    private CharacterState recoilState;

    void Awake()
    {
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        recoilState = new CharacterState(ConstantStrings.RECOIL_STATE, new bool[] { false, false });
        stateManager.RegisterCharacterState(recoilState.name, recoilState);
    }
}
