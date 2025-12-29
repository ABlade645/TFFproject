using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrbParticle : MonoBehaviour
{
    bool isAppeared = true;
    AudioSource sound;
    ParticleSystem particle;

    float pitch;

    private void Update()
    {
        if (isAppeared)
        {
            sound = GetComponent<AudioSource>();
            particle = GetComponent<ParticleSystem>();

            pitch = Random.Range(1, 1.5f);
            sound.pitch = pitch;

            sound.Play();
            particle.Play();
            isAppeared = false;

            Invoke("oDestroy", 0.3f);
        }
    }

    private void oDestroy()
    {
        Destroy(gameObject);
    }
}
