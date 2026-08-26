using UnityEngine;

public class KeyTerminal : MonoBehaviour
{
    public GameObject key;
    bool canInterract;
    public bool terminal;
    public Transform Pos;
    public GameObject Door;


    void Update()
    {
        if (canInterract)
        {
            if (key.GetComponent<keys>().isHeld)
            {
                key.GetComponent<keys>().used = true;
                terminal = true;
                key.GetComponent<keys>().isHeld = false;
                key.transform.position = Pos.position;
                Door.SetActive(false);              
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")       
            canInterract = true;      
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")       
            canInterract = false;      
    }
}
