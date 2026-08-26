using System.Collections;
using UnityEngine;

public class SlimeAI : MonoBehaviour , IDamagable
{
    public string dropName;
    public float shealth;
    public float dMultR;
    public float dMultP;

    bool isAppeared = true;
    AudioSource sound;
    float pitch;

    public GameObject player;
    public Rigidbody2D rb;

    public float maxTimeBtwJump;
    public float timeBtwJump;
    public float jForce;
    public float height;

    bool isSpawning = true;
    public string spawnEffect;
    public ParticleSystem trail;

    public bool hasAI;

    float damageCD;
    float maxDamageCD = 0.01f;

    bool canDash = true;
    bool allowedToDash;
    bool isDashing;
    public float Direction;
    public float dashingTime;
    public float dashingCoolDown;
    public bool isAttacking;
    public GameObject slimeObj;

    public GameObject slime;

    //cache--------------
    ObjectCache cache;
    EffectCache eCache;

    public float knockbackForce;

    public float checkDist;
    public LayerMask whatIsGround;

    //attack variant
    int probability;
    public int triggerProbability;
    public float specialAttackForce;

    //combo
    Combo combo;
    PowerPointsSystem points;
    bool killedByPhys;
    bool killedByRang;

    //status
    StatusEffects status;
    SlimePoisoned poisoned;

    //death
    public GameObject deathEffect;
    public GameObject psnDeathEffect;

    void Update()
    {


        if (status == null)
        {
            status = GetComponent<StatusEffects>();
            poisoned = GetComponent<SlimePoisoned>();
        }

        if (status.poison && GetComponentInParent<SpriteRenderer>().color != poisoned.poisonedColor)
        {
            GetComponentInParent<SpriteRenderer>().color = poisoned.poisonedColor;
        }
         
        if (status.poison == false && GetComponentInParent<SpriteRenderer>().color != poisoned.defaultColor)
        {
            GetComponentInParent<SpriteRenderer>().color = poisoned.defaultColor;
        }

        if (combo == null)
        {
            combo = GameObject.Find("Combo Manager").GetComponent<Combo>();
        }

        if (isAppeared == true)
        {
            isAppeared = false;
            sound = gameObject.GetComponent<AudioSource>();
            points = GameObject.Find("ScoreManager").GetComponent<PowerPointsSystem>();
        }

        allowedToDash = Physics2D.OverlapCircle(transform.position, checkDist, whatIsGround);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (damageCD >= 0)
        {
            damageCD -= Time.deltaTime;
        }

        if (isSpawning == true)
        {
            preSettings();
            isSpawning = false;
        }

        if (slime.transform.position.x > 0 && slime.transform.position.x < player.transform.position.x)
        {
            Direction = 1;
        }

        if (slime.transform.position.x > 0 && slime.transform.position.x > player.transform.position.x)
        {
            Direction = -1;
        }

        if (slime.transform.position.x < 0 && slime.transform.position.x < player.transform.position.x)
        {
            Direction = -1;
        }

        if (slime.transform.position.x < 0 && slime.transform.position.x > player.transform.position.x)
        {
            Direction = 1;
        }

        if (hasAI == true && canDash && allowedToDash)
        {
            probability = Random.Range(1, 101);
            StartCoroutine(dash());
        }

        //death-------------------------------------------------------------------------------------------------------
        if (shealth <= 0)
        {
            Destroy(slimeObj);
            Instantiate(cache.GetFromCache(dropName), transform.position, Quaternion.identity);

            //stunts-------------------------------------------------------------
            if (killedByPhys)
            {
                points.Kill();
            }

            if (killedByRang)
            {
                points.Stoned();
            }

            if (!allowedToDash && killedByRang)
            {
                points.Sniped();
            }

            if (!allowedToDash && killedByPhys)
            {
                points.Airborn();
            }
            //----------------------------------------------------------------------------

            //status death-----------------------------------------------------------------------
            if (status.poison == false)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            if (status.poison)
            {
                Instantiate(psnDeathEffect, transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator dash()
    {
        canDash = false;
        isDashing = true;
        isAttacking = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(Direction * jForce, height);
        JumpSound();
        trail.Play();
        yield return new WaitForSeconds(dashingTime/2);
        if(probability <= triggerProbability)
        {
            trail.Play();
            if (transform.position.x > player.transform.position.x)
            {
                rb.velocity = new Vector2(-(transform.position.x - player.transform.position.x), -(transform.position.y - player.transform.position.y)).normalized * specialAttackForce;
            }

            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y)).normalized * specialAttackForce;
            }
            yield return new WaitForSeconds(dashingTime / 2);
        }
        if (probability > 100 - triggerProbability)
        {
            if (status.poison)
            {
                poisoned.isActive = true;
                GetComponentInParent<SpriteRenderer>().color = poisoned.defaultColor;
            }
            yield return new WaitForSeconds(dashingTime / 2);
        }
        rb.gravityScale = originalGravity;
        isDashing = false; 
        isAttacking = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
        probability = 0;
    }

    public void TakeDamagePhysical(float damage)
    {
        if (damageCD <= 0)
        {
            Proceedure(damage * dMultP);
            if (shealth <= 0)
                killedByPhys = true;
        }

        damageCD = maxDamageCD;
    }

    public void TakeDamageRanged(float damage)
    {
        if (damageCD <= 0)
        {
            Proceedure(damage * dMultR);
            if (shealth <= 0)
                killedByRang = true;
        }
        
        damageCD = maxDamageCD;
    }

    public void TakeDamagePit(float damage)
    {
        shealth -= damage;
    }

    void Proceedure(float damage)
    {
        shealth -= damage;

        //combo
        combo.damageCount += damage;
        combo.isActive = true;
        combo.time = combo.maxTime;
        combo.TextOffset();      
    }

    public void ExplosionDamage(float explDamage)
    {
        if (damageCD <= 0)       
            shealth -= explDamage;      

        damageCD = maxDamageCD;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkDist);
    }

    void JumpSound()
    {
        pitch = Random.Range(1, 1.5f);
        sound.pitch = pitch;
        sound.Play();
    }

    void preSettings()
    {
        timeBtwJump = maxTimeBtwJump;
        allowedToDash = true;
        
        cache = GameObject.FindGameObjectWithTag("ObjectCache").GetComponent<ObjectCache>();
        eCache = GameObject.FindGameObjectWithTag("EffectCache").GetComponent<EffectCache>();

        Instantiate(eCache.GetFromCache(spawnEffect), transform.position, Quaternion.identity);
    }
}
