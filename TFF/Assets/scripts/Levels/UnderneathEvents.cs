using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnderneathEvents : MonoBehaviour
{
    //Intro part
    bool canInterract;
    public string text;
    public Text TxTGameObject;
    public AudioSource audio;
    public float textSpeed;
    public float delay;

    //Enemy trap
    public LeverBool levers;
    public GameObject trapCollider;

    void Start()
    {
        canInterract = true;
        levers = GetComponent<LeverBool>();
    }

    //Intro {
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canInterract == true)
        {
            canInterract = false;
            startCoroutine();
            Invoke("startCoroutineS", delay);
        }
    }

    void startCoroutine()
    {
        StartCoroutine(TextCoroutine());
    }

    void startCoroutineS()
    {
        StartCoroutine(TextCoroutineS());
    }

    IEnumerator TextCoroutine()
    {
        foreach (char abc in text)
        {
            TxTGameObject.text += abc;
            audio.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator TextCoroutineS()
    {
        foreach (char abc in text)
        {
            TxTGameObject.text += abc;
            audio.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }
    //Intro }

    private void Update()
    {
        //Enemy trap {
        if (levers.activated == true)
        {
            trapCollider.SetActive(true);
        }
        //Enemy trap }
    }
}
