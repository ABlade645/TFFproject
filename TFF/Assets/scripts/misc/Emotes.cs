using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotes : MonoBehaviour
{
    Animator anim;
    public Animator rHand;
    public Animator lHand;
    public Animator face;

    GameObject player;
    GameObject cursor;

    public GameObject pointF;
    public GameObject pointS;

    public string[] emoteName;
    public float maxIdleTime;
    public float idleTime;
    bool canEmote;
    int random;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        canEmote = true;
    }

    void Update()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude == 0 && cursor.GetComponent<Rigidbody2D>().velocity.magnitude == 0 && canEmote == true)
        {
            if (idleTime > 0)
            {
                idleTime -= Time.deltaTime;
            }
        }

        if (idleTime <= 0)
        {
            pointF.GetComponent<handTurn>().lookAtCursor = false;
            pointS.GetComponent<handTurn>().lookAtCursor = false;
            anim.enabled = true;
            idleTime = maxIdleTime;
            canEmote = false;
            random = Random.Range(0, emoteName.Length);
            EmoteStart();
            pointF.transform.rotation = new Quaternion(0, 0, 0, 0);
            pointS.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (player.GetComponent<Rigidbody2D>().velocity.magnitude != 0 || cursor.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
        {
            Default();
            idleTime = maxIdleTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {            
            Default();
            idleTime = maxIdleTime;
        }
    }
   
    void EmoteStart()
    {
        anim.CrossFade(emoteName[random], 0);
        rHand.enabled = false;
        lHand.enabled = false;
        face.CrossFade(emoteName[random], 0);
        random = 0;
        pointF.transform.rotation = new Quaternion(0, 0, 0, 0);
        pointS.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    //pumped module
    void PumpedEmote()
    {
        anim.CrossFade("Pumped emote", 0);
    }

    //rockin emote
    void RockinEmote()
    {
        anim.CrossFade("Rockin' it emote", 0);
    }

    //actually emote
    void ActuallyEmote()
    {
        anim.CrossFade("Actually emote", 0);
    }

    //default module
    void Default()
    {
        anim.enabled = false;
        anim.CrossFade("Default emote", 0);
        rHand.enabled = true;
        lHand.enabled = true;
        face.CrossFade("FaceEmote", 0.4f);
        canEmote = true;
        Invoke("DisableAnimator", 0.1f);

        pointF.GetComponent<handTurn>().lookAtCursor = true;
        pointS.GetComponent<handTurn>().lookAtCursor = true;
    }

    void DisableAnimator()
    {
        //anim.enabled = false;
    }
}
