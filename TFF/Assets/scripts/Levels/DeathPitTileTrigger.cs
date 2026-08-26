using UnityEngine;

public class DeathPitTileTrigger : MonoBehaviour
{
    public int playerDamage;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<IDamagable>() != null)
            coll.GetComponent<IDamagable>().TakeDamagePit(9999999);

        if (coll.gameObject.CompareTag("Player"))
        {
            coll.GetComponent<PlayerHealth>().health -= playerDamage;
            coll.gameObject.transform.position = coll.GetComponent<HoldPlayerPosition>().vector;
        }           
    }
}
