using System.Collections;
using System.Collections.Generic;
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
    GameObject camera;
    Buttons layer;
    playercontroller controller;
    GameObject playerSoundManager;

    private void Start()
    {
        canReturn = false;
        isReturning = false;

        camera = FindObjectOfType<Camera>().gameObject;
        layer = FindObjectOfType<Buttons>();
        controller = FindObjectOfType<playercontroller>();
        playerSoundManager = GameObject.Find("SoundManager");
        playerSoundManager.SetActive(false);
    }

    void Update()
    {
        if (CDtime > 0)
        {
            CDtime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canReturn && layer.layer == 2 && CDtime <= 0)
        {
            isReturning = true;
            timeline?.Play();
            layer.layer--;
            CDtime = maxCDtime;
            controller.canMove = false;
            playerSoundManager.SetActive(false);
        }

        float checkDist = (camPos.position - camera.transform.position).sqrMagnitude;
        if (isReturning && checkDist > stoppingDist * stoppingDist)
        {
            camera.transform.position += (camPos.position - camera.transform.position).normalized * speed * Time.deltaTime;
            if (camera.GetComponent<CinemachineBrain>().enabled)
            {
                camera.GetComponent<CinemachineBrain>().enabled = false;
            }
        }
        else if(isReturning)
        {
            isReturning = false;
            canReturn = false;
        }
    }
}
