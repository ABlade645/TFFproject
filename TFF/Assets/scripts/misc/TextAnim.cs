using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Transactions;

public class TextAnim : MonoBehaviour
{
    [Header("General")]
    public bool onStart;
    public bool scaleTime;
    public Text TxTGameObject;
    public string text;
    public float textSpeed;
    public float delay;
    public AudioSource audio;

    [Header("Fading")]
    public bool isFading;
    public float lifetime;
    public PlayableDirector playable;

    [Header("Pick Random From Pool")]
    public bool randomlySelectedInPool;
    public string[] textPool;

    //private
    int randomValue;
    bool started = true;
    bool isSkipping;

    void Update()
    {
        if (randomlySelectedInPool && started)
        {
            randomValue = Random.Range(0, textPool.Length);
            text = textPool[randomValue];
            started = false;
        }

        if (isSkipping == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSkipping = false;
                StopCoroutine(TextCoroutine());
            }
        }

        if (isFading == true && started == true)
        {
            Invoke("Fade", lifetime);
            Invoke("SetActive", lifetime + 1);
            started = false;
        }
    }

    public void SetActive()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        text = TxTGameObject.text;
        TxTGameObject.text = "";

        if (onStart == true)
        {
            Invoke("startCoroutine", delay);
        }
    }

    public void Begin()
    {
        Invoke("startCoroutine", delay);
    }

    public void startCoroutine()
    {
        StartCoroutine("TextCoroutine");
    }

    IEnumerator TextCoroutine()
    {
        foreach (char abc in text)
        {
            TxTGameObject.text += abc;
            if (audio != null)
            {
                audio.Play();
            } 
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    public void Fade()
    {
        playable.Play();
    }
}
