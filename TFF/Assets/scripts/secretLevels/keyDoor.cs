using UnityEngine;

public class keyDoor : MonoBehaviour
{
    [HideInInspector]
    public KeyForTheDoor script;
    [HideInInspector]
    public bool hasKey;
    public string keyCardName;
    public GameObject InfoS;
    public GameObject coll;

    //Animations
    [Header("Animation/States")]
    public string open;
    public string closed;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (hasKey)
            {
                anim.CrossFade(open, 0);
                coll.SetActive(false);
            }
            else
                InfoS.SetActive(true);     
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (hasKey)
            {
                anim.CrossFade(closed, 0);
                coll.SetActive(true);
            }
            else
                InfoS.SetActive(false);
        }
    }
}
