using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public Transform pos;
    public float distance;
    public LayerMask mask;
    RaycastHit2D hit;
    public GameObject borders;
    public GameObject enemy;
    public Collider2D[] playersToDetect;

    public GameObject enemies;

    void Update()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        playersToDetect = Physics2D.OverlapCircleAll(pos.position, distance, mask);
        for (int i = 0; i < playersToDetect.Length; i++)
        {
            enemies.SetActive(true);
        }

        if (enemy != null)
        {
            borders.SetActive(true);
        }
        else if (enemy == null)
        {
            borders.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pos.position, distance);       
    }
}
