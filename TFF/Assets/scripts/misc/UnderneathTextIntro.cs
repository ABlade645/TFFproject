using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class UnderneathTextIntro : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    public Sprite newSprite;
    public SpriteRenderer oldSprite;
    public Light2D globalLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Text1.SetActive(true);
            Text2.SetActive(true);
            globalLight.intensity = 0.3f;
            oldSprite.sprite = newSprite;
            Text1.GetComponent<TextAnim>().Begin();
            Text2.GetComponent<TextAnim>().Begin();
        }
    }
}
