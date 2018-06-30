using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStats : MonoBehaviour
{
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private float range;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float spellDamage;
    private float originalProjectileSpeed;

    public bool canBeSlowed = true;

	void Start()
	{
        originalProjectileSpeed = projectileSpeed;
	}

	public float GetCooldownTime()
    {
        return cooldownTime;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public float GetSpellDamage()
    {
        return spellDamage;
    }

    void TimeWarpSlowDown(float[] effect)
    {
        projectileSpeed = projectileSpeed - projectileSpeed * effect[0];
        Invoke("RestoreSpeed", effect[1]);
    }

    void RestoreSpeed() {
        projectileSpeed = originalProjectileSpeed;
    }
}
