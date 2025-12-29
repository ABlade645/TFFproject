using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWaveStarter : MonoBehaviour
{
    public EndlessModeInstantiation script;
    bool canStart = true;

    [HideInInspector]
    public bool removeBoat;
    GameObject boat;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canStart == true)
            {
                script.StartCheckup();
                canStart = false;
                script.intersection = false;
                removeBoat = true;
            }
        }
    }

    private void Update()
    {
        if (script == null)
        {
            script = GameObject.Find("Endless Instantiation manager").GetComponent<EndlessModeInstantiation>();
        }

        if (removeBoat)
        {
            boat = GameObject.FindGameObjectWithTag("boat");
            boat.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1000 * Time.deltaTime;
            Invoke("Delete", 3);
            removeBoat = false;
        }
    }

    void Delete()
    {
        Destroy(boat);
    }
}
