using UnityEngine;

public class Stone : MonoBehaviour
{
    [Header("General")]
    public GameObject stone;
    public ParticleSystem particle;
    public Collider2D enemy;
    public GameObject stoneSmol;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsWall;
    public int numberOfRic;

    [HideInInspector]
    public GameObject Direction;
    [HideInInspector]
    public GameObject dirPos;

    [Header("Calculation")]
    public Transform attackPos;
    public float attackDistance;
    public float damage;
    public float forceModif;
    public float direction;
    public float force;
    public float pForce;
    public float ricoshetF;
    public float yRicoshetOffset;
    public int Sdamage;
    public int stoneH;
    public int stoneMH;
    public float rotSpeed;

    [Header("Time")]
    public float startTimeBtwAttack;
    public float timeBtwAttack;
    public float lifeTime;

    [Header("States")]
    public bool isPunched;
    bool isSpawned = true;

    float stonePos;
    Transform playerPos;
    [HideInInspector]
    public Rigidbody2D rb;
    Collider2D[] enemiesToDamage;
    GameObject hand;
    GameObject player;
    bool canBePunched;
    Vector2 newVelocity;
    bool isRotating;
    int rotDir;
    SettingsSave settings;

    public ParticleSystem comboParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isPunched = false;
        settings = FindObjectOfType<SettingsSave>();
    }

    private void Update()
    {
        if (hand == null)
        {
            hand = GameObject.Find("hand1");
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Direction == null)
        {
            Direction = GameObject.FindGameObjectWithTag("StoneDir");
        }

        if (dirPos == null)
        {
            dirPos = GameObject.FindGameObjectWithTag("dirPos");
        }

        Direction.transform.position = dirPos.transform.position;

        if (stoneSmol == null)
        {
            stoneSmol = GameObject.FindGameObjectWithTag("smolStone");
        }

        if(GameObject.FindGameObjectWithTag("Stone") && isSpawned == true)
        {
            force = hand.GetComponent<leftHand>().force;
            stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(5 * direction, force));
            isSpawned = false;
            stoneH = stoneMH;
        }

        //damage
        if (timeBtwAttack <= 0)
        {
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackDistance, whatIsEnemy);
            foreach (Collider2D o in enemiesToDamage)
            {
                enemy = o.GetComponent<Collider2D>();

                //attack
                var damageable = o.GetComponent<IDamagable>();
                if (damageable != null)
                {
                    damageable.TakeDamageRanged(damage);
                    RepeatingAction();
                }
            }            
        }

        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.position.x < +playerPos.position.x)
        {
            stonePos = -1;
        }
        else
        {
            stonePos = 1;
        }

        //stone dividing
        if (stoneH <= 0)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(stoneSmol, stone.transform.position, Quaternion.identity);
            }

            for (int i = 0; i < 1; i++)
            {
                Instantiate(stoneSmol, new Vector2(stone.transform.position.x + 0.1f, stone.transform.position.y) , Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (Input.GetKey(settings.GetKeyFromCache("Punch")) && canBePunched == false)
        {
            canBePunched = true;
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.Euler(0,0,rotSpeed * rotDir * Time.deltaTime);
        }
    }

    void RepeatingAction()
    {
        timeBtwAttack = startTimeBtwAttack;
        if (isPunched == true)
        {
            //ricoshet
            isPunched = false;
            damage /= 1.5f;
            rb.velocity = (Vector3.up * yRicoshetOffset + (player.transform.position - transform.position).normalized).normalized * ricoshetF;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //boost
        if (collision.gameObject.tag == "Lhand")
        {
            if (canBePunched)
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(Direction.transform.localPosition * pForce);
                particle.Play();
                isPunched = true;
                canBePunched = false;
                damage *= 1.5f;
                comboParticle.Play();
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //wall ricoshet----------------------------------------------------------------------------------
            if (numberOfRic > 0)
            {
                damage *= 2;
                comboParticle.Play();
                isRotating = true;
                rotDir = Random.Range(-1, 2);
                if (rotDir == 0)
                {
                    rotDir = 1;
                }
            }
            //-----------------------------------------------------------------------------------------------

            particle.Play();
            numberOfRic--;
            if (numberOfRic <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackDistance);
    }

    public void takeDamage(int sDamage)
    {
        stoneH -= sDamage;
    }    
}
