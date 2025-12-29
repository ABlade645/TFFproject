using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public Transform attackPos;
    public float attackDistance;
    public LayerMask whatIsPlayer;

    public int sDamage;

    public SlimeAI isAttacking;

    void Update()
    {
        

        if (isAttacking.isAttacking == true)
        {
            Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackDistance, whatIsPlayer);
            for (int i = 0; i < playersToDamage.Length; i++)
            {
                playersToDamage[i].GetComponent<PlayerHealth>().takeDamage(sDamage);
                isAttacking.isAttacking = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackDistance);
    }
}
