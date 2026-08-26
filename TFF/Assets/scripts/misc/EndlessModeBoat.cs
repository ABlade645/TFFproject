using System.Collections;
using UnityEngine;

public class EndlessModeBoat : MonoBehaviour
{
    public GameObject[] islands;
    public float boatSpeed;
    public float boatAcceleration;
    public float stoppingDistance;

    float currentSpeed;

    playercontroller playerScript;
    GameObject player;
    bool canCheck;
    public bool isSailing;

    Rigidbody2D rb;

    void Update()
    {

        islands = GameObject.FindGameObjectsWithTag("arrivePoint");

        if (canCheck)
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
            }

            
            if (playerScript == null)
            {
                playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (playerScript != null)
            {
                StartCoroutine("StartSailing");

            }

            canCheck = false;
        }

        if (isSailing)
        {
            if (currentSpeed < boatSpeed)
            {
                currentSpeed += boatAcceleration * Time.deltaTime;
            }

            if (currentSpeed > boatSpeed)
            {
                currentSpeed = boatSpeed;
            }
            rb.velocity = Vector2.right * boatSpeed * Time.deltaTime;

            player.GetComponent<Rigidbody2D>().velocity += rb.velocity;

            if (Vector2.Distance(transform.position, islands[islands.Length - 1].transform.position) < stoppingDistance)
            {
                isSailing = false;
                playerScript.canMove = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canCheck = true;
            islands = GameObject.FindGameObjectsWithTag("arrivePoint");
        }
    }

    IEnumerator StartSailing()
    {
        playerScript.canMove = false;
        isSailing = true;

        yield return new WaitForSeconds(0);
    }
}
