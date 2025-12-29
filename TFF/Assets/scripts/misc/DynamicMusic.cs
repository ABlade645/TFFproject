using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public GameObject calm;
    public GameObject dynamic;
    public float speed;
    AudioSource calmVol;
    AudioSource dynamicVol;
    public GameObject enemy;
    
    void Start()
    {
        calmVol = calm.GetComponent<AudioSource>();
        dynamicVol = dynamic.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        

        if (enemy != null)
        {
            calmVol.volume -= Time.deltaTime * speed;
            dynamicVol.volume += Time.deltaTime * speed;
        }

        if (enemy == null)
        {
            calmVol.volume += Time.deltaTime * speed;
            dynamicVol.volume -= Time.deltaTime * speed;
        }
    }
}
