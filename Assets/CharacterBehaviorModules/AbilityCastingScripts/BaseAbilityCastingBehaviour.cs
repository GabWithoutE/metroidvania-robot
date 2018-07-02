using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbilityCastingBehaviour : AbstractAbilityCastingBehaviour {

    protected override void CastLightAttack(object castState)
    {
        base.DefaultCastLightAttack(castState);
    }

    protected override void CastHeavyAttack(object castState)
    {
        base.DefaultCastHeavyAttack(castState);
    }

    protected override void CastUtilityAbility(object castState)
    {
        GameObject mine = Instantiate(utilityAbility, abilitySpawnLocation, Quaternion.identity);
        gameObject.transform.root.gameObject.BroadcastMessage("AbilityCasted", mine);
        mine.GetComponent<CasterReference>().SetCaster(transform.root.gameObject);
        //base.DefaultCastUtilityAbility(castState);
    }
}
