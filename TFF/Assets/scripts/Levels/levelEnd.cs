using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class levelEnd : MonoBehaviour
{
    public GameObject Object;
    public string scene;
    public ParticleSystem particle;

    public PlayableDirector Timeline;
    public GameObject Sheet;

    public AudioSource stop;
    public AudioSource start;

    public bool canPass;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Sheet.SetActive(true);
            Sheet.GetComponent<PlayableDirector>().Play();
            //SceneManager.LoadScene(scene);
            Invoke("timeline", 0.5f);            
        }
    }

    void Update()
    {
        if (canPass == true)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(scene);
            }
        }        
    }

    public void timeline()
    {
        stop.Stop();
        start.Play();
        Object.SetActive(true);
        Timeline.Play();
        particle.Play();
        canPass = true;
    }
}
