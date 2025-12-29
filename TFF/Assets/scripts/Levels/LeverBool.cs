using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LeverBool : MonoBehaviour
{
    public bool activated;
    Animator anim;
    public GameObject lever;
    public bool isColliding;
    public PlayableDirector timeline;
    public PlayableDirector cutscene;
    public float waitTime;
    public ParticleSystem particle;
    AudioSource clip;

    void Start()
    {
        activated = false;
        anim = GetComponent<Animator>();
        clip = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }

    void Update()
    {
        if (isColliding == true && Input.GetKeyDown(KeyCode.Q))
        {
            Activate();
        }
    }

    public void Activate()
    {
        activated = true;
        lever.GetComponent<SpriteRenderer>().sprite = null;
        timeline.Play();
        anim.CrossFade("none",0 , 0);
        particle.Play();
        clip.Play();
        Invoke("Cutscene", waitTime);
    }

    void Cutscene()
    {
        cutscene.Play();
    }
}
