using UnityEngine;

public class simpleDoor : MonoBehaviour
{
    public Sprite[] sprites; // 0 - idle/closed; 1 - open; 2 - closed(highlight); 3 - open(highlight)
    SpriteRenderer currentSprite;
    public BoxCollider2D collide;

    public bool checkForLayers;
    public bool canOpen;
    public bool opened;
    bool canInterract;

    Transform player;
    SettingsSave settings;

    void Start()
    {
        settings = FindObjectOfType<SettingsSave>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (player.position.y >= transform.position.y && checkForLayers)
            currentSprite.sortingOrder = 17;
        else
            currentSprite.sortingOrder = 14;

        if (canInterract && canOpen)
            if (Input.GetKeyDown(settings.GetKeyFromCache("Punch")))
                if(!opened)
                    Open();
                else if (opened)
                    Close();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canInterract = true;

            if (!opened)
                currentSprite.sprite = sprites[2];                        
            else           
                currentSprite.sprite = sprites[3];        
        }        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canInterract = false;

            if (currentSprite.sprite == sprites[2])
                currentSprite.sprite = sprites[0];

            if (currentSprite.sprite == sprites[3])
                currentSprite.sprite = sprites[1];
        }
    }

    public void Open()
    {
        collide.enabled = false;
        opened = true;
        currentSprite.sprite = sprites[3];
    }

    public void Close()
    {
        collide.enabled = true;
        opened = false;
        currentSprite.sprite = sprites[2];
    }
}
