using UnityEngine;
using UnityEngine.Playables;
using System.IO;

public class LilFellaEvent : MonoBehaviour
{
    PlayableDirector timeline;
    HandEvent hand;
    byte count = 0;

    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        hand = FindObjectOfType<HandEvent>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && hand.triggered && count < 1)
            count++;
        else if (count == 1)
        {
            Invoke("Launch", 5);
            timeline.Play();
        }
    }

    void Launch()
    {
        System.Diagnostics.Process.Start(Path.Combine(Application.dataPath, "StreamingAssets\\A_Message\\A_Message.exe"));
        Application.Quit();
    }
}
