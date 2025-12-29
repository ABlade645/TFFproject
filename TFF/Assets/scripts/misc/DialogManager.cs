using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text text;
    public float textSpeed;
    public GameObject panel;
    public float waitTime;
    public bool isTalking;
    public bool canSkip;

    public GameObject Info;

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTalking == true && canSkip == true)
            {
                ClearSentence();
            }
        }

        if (canSkip == true && isTalking == true)
        {
            Info.SetActive(true);
        }
        else
        {
            Info.SetActive(false);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        panel.SetActive(true);

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        ClearSentence();
        isTalking = true;
    }

    public void ClearSentence()
    {
        text.text = "";
        dialogText.text = "";
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        canSkip = false;
        string sentence = sentences.Dequeue();
        text.text = sentence;
        StartCoroutine(TextCoroutine());
        //waitTime = text.text.ToCharArray().Length * textSpeed * 4;
    }

    IEnumerator TextCoroutine()
    {
        foreach (char abc in text.text)
        {
            dialogText.text += abc;
            //audio.Play();
            yield return new WaitForSeconds(textSpeed);
        }
        canSkip = true;
        Debug.Log("Courutine stopped");
    }


    public void EndDialog()
    {
        text.text = "";
        dialogText.text = "";
        panel.SetActive(false);
        isTalking = false;
    }
}
