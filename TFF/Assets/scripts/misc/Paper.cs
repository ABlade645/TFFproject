using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    [Header("Genreal")]
    public bool autoFind;
    public bool autoMinScale;
    public bool aimAtCameraCenter;
    public GameObject obj;
    public GameObject endPosObj;
    public int layer;
    public GameObject Text;

    [Header("Calculation")]
    public float pushForce;
    public float speedMult;
    public float time;
    public float stopDist;
    public float maxTimeCD;
    float timeCD;

    [Header("Scaling")]
    public float maxScale;
    public float minScale;
    public float scalingSpeed;

    Rigidbody2D rb;
    Vector2 endPosVec;
    Vector2 startPosVec;
    GameObject player;
    BoxCollider2D coll;
    SpriteRenderer sprite;
    Animator anim;
    ParticleSystem particle;
    TrailRenderer trail;
    int startLayer;

    //bool states------------------------------------------
    bool isHit;
    public bool canInterract = true;
    bool followsCamera;
    bool returns;
    bool scales;
    bool scalesBack;

    bool onStart = true;


    void Load()
    {
        if (autoFind)
        {
            obj = gameObject;
        }

        //components----------------------------------------------
        player = GameObject.FindGameObjectWithTag("Player");
        rb = obj.GetComponent<Rigidbody2D>();
        startPosVec = obj.transform.position;
        coll = obj.GetComponent<BoxCollider2D>();
        sprite = obj.GetComponent<SpriteRenderer>();
        anim = obj.GetComponent<Animator>();
        particle = obj.GetComponentInChildren<ParticleSystem>();
        trail = obj.GetComponentInChildren<TrailRenderer>();
        //--------------------------------------------------------

        //bool States---------------------------------------------
        scalesBack = false;
        scales = false;
        returns = false;
        followsCamera = false;
        isHit = false;
        trail.emitting = false;
        Text.SetActive(false);
        //--------------------------------------------------------

        startLayer = sprite.sortingOrder;
        timeCD = maxTimeCD;

        if (autoMinScale)
        {
            minScale = obj.transform.localScale.x;
        }
 
        if(!aimAtCameraCenter)
        {
            endPosVec = endPosObj.transform.position;
        }       
    }

    private void Update()
    {
        if (aimAtCameraCenter)
        {
            endPosVec = Camera.main.transform.position;
        }       

        if (onStart)
        {
            onStart = false;
            Load();
        }

        if (endPosVec != null && isHit == false && canInterract)
        {
            rb.AddForce((obj.transform.position - player.transform.position).normalized * pushForce);
            isHit = true;
            canInterract = false;
        }

        if (scalesBack == false && returns == false)
        {
            if (isHit)
            {
                Timer();

                time += Time.deltaTime;

                rb.velocity += (endPosVec - (Vector2)obj.transform.position).normalized * speedMult * time;

                if (Vector2.Distance((Vector2)obj.transform.position, endPosVec) < stopDist && timeCD <= 0)
                {
                    isHit = false;
                    followsCamera = true;
                    rb.velocity = Vector2.zero;
                    timeCD = maxTimeCD;
                    trail.emitting = false;
                    Text.SetActive(true);
                }

                if (scales == false)
                {
                    scales = true;
                }

                if (coll.enabled)
                {
                    coll.enabled = false;
                }

                if (sprite.sortingOrder != layer)
                {
                    sprite.sortingOrder = layer;
                    anim.CrossFade("PaperSpin", 0);
                    particle.Play();
                    trail.emitting = true;
                }
            }
        }

        //scaling---------------------------------------------------------------------------------------------
        if (scales)
        {
            obj.transform.localScale = Vector3.MoveTowards(obj.transform.localScale, new Vector3(maxScale, maxScale, 0), scalingSpeed * Time.deltaTime);
            if (obj.transform.localScale.x >= maxScale)
            {
                obj.transform.localScale = new Vector3(maxScale, maxScale, 0);
                scales = false;
            }
        }

        if (scalesBack)
        {
            obj.transform.localScale = Vector3.MoveTowards(obj.transform.localScale, new Vector3(minScale, minScale, 0), scalingSpeed * Time.deltaTime);
            if (obj.transform.localScale.x <= minScale)
            {
                obj.transform.localScale = new Vector3(minScale, minScale, 0);
                scalesBack = false;
                coll.enabled = true;
                sprite.sortingOrder = startLayer;
            }
        }
        //----------------------------------------------------------------------------------------------------

        if (followsCamera)
        {      
            if ((Vector2)obj.transform.position != endPosVec)
            {
                obj.transform.position = endPosVec;
            }

            if (Input.GetKey(KeyCode.Q) && scales == false)
            {
                followsCamera = false;
                returns = true;
                scalesBack = true;
                Text.SetActive(false);
            }
        }     

        if (returns)
        {
            Timer();

            float sqrDist = (startPosVec - (Vector2)obj.transform.position).sqrMagnitude;
            obj.transform.position = Vector2.MoveTowards((Vector2)obj.transform.position, startPosVec, speedMult * time);
            if (sqrDist < stopDist * stopDist && timeCD <= 0)
            {
                returns = false;
                rb.velocity = Vector2.zero;
                time = 0;
                timeCD = maxTimeCD;

            }           
        }
    }

    void Timer()
    {
        if (timeCD > 0)
        {
            timeCD -= Time.deltaTime;
        }
    }

    public void Trigger()
    {
        canInterract = true;
    }
}
