using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;

    public int health;
    public int maxHealth;

    public Image bar;
    public float fill;
    public float amount;
    public float unFill;
    public float reFill;

    DeathScreenPopUp deathScreen;
    
    void Start()
    {
        deathScreen = GameObject.Find("DeathScreen").GetComponent<DeathScreenPopUp>();
        health = 100;
        bar.fillAmount = fill;
    }

    public void SetHealth(int bonusHealth)
    { 
        health += bonusHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        fill += reFill;

        if (fill > 1)
        {
            fill = 1;
        }
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            deathScreen.DeathScreen();
            Destroy(player);
        }
        fill = health;
        bar.fillAmount = fill / 100;

        if (Input.GetKeyDown(KeyCode.H))
        {
            health = 100;
        }
    }

    public void takeDamage(int sDamage)
    {
        health -= sDamage;       
    }

    public void ExplosionDamage(int explDamage)
    {
        health -= explDamage; 
    }

    public void SnekaPrjctlDamage(int snDamage)
    {
        health -= snDamage;
    }

    public void ZZTPrjctlDamage(int ADamage)
    {
        health -= ADamage;
    }
}
