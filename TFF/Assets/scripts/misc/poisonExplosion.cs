using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonExplosion : MonoBehaviour
{
    bool canBurst = true;

    void Update()
    {
        if (canBurst == true)
        {
            canBurst = false;
            GetComponent<ParticleSystem>().Play();
            Invoke("Destroy", 2);
        } 
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
