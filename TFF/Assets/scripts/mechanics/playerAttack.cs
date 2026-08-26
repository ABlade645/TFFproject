using UnityEngine;
using UnityEngine.Playables;

public class playerAttack : MonoBehaviour
{
    public AudioClip swing;
    public AudioClip impact;
    AudioSource source;
    float pitch;

    float timeBtwAttack;
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
    bool makeSound;

    public GameObject hitMark;
    public hitMarkPooling hitMarkPool;
    bool canSpawnHitMark;
    Collider2D[] enemiesToDamage;
    SettingsSave settings;

    void Start()
    {
        slash = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        slashIndex = 1;
        canAttack = true;
        canCharge = true;
        settings = FindObjectOfType<SettingsSave>();
        hitMarkPool = FindObjectOfType<hitMarkPooling>();
    }

    void Update()
    {
        if (hasRootBlade)
        {
            if (timeBtwAttack > 0)
                timeBtwAttack -= Time.deltaTime;

            if (resetTime > 0)
                resetTime -= Time.deltaTime;

            if (resetTime <= 0 && slashIndex != 1)
                slashIndex = 1;

            if (isAttacking && canAttack)
            {
                canAttack = false;
                makeSound = true;

                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackDistance, whatIsEnemy);
                foreach (Collider2D o in enemiesToDamage)
                {
                    enemy = o.GetComponent<Collider2D>();

                    //impact sound
                    if (makeSound)
                    {
                        SoundImpact();
                        makeSound = false;
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
                            Stone stone = enemy.GetComponent<Stone>();
                            stone.rb.velocity = Vector2.down * 25;
                            stone.damage *= 1.2f;
                            stone.numberOfRic--;
                            stone.comboParticle.Play();
                            break;
                        case "powerCore":
                            enemy.GetComponent<LeverBool>().Activate();
                            RepeatingAction();
                            break;
                    }
                }            
            }

            //attack animation module
            if (Input.GetKeyUp(settings.GetKeyFromCache("Weapon Attack")) && timeBtwAttack <= 0)
            {
                timeBtwAttack = startTimeBtwAttack;
                SoundSwing();

                Invoke("RemoveHitMark", 1);

                switch (slashIndex)
                {
                    case 1:
                        if (strongAttack)
                            slash.CrossFade("strongSlash", 0);
                        else
                            slash.CrossFade("Slash", 0);
                        break;

                    case 2:
                        if (strongAttack)
                            slash.CrossFade("strongSlash2", 0);
                        else
                            slash.CrossFade("Slash2", 0);
                        break;

                    case 3:
                        if (strongAttack)
                            slash.CrossFade("strongSlash3", 0);
                        else
                            slash.CrossFade("Slash3", 0);
                        break;
                }

                canAttack = true;

                Invoke("StopAnim", 0.3f);
                Invoke("AttackIndex", 0.35f);
                resetTime = maxResetTime;
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
                canCharge = true;
        }      
    }

    
    //Repeating Action reference-----------------------------------
    void RepeatingAction()
    {
        hitMarkPool.SetHitmark();        
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
            Destroy(GameObject.FindGameObjectsWithTag("hitMark")[i]);       
    }

    //attack variation
    void AttackIndex()
    {
        slashIndex++;

        if (slashIndex > 3)       
            slashIndex = 1;      
    }

    //animation
    void StopAnim()
    {
        slash.CrossFade("Slash_idle", 0);
        strongAttack = false;
        holdTime = maxHoldTime;
        
        canSpawnHitMark = true;
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
}

//Attacking interface reference--------------------------------
public interface IDamagable
{
    void TakeDamagePhysical(float damage);
    void TakeDamageRanged(float damage);
    void TakeDamagePit(float damage);
}

