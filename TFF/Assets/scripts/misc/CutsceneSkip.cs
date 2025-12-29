using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneSkip : MonoBehaviour
{
    public PlayableDirector cutscene;
    public PlayableDirector cutsceneS;
    bool canSkip;

    void Start()
    {
        canSkip = true;
    }

    void Update()
    {
        if (canSkip == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cutscene.Stop();
                canSkip = false;
                cutsceneS.Play();
            }
        }
    }
}
