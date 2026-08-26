using UnityEngine;

public class keys : MonoBehaviour
{
    public GameObject keyA;
    public Transform point;
    public bool isHeld;
    bool canPick;

    public bool used;

    // Update is called once per frame
    void Update()
    {
        if (canPick && !used)         
            if (!isHeld)           
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isHeld = true;
                    canPick = false;
                }                 

        if (isHeld)
        {
            keyA.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            keyA.transform.position = point.position;
            keyA.GetComponent<BoxCollider2D>().isTrigger = true; 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")       
            canPick = true;       
    }
}
