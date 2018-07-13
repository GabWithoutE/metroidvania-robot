using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStats : MonoBehaviour
{
    public float cooldownTime;
	public float range;
	public float projectileSpeed;
	public float spellDamage;
	private float originalProjectileSpeed;

    public bool canBeSlowed = true;

	void Awake()
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
