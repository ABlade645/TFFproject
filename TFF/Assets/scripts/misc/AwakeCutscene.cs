using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AwakeCutscene : MonoBehaviour
{

    float await = 0.5f;

    public GameObject Player;
    public Transform point;
    private Rigidbody2D rb;

    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Player.transform.position == point.position)
        {
            await -= Time.deltaTime;
            rb.velocity = Player.transform.position;
        }

        if (await < 0)
        {
            gameObject.SetActive(false);
        }  

        Player.transform.position = point.position;        
    }
}
