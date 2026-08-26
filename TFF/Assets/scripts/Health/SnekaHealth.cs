using UnityEngine;

public class SnekaHealth : MonoBehaviour, IDamagable
{
    public float health;
    public GameObject parent;
    public string dropName;
    public string spawnAnim;

    public float dMultR;
    public float dMultP;

    GameObject player;
    Rigidbody2D rb;

    float damageCD;
    public float maxDamageCD;

    public float knockbackForce;

    bool canGetInfo = true;

    Combo combo;
    bool killedByPhys;
    bool killedByRang;
    PowerPointsSystem points;

    bool isSpawned = true;

    //cache-----
    ObjectCache cache;
    EffectCache eCache;

    void Update()
    {
        if (isSpawned)
        {
            cache = GameObject.FindGameObjectWithTag("ObjectCache").GetComponent<ObjectCache>();
            eCache = GameObject.FindGameObjectWithTag("EffectCache").GetComponent<EffectCache>();

            Instantiate(eCache.GetFromCache(spawnAnim), transform.position, Quaternion.identity);
            isSpawned = false;
        }

        if (combo == null)
        {
            combo = GameObject.Find("Combo Manager").GetComponent<Combo>();
            points = GameObject.Find("ScoreManager").GetComponent<PowerPointsSystem>();
        }

        if (damageCD > 0)
        {
            damageCD -= Time.deltaTime;
        }

        if (canGetInfo == true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rb = GetComponentInParent<Rigidbody2D>();
        }

        if (health <= 0)
        {
            //stunts---------------------------------------------------------------
            if (killedByPhys)
            {
                points.Kill();
            }

            if (killedByRang)
            {
                points.Stoned();
            }
            //---------------------------------------------------------------------

            Destroy(parent);
            Instantiate(cache.GetFromCache(dropName), transform.position, Quaternion.identity);
        }
    }

    public void TakeDamagePhysical(float damage)
    {
        if (damageCD <= 0)
            if (health <= 0)            
                killedByPhys = true;

        Proceedure(damage * dMultP);
    }

    public void TakeDamageRanged(float damage)
    {
        if (damageCD <= 0)
            if (health <= 0)            
                killedByRang = true;           
        
        Proceedure(damage * dMultR);     
    }

    public void TakeDamagePit(float damage)
    {
        health -= damage;
    }

    void Proceedure(float damage)
    {
        if (damageCD <= 0)
        {
            health -= damage;

            //combo
            combo.damageCount += damage;
            combo.isActive = true;
            combo.time = combo.maxTime;
            combo.TextOffset();

        }
        damageCD = maxDamageCD;

        if (transform.position.x > player.transform.position.x)
            rb.velocity = (transform.position - player.transform.position) * knockbackForce;

        if (transform.position.x < player.transform.position.x)
            rb.velocity = -(player.transform.position - transform.position) * knockbackForce;
    }
}
