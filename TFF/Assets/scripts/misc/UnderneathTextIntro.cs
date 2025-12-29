using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderneathTextIntro : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Text1.GetComponent<TextAnim>().Begin();
            Text2.GetComponent<TextAnim>().Begin();
        }
    }
}
