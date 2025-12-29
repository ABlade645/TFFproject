using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class minecartPath : MonoBehaviour
{
    [Header("General")]
    public bool autoFind;
    public GameObject minecart;
    public float maxSpeed;
    public float accelerationTime;
    public float deltaSpeed;
    public float actDist;
    public float maxCdTime;
    public float rotationOffset;
    public float rotationSpeed;
    public Transform playerPos;

    [Header("Pop-Up")]
    public GameObject popUp;
    public PlayableDirector show;
    public PlayableDirector disable;
    public Transform position;

    GameObject player;
    float speed;
    bool moves;
    float rotateZ;

    float cdTime;   

    [Header("Path")]
    public bool drawGizmos;
    public Vector2[] path;

    [Header("Misc/Debug")]
    public float movingMode;
    public int j = 1;

    void Start()
    {
        if (autoFind)
        {
            minecart = GameObject.Find("minecart");
            cdTime = maxCdTime;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        movingMode = 0;
        //moves = true;
    }

    void Update()
    {
        //popUp-----------------------------------------------------------
        if (popUp.activeSelf)
        {
            if (popUp.transform.position != position.position)
            {
                popUp.transform.position = position.position;
            }
        }

        //moving mode check------------------------------------------------------------
        if (movingMode > 1)
        {
            movingMode = 1;
        }

        if (movingMode < -1)
        {
            movingMode = -1;
        }

        //cooldown---------------------------------------------------
        if (cdTime > 0)
        {
            cdTime -= Time.deltaTime;
        }

        if (j  == path.Length && minecart.transform.rotation != new Quaternion(0, 0, 0, 0))
        {
            minecart.transform.rotation = new Quaternion(0,0,0,0);
        }

        //movement/velocity-------------------------------------------
        if (moves)
        {
            if (speed < maxSpeed * movingMode)
            {
                speed = accelerationTime * deltaSpeed;
            }
            if (speed > maxSpeed * movingMode)
            {
                speed = maxSpeed * movingMode;
            }


            if (movingMode > 0)
            {
                minecart.transform.position = Vector2.MoveTowards(minecart.transform.position, path[j + 1], speed);
                player.transform.position = playerPos.position;
                if (Vector2.Distance(minecart.transform.position, path[j + 1]) <= actDist && cdTime <= 0)
                {
                    cdTime = maxCdTime;
                    j++;
                }

                //look at next node------------------------
                Vector3 difference = new Vector3(path[j + 1].x, path[j + 1].y, 0) - minecart.transform.position;
                rotateZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
                transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotationOffset + 1.44f);
            }

            if (movingMode < 0)
            {
                minecart.transform.position = Vector2.MoveTowards(minecart.transform.position, path[j - 1], -speed);
                player.transform.position = playerPos.position;
                if (Vector2.Distance(minecart.transform.position, path[j - 1]) <= actDist && cdTime <= 0)
                {
                    cdTime = maxCdTime;
                    j--;
                }

                //look at next node------------------------
                Vector3 difference = new Vector3(path[j - 1].x, path[j - 1].y, 0) - minecart.transform.position;
                rotateZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
                transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotationOffset - 178.56f);
            }
        }
    }

    //buttons-------------------------------------------------
    public void SForward()
    {
        movingMode += 0.5f;
    }
    public void FForward()
    {
        movingMode += 1;
    }
    public void SBackward()
    {
        movingMode -= 0.5f;
    }
    public void FBackward()
    {
        movingMode -= 1;
    }

    //collision activation-----------------------------------------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moves = true;
            player.GetComponent<playercontroller>().inMinecart = true;
            popUp.SetActive(true);
            show.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<playercontroller>().inMinecart = false;
            moves = false;
            disable.Play();
            Invoke("DisablePopUp", 0.5f);
        }
    }

    void DisablePopUp()
    {
        popUp.SetActive(false);
    }

    //gizmos--------------------------------------------------------------------------
    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.Length - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }

            for (int i = 0; i < path.Length - 1; i++)
            {
                Gizmos.DrawWireSphere(path[i], actDist);
            }
        }
    }
}
