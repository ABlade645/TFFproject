using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationQueue : MonoBehaviour
{
    GameObject[] instantiators;
    public float instantiationInterval;

    void Start()
    {
        instantiators = GameObject.FindGameObjectsWithTag("EnemyInstantiatior");
    }

    public void StartInstantiation()
    {
        if (instantiators != null)
        {
            StartCoroutine("InstantiateCoroutine");
        }
        else
        {
            Debug.Log("Instantiation error: could not find instantiators");
        }
    }

    IEnumerator InstantiateCoroutine()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < instantiators.Length; i++)
        {
            instantiators[i].GetComponent<enemyInstanciator>().Instanciate();
            yield return new WaitForSeconds(instantiationInterval);
        }
    }
}
