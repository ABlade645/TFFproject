using UnityEngine;

public class playercontroller : MonoBehaviour
{
	public bool canMove;
	public float moveInput;
	public float JumpForce;
	public float speed;
	public float acceleration;

	public GameObject Player;

	public ParticleSystem trail;
	public ParticleSystem particle;
	public ParticleSystem smoke;
	public ParticleSystem groundColl;

	public string jumpBoost;
	public float jbForce;

	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public bool inMinecart;

	Animator move;

	private bool facingRight = true;

	public bool isGrounded;
	public Transform groundCheck;

	public float checkRadius;
	public LayerMask whatIsGround;
	bool canJumpOR = true;

	int extraJumps;
	public int extraJumpsValue;

	float ObjSpeed;
	float direction;

	RaycastHit2D hit;
	RaycastHit2D hitS;
	float distance = 1.1f;
	float slideSP = 5;
	Vector3 rayPos = new Vector3(0,0,0);
	float jumpTimer;
	public float maxJumpTimer;
	public float maxTimeBeforeJump;
	public float timeBeforeJump;
    public float maxStunTime;
    public Dash script;
    public int storedCheckPointIndex;

    [HideInInspector]
    public bool hitStun;
    [HideInInspector]
    public bool isGrabbed;
	

    bool movingLeft;
    bool movingRight;
    float stunTime;
    Climbing climbing;

    private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		move = GetComponent<Animator>();
		climbing = GetComponent<Climbing>();
		canMove = true;
    }


	private void FixedUpdate()
	{
		if (canMove)
		{
			isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

			//movement direction----------------------
			moveInput = Input.GetAxis("Horizontal");

            if (script.isDashing == false && climbing.isClimbing == false && hitStun == false && isGrabbed == false && inMinecart == false)
			{
				rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
			}

			if (hitStun == true)
			{
				rb.freezeRotation = false;
				stunTime -= Time.deltaTime;
				if (stunTime <= 0)
				{
					stunTime = maxStunTime;
					hitStun = false;
					rb.freezeRotation = true;
					rb.rotation = 0;
				}
			}

			if (facingRight == false && moveInput > 0)
			{
				if (isGrounded == true)
				{
					particle.Play();
					facingRight = !facingRight;
				}
			}
			else if (facingRight == true && moveInput < 0)
			{
				if (isGrounded == true)
				{
					particle.Play();
					facingRight = !facingRight;
				}
			}
		}	
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == jumpBoost && Input.GetKey(KeyCode.Space))
		{
			rb.velocity = Vector2.up * jbForce;
		}
	}

	public void Update()
	{
		if (canMove)
		{
            if (timeBeforeJump > 0)
            {
                timeBeforeJump -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeBeforeJump = maxTimeBeforeJump;
            }

            if (timeBeforeJump > 0)
            {
                if (isGrounded == true)
                {
                    rb.velocity = Vector2.up * JumpForce;
                }
            }

            if (moveInput > 0)
            {
                direction = 1;
            }

            if (moveInput < 0)
            {
                direction = -1;
            }

            var currentSp = rb.velocity.y;

            ObjSpeed = currentSp;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                smoke.Play();
            }

            jumpTimer -= Time.deltaTime;
            if (isGrounded == true)
            {
                jumpTimer = maxJumpTimer;
            }

            if (jumpTimer > 0)
            {
                extraJumps = extraJumpsValue;
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && jumpTimer > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
            }

            if (isGrounded == true)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    movingRight = true;
                }
                else if (Input.GetKeyUp(KeyCode.D))
                {
                    movingRight = false;
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    movingLeft = true;
                }
				else if (Input.GetKeyUp(KeyCode.A))
                {
                    movingLeft = false;
                }
            }
        }
    }

	void GroundColl()
    {
		groundColl.Play();
    }
    private void OnDrawGizmos()
    {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
