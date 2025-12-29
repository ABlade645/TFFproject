using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LevelSelect : MonoBehaviour
{
    public GameObject info;
    bool canEnter;
    public string scene;
    public PlayableDirector timelineUP;
    public PlayableDirector timelineDOWN;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timelineUP.Play();
            canEnter = true;
            info.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timelineDOWN.Play();
            canEnter = false;
            info.SetActive(false);
        }
    }

    private void Update()
    {
        if (canEnter == true && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
