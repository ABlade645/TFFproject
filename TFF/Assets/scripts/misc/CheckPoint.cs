using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    GameObject player;
    Buffer buffer;
    public Vector2 pos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void PosReader(Buffer buffer)
    {
        buffer.playerPos = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            PosReader(buffer);
        }
    }

    private void Awake()
    {        
        transform.position = buffer.playerPos;
    }
}
