using UnityEngine;

public class leftHand : MonoBehaviour
{
    AudioSource source;
    float pitch;

    public float maxKnockbackForce;
    public float knockbackForce;
    public float chargeSpeed;
    bool canCharge;

    Animator timeline;
    bool animationPlaying;

    public float timeBtwAttack;
    public float startTimeBtwAttack;
    float timeBtwThrow;
    public float startTimeBtwThrow;

    public Transform attackPos;
    public Transform stoneVector;
    public float attackDistance;
    public LayerMask whatIsEnemy;
    public float damage;
    public string stoneName;
    public GameObject smolStone;
    public bool isDestroyed;

    public GameObject stoneObject;
    int stoneH;
    public bool canThrow;
    public bool restrictedStone;
    Collider2D enemy;

    public float force;
    public float maxForce;
    public float forceMultiplier;

    public GameObject powerBar;
    GameObject player;

    ObjectCache cache;
    SettingsSave settings;

    bool canAttack;
    Collider2D[] enemiesToDamage;
    int i;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        timeline = GetComponent<Animator>();
        stoneH = stoneObject.GetComponent<Stone>().stoneH;
        restrictedStone = false;
        powerBar.transform.localScale = new Vector3(0, powerBar.transform.localScale.y, powerBar.transform.localScale.z);
        canAttack = true;

        cache = GameObject.FindGameObjectWithTag("ObjectCache").GetComponent<ObjectCache>();
        settings = FindObjectOfType<SettingsSave>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stoneH <= 0 && GameObject.FindGameObjectWithTag("Stone"))
        {
            isDestroyed = true;
        }
        else
        {
            isDestroyed = false;
        }

        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }

        //Stone creating
        if (canThrow == true)
        {
            if (Input.GetKey(settings.GetKeyFromCache("Rock Throw")))
            {
                force += forceMultiplier * Time.deltaTime;
                powerBar.transform.localScale += new Vector3((forceMultiplier * Time.deltaTime)/ 150, 0, 0);
                if (powerBar.transform.localScale.x > 0.5)
                {
                    powerBar.transform.localScale = new Vector3(0.5f, powerBar.transform.localScale.y, powerBar.transform.localScale.z);
                }
            }

            if (Input.GetKeyUp(settings.GetKeyFromCache("Rock Throw")))
            {
                for (int i = 0; i < 1; i++)
                {
                    Instantiate(cache.GetFromCache(stoneName), stoneVector.transform.position, Quaternion.identity);
                }
                Invoke("Nullifier", 0.2f);
                timeBtwThrow = startTimeBtwThrow;
                canThrow = false;
                powerBar.transform.localScale = new Vector3(0, powerBar.transform.localScale.y, powerBar.transform.localScale.z);
            }

            if (force > maxForce)
            {
                force = maxForce;
            }
        }

        if (timeBtwThrow > 0)
        {
            timeBtwThrow -= Time.deltaTime;
        }

        if (timeBtwThrow <= 0)
        {
            if (restrictedStone == false)
            {
                canThrow = true;
            }
        }
       
        //Punch attack------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyUp(settings.GetKeyFromCache("Punch")))
        {
            canCharge = false;
            Invoke("ChargeNullifier", 0.3f);
            if (timeBtwAttack <= 0)
            {
                Invoke("PlayAnim", 0);
                timeBtwAttack = startTimeBtwAttack;
                canAttack = true;
                animationPlaying = true;
            }
        }

        if (animationPlaying == true)
        {
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackDistance, whatIsEnemy);
            foreach (Collider2D o in enemiesToDamage)
            {
                enemy = o.GetComponent<Collider2D>();
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (i == 0)
                    {
                        SoundImpact();
                    }                   
                }

                //attack
                var damageable = o.GetComponent<IDamagable>();
                if (damageable != null)
                {
                    damageable.TakeDamagePhysical(damage);
                    if (o.GetComponent<Rigidbody2D>() != null)
                    {
                        o.GetComponent<Rigidbody2D>().AddForce((o.GetComponent<Transform>().position - player.transform.position).normalized * knockbackForce);
                    }   
                    RepeatingAction();
                }

                switch (enemy.tag)
                {
                    case "powerCore":
                        enemy.GetComponent<LeverBool>().Activate();
                        RepeatingAction();
                        break;
                    case "Interractable":
                        enemy.GetComponent<Paper>().Trigger();
                        RepeatingAction();
                        break;
                }
            }
        }

        //punch charge
        if (Input.GetKeyDown(settings.GetKeyFromCache("Punch")))
        {
            canCharge = true;
        }

        if (canCharge)
        {
            knockbackForce += chargeSpeed * Time.deltaTime;
            if (knockbackForce >= maxKnockbackForce)
            {
                knockbackForce = maxKnockbackForce;
            }
        }

        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void RepeatingAction()
    {
        knockbackForce = 0;
        canAttack = false;
        AnimTurnOff();
    }

    void PlayAnim()
    {
        timeline.CrossFade("FistPunch", 0);
    }

    void AnimTurnOff()
    {
        animationPlaying = false;
    }

    void Nullifier()
    {
        force = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackDistance);
    }

    void SoundImpact()
    {
        pitch = Random.Range(1, 1.5f);
        source.pitch = pitch;
        //source.volume = 0.5f;
        source.Play();
    }

    void ChargeNullifier()
    {
        knockbackForce = 0;
    }
}
