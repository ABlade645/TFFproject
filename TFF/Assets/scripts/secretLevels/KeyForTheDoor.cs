using UnityEngine;

public class KeyForTheDoor : MonoBehaviour
{
    public GameObject Key;
    GameObject Info;
    public string[] door;
    bool canTake;

    void Start()
    {
        canTake = false;
    }

    void Update()
    {

        if (GameObject.Find(door[0]).GetComponent<keyDoor>().script == null)
            for (int i = 0; i < door.Length; i++)
                GameObject.Find(door[i]).GetComponent<keyDoor>().script = gameObject.GetComponent<KeyForTheDoor>();              

        if (Info == null)
            Info = GameObject.Find("Interraction Info");

        if (Key.activeSelf)       
            if (canTake)          
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    for (int i = 0; i < door.Length; i++)
                        GameObject.Find(door[i]).GetComponent<keyDoor>().hasKey = true;
                    Invoke("Delay", 0.1f);
                }       
    }

    void Delay()
    {
        Key.SetActive(false);
        Info.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canTake = true;
            Info.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canTake = false;
            Info.SetActive(false);
        }
    }
}
