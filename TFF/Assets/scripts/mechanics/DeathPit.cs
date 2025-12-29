using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class DeathPit : MonoBehaviour
{
    public float yLayer;
    public bool drawGizmos;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.y < yLayer)
            {
                player.GetComponent<PlayerHealth>().health = 0;
            }
        }       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (drawGizmos)
        {
            Gizmos.DrawLine(new Vector2(-Mathf.Infinity, yLayer), new Vector2(Mathf.Infinity, yLayer));
        }
    }

}
