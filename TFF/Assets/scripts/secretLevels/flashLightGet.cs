using UnityEngine;
using UnityEngine.Playables;

public class flashLightGet : MonoBehaviour
{
    public flashLight script;
    public GameObject textS;
    public GameObject text;
    public PlayableDirector timeline;

    bool canInterract;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && canInterract)
        {
            script.hasFlash = true;
            timeline.Play();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canInterract = true;
            text.SetActive(true);
        }      
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInterract = true;
            text.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!script.hasFlash)
                textS.SetActive(true);
            else   
                Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        textS.SetActive(false);
    }
}
