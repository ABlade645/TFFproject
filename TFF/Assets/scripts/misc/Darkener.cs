using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Darkener : MonoBehaviour
{
    public GameObject point;
    public GameObject player;
    public GameObject lightSource;
    Light2D source;
    public float intensity;
    public float criticalIntensity;

    bool canDarken;
    bool darking;
    bool lighten;
    bool triggered;

    private void Start()
    {
        source = lightSource.GetComponent<Light2D>();
    }

    void Update()
    {

        if (canDarken == true && darking == true)
        {
            source.intensity -= Time.deltaTime;
        }
        else if (canDarken == false && lighten == true)
        {
            source.intensity += Time.deltaTime;
        }

        if (source.intensity > intensity && triggered)
        {
            lighten = false;
            source.intensity = intensity;
        }

        if (source.intensity <= criticalIntensity)
        {
            source.intensity = criticalIntensity;
            darking = false;
        }
        else
        {
            darking = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canDarken = true;
            lighten = false;
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canDarken = false;
            lighten = true;
        }
    }
}
