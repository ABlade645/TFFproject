using UnityEngine;

public class enemyInstaciationTrigger : MonoBehaviour
{
    float intermissionTime;
    public float maxIntermissionTime;

    public EnemyWavePackage[] enemies;
    public GameObject borders;

    int currentWave = 0;
    BoxCollider2D coll;


    void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")                  
            if (coll.enabled)
                coll.enabled = false;    
    }

    bool Check()
    {
        return GameObject.FindGameObjectWithTag("Enemy") ? true : false;
    }

    void NextWave()
    {
        if(currentWave < enemies.Length)
            enemies[currentWave].Spawn();
        currentWave++;
    }

    private void Update()
    {
        if (Check() && intermissionTime != maxIntermissionTime)
            intermissionTime = maxIntermissionTime;
        else if (!Check() && intermissionTime > 0)
            intermissionTime -= Time.deltaTime;

        if(currentWave < enemies.Length && !coll.enabled && !borders.activeSelf)
            borders.SetActive(true);

        if (!Check() && currentWave == enemies.Length + 1 && borders.activeSelf)
            borders.SetActive(false);

        if (!Check() && currentWave < enemies.Length + 1 && !coll.enabled)
            if (intermissionTime <= 0)                        
                NextWave();            
            
        if(intermissionTime > 0)
            intermissionTime -= Time.deltaTime;
    }
}
