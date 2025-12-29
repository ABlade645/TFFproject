using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UnderneathFallCutscene : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Animator>().enabled = true;
            timeline.Play();
        }
    }
}
