using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class levelSelectToMenu : MonoBehaviour
{
    public PlayableDirector timeline;
    public PlayableDirector timelineS;
    public Transform camPos;
    public float speed;
    public float stoppingDist;
    public float maxCDtime;
    [HideInInspector]
    public float CDtime;
    public bool canReturn;
    bool isReturning;
    Buttons layer;

    private void Start()
    {
        canReturn = false;
        isReturning = false;

        layer = FindObjectOfType<Buttons>();
    }

    void Update()
    {
        if (CDtime > 0)
            CDtime -= Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.Escape) && canReturn && layer.layer == 2 && CDtime <= 0)
        {
            isReturning = true;
            timeline?.Play();

            layer.layer--;
            CDtime = maxCDtime;

            layer.currentPos = layer.poses[0];
            layer.currentIndex = 0;
        }
        else if(isReturning)
        {
            isReturning = false;
            canReturn = false;
        }
    }
}
