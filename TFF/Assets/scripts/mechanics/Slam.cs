using UnityEngine;

public class Slam : MonoBehaviour
{
    [Header("General")]
    public GameObject player;
    public float speed;

    //public GameObject SlamPos;
    [Header("Debug bools")]
    public bool canSlam;
    public bool isSlamming;

    [Header("Duration")]
    public float MaxTime;

    [Header("Gizmos")]
    public float slamRadius;
    public LayerMask whatIsEnemy;
    public float force;
    public bool drawGizmos;
    public Input key;

    [Header("Other")]
    public ParticleSystem particle;
    public Animator anim;
    public float maxBoostTime;

    float boostTime;
    Rigidbody2D rb;
    float time;
    bool canPlayParticle;
    plankBreak plank;
    [HideInInspector]
    public float verticalMagnitude;
    SettingsSave settings;


    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canPlayParticle = true;
        settings = FindObjectOfType<SettingsSave>();
    }

    void Update()
    {
        verticalMagnitude = rb.velocity.y;

        if (GetComponent<playercontroller>().isGrounded == false)
        {
            if (Input.GetKeyDown(settings.GetKeyFromCache("Slam")))
            {
                rb.velocity = Vector2.down * speed;
                canSlam = true;
            }          
        }

        if (GetComponent<playercontroller>().isGrounded == true)
        {
            if (canSlam)
            {
                isSlamming = true;
            }
        }

        if (isSlamming == true)
        {
            if (canPlayParticle)
            {
                particle.Play();                
                canPlayParticle = false;
                boostTime = maxBoostTime;
            }
            Collider2D[] enemiesToThrow = Physics2D.OverlapCircleAll(transform.position, slamRadius, whatIsEnemy);
            if (enemiesToThrow != null)
            {
                for (int i = 0; i < enemiesToThrow.Length; i++)
                {
                    if (enemiesToThrow[i].GetComponent<Rigidbody2D>() != null)
                    {
                        enemiesToThrow[i].GetComponent<Rigidbody2D>().velocity = Vector2.up * force;
                    }

                    if (i == enemiesToThrow.Length)
                    {
                        isSlamming = false;
                    }

                }
            }
            time += Time.deltaTime;
        }

        if (boostTime > 0)
        {
            boostTime -= Time.deltaTime;
            GetComponent<playercontroller>().JumpForce = 30;
        }

        if (time > MaxTime)
        {
            time = 0;
            canSlam = false;
            isSlamming = false;
            canPlayParticle = true;
            GetComponent<playercontroller>().JumpForce = 25;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, slamRadius);
        }
    }
}
