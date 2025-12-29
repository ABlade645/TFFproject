using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zztAI : MonoBehaviour
{
    public float maxTimeBtwAttack;

    public GameObject[] tentacle;
    public GameObject pose;

    public float grabDist;
    public float interpolationTime;

    public Vector3 grabPrepare;

    public float timeBtwAttack;
    public float targetDist;

    public float forceY;
    public float forceX;

    public float gCheckRad;
    public LayerMask ground;
    public LayerMask whatIsPlayer;

    public GameObject TemporaryPoint;
    public GameObject slamParticle;

    public GameObject alcInstPoint;
    public GameObject bullet;
    public float shootInterval;
    public int timesToShoot;
    public int roundsAmount;

    public float slamForce;

    int attackIndex;

    GameObject player;
    bool isAttacking;

    //bool states
    bool isGrabbing;
    bool playerDrag;
    bool canDeactiveJTent;
    bool isSlamming;


    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, gCheckRad, ground);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (isAttacking == false)
        {
            if (timeBtwAttack > 0)
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }

        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = maxTimeBtwAttack;
            //attackIndex = Random.Range(1, 3);
            attackIndex = 0;
            GrabSequence();
        }
        
        switch (attackIndex)
        {
            case 1:
                //GrabSequence();
                StartCoroutine("AlcRounds");
                attackIndex = 0;
                break;
            case 2:                
                if (hit != null)
                {
                    StartCoroutine("JumpCoroutine");
                }
                attackIndex = 0;
                break;
        }

        if (hit != null && canDeactiveJTent == true)
        {
            if (isSlamming == true)
            {           
                //bug: function works only if player is nearby
                Collider2D[] slam = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y - 2.5f), gCheckRad * 1.5f, whatIsPlayer);
                for (int i = 0; i < slam.Length; i++)
                {
                    if (slam[i] == player.GetComponent<Collider2D>())
                    {
                        Instantiate(slamParticle, new Vector2(transform.position.x, transform.position.y - 2.5f), Quaternion.identity);
                        player.GetComponent<playercontroller>().hitStun = true;
                        player.GetComponent<Rigidbody2D>().velocity = Vector2.up * slamForce;

                        isSlamming = false;
                        tentacle[3].SetActive(false);
                        tentacle[4].SetActive(false);
                        canDeactiveJTent = false;
                    }

                    if (slam[i] != player.GetComponent<Collider2D>())
                    {
                        Instantiate(slamParticle, new Vector2(transform.position.x, transform.position.y - 2.5f), Quaternion.identity);
                        isSlamming = false;
                        tentacle[3].SetActive(false);
                        tentacle[4].SetActive(false);
                        canDeactiveJTent = false;
                    }
                }                
            }

            if (isSlamming == false)
            {
                tentacle[3].SetActive(false);
                tentacle[4].SetActive(false);
                canDeactiveJTent = false;
            }
        }

        //grab sequence-------------------------------------------
        if (isGrabbing)
        {
            //GrabSlamSequence();
        }
    }

    //jump------------------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator JumpCoroutine()
    {
        isAttacking = true;        
        GetComponent<Rigidbody2D>().velocity = (Vector3.up * forceY + (player.transform.position - transform.position).normalized * forceX);
        tentacle[3].SetActive(true);
        tentacle[4].SetActive(true);
        Instantiate(TemporaryPoint, tentacle[3].GetComponent<InversedKinematicsLeg>().targetPoint.transform.position, Quaternion.identity);
        Instantiate(TemporaryPoint, tentacle[4].GetComponent<InversedKinematicsLeg>().targetPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        tentacle[3].GetComponent<InversedKinematicsLeg>().objectToConnect = GameObject.FindGameObjectsWithTag("TemporaryZZTPos")[0];
        tentacle[4].GetComponent<InversedKinematicsLeg>().objectToConnect = GameObject.FindGameObjectsWithTag("TemporaryZZTPos")[1];
        yield return new WaitForSeconds(0.9f);

        tentacle[3].GetComponent<InversedKinematicsLeg>().objectToConnect = GameObject.FindGameObjectWithTag("002");
        tentacle[4].GetComponent<InversedKinematicsLeg>().objectToConnect = GameObject.FindGameObjectWithTag("002");
        canDeactiveJTent = true;
        yield return new WaitForSeconds(0.5f);

        GameObject[] tempPos = GameObject.FindGameObjectsWithTag("TemporaryZZTPos");
        for (int i = 0; i < tempPos.Length; i++)
        {
            Destroy(tempPos[i]);
        }
        int probability = Random.Range(1, 4);
        switch (probability)
        {
            case 1:
                GetComponent<Rigidbody2D>().velocity = (Vector3.down * (forceY * 2));
                isSlamming = true;
                break;
        }      
        isAttacking = false;
    }

    //alcaline rounds------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator AlcRounds()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0);

        for (int i = 0; i < roundsAmount; i++)
        {
            for (int k = 0; k < timesToShoot; k++)
            {
                Instantiate(bullet, alcInstPoint.transform.position, alcInstPoint.GetComponentInParent<Transform>().rotation);
                yield return new WaitForSeconds(shootInterval);
            }

            yield return new WaitForSeconds(shootInterval * 10);
        }
        
        isAttacking = false;
    }


    //grab attack-----------------------------------------------------------------------------------------------------------------------------------------------------------
    void GrabSequence()
    {

        if (Vector2.Distance(player.transform.position, pose.transform.position) < grabDist)
        {
            //tentacle.SetActive(true);
            //isGrabbing = true;
        }
    }

    void GrabSlamSequence()
    {

        InversedKinematicsLeg temporaryConnector;
        temporaryConnector = tentacle[0].GetComponent<InversedKinematicsLeg>();

        if (temporaryConnector.objectToConnect == null)
        {
            temporaryConnector.objectToConnect = player;
        }

        if (Vector2.Distance(player.transform.position, temporaryConnector.line[1].transform.position) < grabDist && player.GetComponent<playercontroller>().isGrabbed == false)
        {
            player.GetComponent<playercontroller>().isGrabbed = true;

            temporaryConnector.objectToConnect = GameObject.FindGameObjectWithTag("IK followPoint");
            temporaryConnector.objectToConnect.transform.position = temporaryConnector.line[1].transform.position;
        }

        if (player.GetComponent<playercontroller>().isGrabbed == true) //&& Vector2.Distance(temporaryConnector.line[1].transform.position, grabPrepare + transform.position) > targetDist)
        {
            playerDrag = true;
            
            temporaryConnector.objectToConnect.transform.position = Vector3.Slerp(temporaryConnector.objectToConnect.transform.position, grabPrepare + transform.position, interpolationTime * Time.deltaTime);
        }

        if (playerDrag == true)
        {
            player.transform.position = temporaryConnector.line[1].transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gCheckRad);
    }
}
