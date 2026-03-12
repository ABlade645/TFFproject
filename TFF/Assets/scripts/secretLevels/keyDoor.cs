using UnityEngine;
using UnityEngine.UI;

public class keyDoor : MonoBehaviour
{
    [HideInInspector]
    public KeyForTheDoor script;
    [HideInInspector]
    public bool hasKey;
    public string keyCardName;
    //[HideInInspector]
    public string InfoS = "A card is needed";
    //[HideInInspector]
    public Color InfoC = Color.white;
    public GameObject Info;
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
            {
                Info.SetActive(true);
                Info.GetComponent<Text>().text = InfoS;
                Info.GetComponent<Text>().color = InfoC;
            }
                     
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
                Info.SetActive(false);
        }
    }
}
