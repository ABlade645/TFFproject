using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Signals : MonoBehaviour
{
    public PlayableDirector playable;

    public void Continue()
    {
        playable.Play();
    }
}
