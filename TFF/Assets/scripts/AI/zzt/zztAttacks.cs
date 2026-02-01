using System.Collections;
using UnityEngine;

public class zztAttacks : MonoBehaviour
{
    [Header("General")]
    public zztAI brain;
    public zztStates states;

    Rigidbody2D rb;

    [Header("Jump")]
    public bool drawJumpGizmos;
    public float jumpHeight;
    public float jumpForce;

    [Header("Slam")]
    public bool drawSlamGizmos;
    public float slamRadius;
    public float slamForce;
    public float slamOffset;

    public void Setup()
    {
        rb = brain.rb;

        if(brain == null)
            brain = GetComponent<zztAI>();
        if(states == null)
            states = GetComponent<zztStates>();
    }

    public void Jump()
    {
        StartCoroutine("JumpCoroutine");
    }


    //1
    IEnumerator JumpCoroutine()
    {
        states.isJumping = true;
        Vector2 contactPoint;
        float slamTime = 0.1f; 

        if (brain.groundCheck)
        {
            rb.velocity = Vector2.down * jumpForce/2;
            yield return new WaitForSeconds(0.2f);
            rb.velocity = new Vector2(brain.target.transform.position.x - transform.position.x, jumpHeight + brain.rb.gravityScale).normalized * jumpForce;
        }

        yield return new WaitForSeconds(2f);

        while(slamTime > 0)
        {
            rb.velocity = Vector2.down * jumpForce * 2.5f;
            slamTime -= Time.deltaTime;
        }          

        if (brain.groundCheck)
        {
            Collider2D[] slam = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + slamOffset), slamRadius);
            foreach (Collider2D o in slam)
            {
                Rigidbody2D rb = o.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.velocity = Vector2.up * slamForce;
            }
        }

        states.isJumping = false;
        yield return null;
    }

    void OnDrawGizmos()
    {
        if (drawJumpGizmos)
        {
            Gizmos.color = Color.red;
            Vector2 newVec = new Vector2(brain.target.transform.position.x - transform.position.x, jumpHeight + brain.rb.gravityScale).normalized * jumpForce + (Vector2)transform.position;
            Vector2 horizVec = new Vector2(newVec.x, transform.position.y);

            Gizmos.DrawLine(transform.position, newVec);
            Gizmos.DrawLine(transform.position, horizVec);
            Gizmos.DrawLine(new Vector2(brain.target.transform.position.x - transform.position.x, jumpHeight + brain.rb.gravityScale).normalized * jumpForce + (Vector2)transform.position, horizVec);
        }

        if(drawSlamGizmos)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + slamOffset), slamRadius);
        }
    }
}
