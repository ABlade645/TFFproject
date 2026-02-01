using UnityEngine;

public class zztAI : MonoBehaviour
{
    [Header("General")]
    public GameObject target;
    public Rigidbody2D rb;
    public LayerMask whatIsGround;

    [Header("Options")]
    public float maxTimeBtwAttack;
        
    public int[] attackWeight;
    public float groundCheckDistance;

    zztAttacks attacks;
    zztStates states;
    zztPathExecuter tentacles;

    float maxWaitTime;
    float waitTime;
    float timeBtwAttack;

    [HideInInspector]
    public int attackIndex; //0 - idle
    bool hasSpawned;
    
    public bool groundCheck;


    private void Update()
    {
        if (!hasSpawned)
            Setup();

        if(timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
            if(!states.idle)
                states.idle = true;
        }

        if (timeBtwAttack <= 0 && states.idle)
        {
            states.idle = false;
            
            attackIndex = Randomizer();

            waitTime = maxWaitTime;
            AttackDistributor(attackIndex);           
        }

        if (waitTime > 0)
            waitTime -= Time.deltaTime;
        else
            RethinkYourLifeExistance();

        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        if(ray)
             groundCheck = true;
        else
            groundCheck = false;
    }

    int Randomizer()
    {
        int result = 0, total = 0, value;

        for (int i = 0; i < attackWeight.Length; i++)
            total += attackWeight[i];

        value = Random.Range(0, total);

        int temp = 0;

        for (int i = 0; i < attackWeight.Length; i++)
        {
            temp += attackWeight[i];
            if (value < temp)
            {
                result = i;
                break;
            }             
        }
            
        return result;
    }

    void AttackDistributor(int value)
    {
        switch (value)
        {
            case 0:
                break;

            case 1:
                while(waitTime > 0)
                {
                    waitTime -= Time.deltaTime;
                    if(groundCheck)
                    {
                        attacks.Jump();
                        break;
                    }                
                }
                break;

            case 2:
                tentacles.Prepare(0);
                break;

            default:
                Debug.Log("ZZT AI: attack does not exist");
                break;
        }

        ResetTimer();
    }

    public void ResetTimer()
    {
        timeBtwAttack = maxTimeBtwAttack;

        states.idle = false;
    }

    public void RethinkYourLifeExistance() //free will goes brrrrrrrrrrrrrrrrrrrrrr
    {
        timeBtwAttack = 0;
        states.idle = true;
    }

    void Setup()
    {
        maxWaitTime = maxTimeBtwAttack * 2;

        if(target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();

        attacks = GetComponent<zztAttacks>();
        attacks.Setup();
        states = GetComponent<zztStates>();
        tentacles = GetComponent<zztPathExecuter>();

        timeBtwAttack = maxTimeBtwAttack;
        hasSpawned = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, Vector2.down * groundCheckDistance + (Vector2)transform.position);
    }

    /*
    void OnDrawGizmos()
    {
        
    }
    */
}
