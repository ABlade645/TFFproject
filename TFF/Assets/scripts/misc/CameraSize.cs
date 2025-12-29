using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraSize : MonoBehaviour
{
    public GameObject cinCam;
    public GameObject Cam;

    public Slider slider;
    public float sizeMultiplier;


    void Start()
    {
        slider.value = 0.5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            slider.value += 0.05f;
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            slider.value -= 0.05f;
        }

        cinCam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 8 + slider.value * sizeMultiplier;
    }

        

}
