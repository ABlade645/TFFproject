using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInstaciationTrigger : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject borders;

    public GameObject detector;

    public bool canInstanciate;

    private void Start()
    {
        canInstanciate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canInstanciate)
            {
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<enemyInstanciator>().Instanciate();
                    canInstanciate = false;
                }
                Invoke("Check", 0.1f);
            }
            borders.SetActive(true);
            
        }
    }

    void Check()
    {
        detector.GetComponent<enemyDetector>().Spawned();
    }

    private void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy") && borders.activeSelf)
        {
            borders.SetActive(false);
        }
    }
}
