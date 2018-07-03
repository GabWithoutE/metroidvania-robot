using UnityEngine;
using System.Collections;

public class MineExplode : MonoBehaviour
{
    public GameObject explosion;
    public GameObject enemyExplosion;
    private SpellStats spellStats;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        if (other.tag == "Enemy")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
            
            GameObject mineExplosion = Instantiate(explosion, transform.position, transform.rotation, transform);
            mineExplosion.GetComponent<TurretExplosionDamageSender>().BlowUpTurrets(spellStats.GetSpellDamage(), spellStats.GetRange());
        }        
    }

    private void Awake()
    {
        spellStats = GetComponent<SpellStats>();
    }
}