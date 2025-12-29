using UnityEngine;

public class inspectFurniture : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite selected;
    public Sprite opened;
    Sprite deflt;

    [Header("Other")]
    public Transform lootPoint;
    public GameObject loot;
    AudioSource sound;
    bool canInterract;
    bool activated = false;
    

    void Start()
    {
        deflt = GetComponent<SpriteRenderer>().sprite;
        sound = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if(canInterract && Input.GetKeyDown(KeyCode.Q) && !activated && canInterract)
        {
            if (loot != null)
                Instantiate(loot, lootPoint);
            GetComponent<SpriteRenderer>().sprite = opened;
            sound.Play();
            activated = true;
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !activated)
        {
            GetComponent<SpriteRenderer>().sprite = selected;
            canInterract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            GetComponent<SpriteRenderer>().sprite = deflt;
            canInterract = false;
        }
    }
}
