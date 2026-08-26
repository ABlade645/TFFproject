using UnityEngine;

public class Plants : MonoBehaviour, IDamagable
{
    [Header("Settings")]
    public bool collidable;
    public bool affectedByWind;
    public bool useseParticle; 

    [Header("General")]
    public GameObject prefab;
    public float collisionOffset;
    public float returnSpeed;
    public int windDir;
    public GameObject particle;

    Quaternion quaternion;
    Transform player;
    float health = 5;
    Animator anim;

    void Start()
    {
        quaternion = transform.localRotation;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        if (affectedByWind)
        {
            if (windDir == 1)
            {
                anim.CrossFade("Wind_Right", 0);
            }

            if (windDir == -1)
            {
                anim.CrossFade("Wind_Left", 0);
            }
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Break();
        }

        //Wind Affection-----------------------------------
        if (!affectedByWind)
        {
            anim.CrossFade("Wind_Idle", 0);
        }
        //-------------------------------------------------

        //Collision----------------------------------------
        if (collidable)
        {
            if (Quaternion.Angle(transform.rotation, Quaternion.identity) != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime);
            }
        }
        //-------------------------------------------------
    }

    public void TakeDamagePhysical(float damage)
    {
        health -= damage;
    }

    public void TakeDamageRanged(float damage)
    {
        health -= damage;
    }

    public void TakeDamagePit(float damage)
    {
        health -= damage;
    }

    void Break()
    {
        Instantiate(prefab, transform.position, quaternion);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (collidable)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                if (player.position.x > transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0,0, collisionOffset);
                }

                if (player.position.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, -collisionOffset);
                }
            }
        }

        if (useseParticle)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
