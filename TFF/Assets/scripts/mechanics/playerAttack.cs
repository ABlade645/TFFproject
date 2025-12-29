using UnityEngine;
using UnityEngine.Playables;

public class playerAttack : MonoBehaviour
{
    public AudioClip swing;
    public AudioClip impact;
    AudioSource source;
    float pitch;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackDistance;
    public float damage;
    public int sDamage;

    public Animator slash;
    public PlayableDirector timeline;
    public GameObject objects;

    public bool hasRootBlade;
    public GameObject RootBlade;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsRock;

    public float maxAttackTime;
    float attackTime;
    bool attacked;
    bool attacking;
    public bool isPlaying;
    bool canStop;

    public float maxResetTime;
    float resetTime;

    public int slashIndex;

    Collider2D enemy;
    public ParticleSystem particle;

    public float maxHoldTime;
    float holdTime;
    public bool strongAttack;
    public Animator strongCharge;
    bool canCharge = true;

    public bool isAttacking;
    bool canAttack;

    public GameObject hitMark;
    bool canSpawnHitMark;
    Collider2D[] enemiesToDamage;
    int i;
    SettingsSave settings;

    void Start()
    {
        slash = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        slashIndex = 1;
        canAttack = true;
        canCharge = true;
        settings = FindObjectOfType<SettingsSave>();
    }

    void Update()
    {
        if (hasRootBlade == true)
        {
            if (isAttacking == true && canAttack == true)
            {
                
                attackTime = maxAttackTime;
                timeBtwAttack = startTimeBtwAttack;
                if (true)
                {
                    enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackDistance, whatIsEnemy);
                    foreach (Collider2D o in enemiesToDamage)
                    {
                        enemy = o.GetComponent<Collider2D>();
                        for (i = 0; i < enemiesToDamage.Length; i++)
                        {
                            //impact sound
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
                            RepeatingAction();
                        }

                        switch (enemy.gameObject.tag)
                        {
                            case "Stone":
                                RepeatingAction();
                                enemy.GetComponent<Stone>().rb.velocity = Vector2.down * 25;
                                enemy.GetComponent<Stone>().damage *= 1.2f;
                                enemy.GetComponent<Stone>().numberOfRic--;
                                enemy.GetComponent<Stone>().comboParticle.Play();
                                break;
                            case "powerCore":
                                enemy.GetComponent<LeverBool>().Activate();
                                RepeatingAction();
                                break;
                        }
                    }
                }              
            }
            

            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
                attacking = true;
            }

            if (attackTime <= 0)
            {
                attacking = false;
            }

            if (resetTime > 0)
            {
                resetTime -= Time.deltaTime;
            }

            if (resetTime <= 0)
            {
                slashIndex = 1;
            }

            if (hasRootBlade == true)
            {
                //attack animation module
                if (Input.GetKeyUp(settings.GetKeyFromCache("Weapon Attack")))
                {
                    SoundSwing();

                    Invoke("RemoveHitMark", 1);

                    canStop = true;

                    if (slashIndex == 1 && attackTime <= 0)
                    {
                        if (strongAttack == true)
                        {
                            slash.CrossFade("strongSlash", 0);
                            Invoke("StopAnim", 0.3f);
                        }
                        else
                        {
                            slash.CrossFade("Slash", 0);
                        }
                        Invoke("AttackIndex", 0.35f);
                        
                        resetTime = maxResetTime;
                    }                    
                
                    if (slashIndex == 2 && attackTime <= 0)
                    {
                        if (strongAttack == true)
                        {
                            slash.CrossFade("strongSlash2", 0);
                            Invoke("StopAnim", 0.3f);
                        }
                        else
                        {
                            slash.CrossFade("Slash2", 0);
                        }
                        Invoke("AttackIndex", 0.35f);
                        
                        resetTime = maxResetTime;
                    }

                    if (slashIndex == 3 && attackTime <= 0)
                    {
                        if(strongAttack == true)
                        {
                            slash.CrossFade("strongSlash3", 0);
                            Invoke("StopAnim", 0.3f);
                        }
                        else
                        {
                            slash.CrossFade("Slash3", 0);
                        }
                        Invoke("AttackIndex", 0.35f);
                       
                        resetTime = maxResetTime;                        
                    }                    
                }

                if (isPlaying == false && canStop)
                {
                    StopAnim();                    
                }

                //Strong attack hold----------------------------------------------------------------
                if (Input.GetKey(settings.GetKeyFromCache("Weapon Attack")))
                {
                    holdTime -= Time.deltaTime;

                    if (holdTime <= 0)
                    {
                        strongAttack = true;
                        if (canCharge)
                        {
                            strongCharge.CrossFade("StrongChargeBurst", 0);
                            canCharge = false;
                        }                        
                    }
                }

                if (!canCharge && Input.GetKeyUp(settings.GetKeyFromCache("Weapon Attack")))
                {
                    canCharge = true;
                }
            }
        }
        timeBtwAttack -= Time.deltaTime;
    }

    
    //Repeating Action reference-----------------------------------
    void RepeatingAction()
    {
        GameObject.FindObjectOfType<hitMarkPooling>().SetHitmark();
        timeBtwAttack = startTimeBtwAttack;
        attackTime = 0;
        canAttack = false;
        //hitmark
        if (canSpawnHitMark)
        {
            Instantiate(hitMark, enemy.ClosestPoint(transform.position), Quaternion.identity);
            hitMark.GetComponent<Animator>().CrossFade("hitMark", 0);
            canSpawnHitMark = false;
        }
    }

    //hitmark removal
    void RemoveHitMark()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("hitMark").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("hitMark")[i]);
        }
    }

    //attack variation
    void AttackIndex()
    {
        slashIndex++;
        attackTime = maxAttackTime;
        if (slashIndex > 3)
        {
            slashIndex = 1;
        }
    }

    //animation
    void StopAnim()
    {
        slash.CrossFade("Slash_idle", 0);
        strongAttack = false;
        holdTime = maxHoldTime;
        canAttack = true;
        canSpawnHitMark = true;
        canStop = false;
    }

    //rootblade recieve (remove into a different script)
    public void GetRootBlade()
    {
        hasRootBlade = true;
        Destroy(RootBlade);
        objects.SetActive(true);
;       timeline.Play();
        particle.Play();
    }

    //gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackDistance);
    }

    //sounds
    void SoundSwing()
    {
        source.clip = swing;
        pitch = Random.Range(1, 1.5f);
        source.pitch = pitch;
        source.volume = 1;
        source.Play();
    }

    void SoundImpact()
    {
        source.clip = impact;
        pitch = Random.Range(1, 1.5f);
        source.pitch = pitch;
        source.volume = 0.5f;
        source.Play();
    }

    //save and load
    public void Save()
    {
        //SaveSystem.SaveData(this);
    }

    public void Load()
    {
        //SaveSystem.LoadData();
    }
}

//Attacking interface reference--------------------------------
public interface IDamagable
{
    void TakeDamagePhysical(float damage);
    void TakeDamageRanged(float damage);
}

