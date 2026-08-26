using UnityEngine;

public class EnemyWavePackage : MonoBehaviour
{
    public enemyInstanciator[] instantiators;

    public void Spawn()
    {
        foreach (enemyInstanciator enemy in instantiators)
            enemy.GetComponent<enemyInstanciator>().Instanciate();
    }
}
