using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ClimbingTextAnim : MonoBehaviour
{
    public PlayableDirector timeline;
    bool canPlay;

    private void Start()
    {
        canPlay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canPlay)
        {
            timeline.Play();
            canPlay = false;
        }
    }
}
