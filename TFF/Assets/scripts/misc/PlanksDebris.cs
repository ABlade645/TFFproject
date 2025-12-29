using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanksDebris : MonoBehaviour
{
    public GameObject[] debris;
    public float maxFadingDelay;
    public float fadingSpeed;
    float fadingDelay;

    Collider2D player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        fadingDelay = maxFadingDelay;
    }

    private void Update()
    {
        fadingDelay -= Time.deltaTime;

        for (int i = 0; i < debris.Length; i++)
        {
            Physics2D.IgnoreCollision(debris[i].GetComponent<Collider2D>(), player, true);

            if (fadingDelay <= 0)
            {
                SpriteRenderer sprite = debris[i].GetComponent<SpriteRenderer>();
                sprite.color -= new Color(0, 0, 0, fadingSpeed * Time.deltaTime);
            }
        }

        if (fadingDelay < -maxFadingDelay)
        {
            Destroy(gameObject);
        }
    }
}
