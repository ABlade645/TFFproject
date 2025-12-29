using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class LayerSwitch : MonoBehaviour
{
    [Header("General")]
    public float minAlpha;
    public bool enters = false;
    public bool allowedToEnter;

    [Header("FadeOut")]
    public Tilemap[] tilemapsToFade;
    public Light2D[] lightsToFade;
    public GameObject[] objectsToFade;
    [Header("FadeIn")]
    public Tilemap[] tilemaps;
    public Light2D[] lights;
    public GameObject[] objects;

    [Header("Miscelanious")]
    public GameObject info;
    public float fadeSpeed;
    public float maxTimeBtwEntrance;
    float timeBtwEntrance;
    bool canEnter;
    public float[] bufferLight;
    LayerController layer;

    void Start()
    {
        enters = false;
        layer = FindObjectOfType<LayerController>();
    }

    void Update()
    {
        if (allowedToEnter && GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            //allowedToEnter = false;
        }
        if (!allowedToEnter && GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
            //allowedToEnter = true;
        }

        if (timeBtwEntrance > 0)
        {
            timeBtwEntrance -= Time.deltaTime;
        }

        if (timeBtwEntrance <= 0)
        {
            enters = false;
        }

        if (allowedToEnter && canEnter && Input.GetKeyDown(KeyCode.Q))
        {
            if (layer.layer == 0 && timeBtwEntrance <= 0)
            {
                timeBtwEntrance = maxTimeBtwEntrance;
                layer.layer = 1;
            }

            if (layer.layer == 1 && timeBtwEntrance <= 0)
            {
                timeBtwEntrance = maxTimeBtwEntrance;
                layer.layer = 0;
            }
        }

        if (layer.layer == 1)
        {  
            //tilemaps fading---------------------------------------------
            float aChange = 1;
            if (aChange > minAlpha)
            {
               // aChange -= fadeSpeed * Time.deltaTime;
            }

            for (int i = 0; i < tilemapsToFade.Length; i++)
            {
                tilemapsToFade[i].color = new Color(tilemapsToFade[i].color.r, tilemapsToFade[i].color.g, tilemapsToFade[i].color.b, minAlpha);

                if (tilemapsToFade[i].GetComponent<TilemapCollider2D>() != null)
                {
                    tilemapsToFade[i].GetComponent<TilemapCollider2D>().enabled = false;
                }
            }

            for (int i = 0; i < objectsToFade.Length; i++)
            {
                if (objectsToFade[i] != null)
                {
                    SpriteRenderer sprite = objectsToFade[i].GetComponent<SpriteRenderer>();
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, minAlpha);

                    if (objectsToFade[i].GetComponent<BoxCollider2D>() != null)
                    {
                        objectsToFade[i].GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
            //------------------------------------------------------------

            //lights fading-----------------------------------------------
            for (int i = 0; i < lightsToFade.Length; i++)
            {
                if (lightsToFade[i].intensity > 0)
                {
                    lightsToFade[i].intensity = 0;
                }

                if (lightsToFade[i].intensity < bufferLight[i])
                {
                    lightsToFade[i].intensity = 0;
                }
            }
            //------------------------------------------------------------

            //tilemaps Appearing------------------------------------------
            float aChangeBack = 0;
            if (aChangeBack < 1)
            {
                //aChangeBack += fadeSpeed * Time.deltaTime;
            }

            for (int i = 0; i < tilemaps.Length; i++)
            {
                tilemaps[i].color = new Color(tilemaps[i].color.r, tilemaps[i].color.g, tilemaps[i].color.b, 1);

                if (tilemaps[i].GetComponent<TilemapCollider2D>() != null)
                {
                    tilemaps[i].GetComponent<TilemapCollider2D>().enabled = true;
                }              
            }

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    SpriteRenderer sprite = objects[i].GetComponent<SpriteRenderer>();
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);

                    if (objects[i].GetComponent<BoxCollider2D>() != null)
                    {
                        objects[i].GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
            //------------------------------------------------------------

            //lights appearing--------------------------------------------
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i].intensity < 0.5)
                {
                    lights[i].intensity = 0.5f;
                }

                if (lights[i].intensity > 0.5)
                {
                    lights[i].intensity = 0.5f;
                }
            }
            //------------------------------------------------------------
            
        }

        if (layer.layer == 0)
        {
            //tilemaps fading---------------------------------------------
            float aChange = minAlpha;
            if (aChange < 1)
            {
                //aChange += fadeSpeed * Time.deltaTime;
            }

            for (int i = 0; i < tilemapsToFade.Length; i++)
            {
                tilemapsToFade[i].color = new Color(tilemapsToFade[i].color.r, tilemapsToFade[i].color.g, tilemapsToFade[i].color.b, 1);

                if (tilemapsToFade[i].GetComponent<TilemapCollider2D>() != null)
                {
                    tilemapsToFade[i].GetComponent<TilemapCollider2D>().enabled = true;
                }
            }

            for (int i = 0; i < objectsToFade.Length; i++)
            {
                if (objectsToFade[i] != null)
                {
                    SpriteRenderer sprite = objectsToFade[i].GetComponent<SpriteRenderer>();
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);

                    if (objectsToFade[i].GetComponent<BoxCollider2D>() != null)
                    {
                        objectsToFade[i].GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
            //------------------------------------------------------------

            //lights------------------------------------------------------
            for (int i = 0; i < lightsToFade.Length; i++)
            {
                if (lightsToFade[i].intensity < bufferLight[i])
                {
                    lightsToFade[i].intensity = bufferLight[i];
                }

                if (lightsToFade[i].intensity > bufferLight[i])
                {
                    lightsToFade[i].intensity = bufferLight[i];
                }
            }
            //------------------------------------------------------------

            //tilemaps Appearing------------------------------------------
            float aChangeBack = 1;
            if (aChangeBack > 0)
            {
                //aChangeBack -= fadeSpeed * Time.deltaTime;
            }

            for (int i = 0; i < tilemaps.Length; i++)
            {
                tilemaps[i].color = new Color(tilemaps[i].color.r, tilemaps[i].color.g, tilemaps[i].color.b, 0);
                if (tilemaps[i].GetComponent<TilemapCollider2D>() != null)
                {
                    tilemaps[i].GetComponent<TilemapCollider2D>().enabled = false;
                }
            }

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    SpriteRenderer sprite = objects[i].GetComponent<SpriteRenderer>();
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);

                    if (objects[i].GetComponent<BoxCollider2D>() != null)
                    {
                        objects[i].GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
            //------------------------------------------------------------

            //lights appearing--------------------------------------------
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i].intensity > 0)
                {
                    lights[i].intensity = 0;
                }

                if (lights[i].intensity < 0)
                {
                    lights[i].intensity = 0;
                }
            }
            //------------------------------------------------------------
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canEnter = true;
            info.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canEnter = false;
            info.SetActive(false);
        }
    }
}
