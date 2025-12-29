using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SecretNotes : MonoBehaviour
{
    public GameObject info;
    public string text;
    public PlayableDirector timeline;
    public PlayableDirector timelineRev;
    Text txtObj;
    bool canInterract;
    bool isLooking;

    void Start()
    {
        txtObj = GameObject.Find("TxtObj").GetComponent<Text>();
    }

    void Update()
    {
        if (canInterract && Input.GetKeyDown(KeyCode.Q) && canInterract)
        {
            if (!isLooking && timelineRev.state != PlayState.Playing)
            {
                timeline.Play();
                isLooking = true;
                Invoke("ShowText", 0.4f);
            }

            if (isLooking && timeline.state != PlayState.Playing)
            {
                timelineRev.Play();
                isLooking = false;
                txtObj.text = "";
                txtObj.gameObject.SetActive(false);
            }
        }
    }

    void ShowText()
    {
        txtObj.gameObject.SetActive(true);
        txtObj.text = text;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        canInterract = true;
        info.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canInterract = false;
        info.SetActive(false);
        if (isLooking)
        {
            timelineRev.Play();
            isLooking = false;
        }
    }
}
