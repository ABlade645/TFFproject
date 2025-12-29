using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePoisoned : MonoBehaviour
{
    public string projectileName;
    StatusEffects status;
    GameObject player;

    public bool isActive;
    public float offset;

    public Color defaultColor;
    public Color poisonedColor;

    ObjectCache cache;
    bool isSpawned = true;

    private void Update()
    {
        if (isSpawned)
        {
            cache = GameObject.FindGameObjectWithTag("ObjectCache").GetComponent<ObjectCache>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            status = GetComponent<StatusEffects>();
        }

        if (status.poison && isActive && player != null)
        {
            Instantiate(cache.GetFromCache(projectileName), transform.position + (player.transform.position - transform.position).normalized * offset, Quaternion.identity);

            status.poison = false;
            isActive = false;
        }
    }
}
