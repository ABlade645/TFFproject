using UnityEngine;

public class plankBreak : MonoBehaviour, IDamagable
{
    Slam slam;
    Quaternion quaternion;

    public bool canBreak;
    public float breakingMagnitude;

    [Header("Manual Break")]
    public float health;

    [Header("Prefab")]
    public GameObject prefab;

    void Start()
    {
        if (slam == null)       
            slam = FindObjectOfType<Slam>();
        
        quaternion = transform.localRotation;
    }

    void Update()
    {
        if (health <= 0)       
            Break();      

        if (slam.verticalMagnitude < -breakingMagnitude)        
            canBreak = true;       
        else       
            canBreak = false;       
    }

    public void TakeDamagePhysical(float damage)
    {
        health -= damage;
    }

    public void TakeDamageRanged(float damage)
    {
        health -= damage;
    }

    public void TakeDamagePit(float damage)
    {
        health -= damage;
    }

    public void Break()
    {
        Instantiate(prefab, transform.position, quaternion);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && canBreak)      
            Break();      
    }
}
