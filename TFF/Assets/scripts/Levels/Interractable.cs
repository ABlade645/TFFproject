using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interractable : MonoBehaviour
{
    [HideInInspector]
    public Image image;
    bool canInterract;
    bool isInterracting;

    float startSize;

    [Header("General")]
    public bool activated;
    public float fillSpeed;
    public float endSize;
    public bool isActive;
    public GameObject Info;

    void Start()
    {
        image = GetComponentInChildren<Image>();
        startSize = image.transform.localScale.x;
        isActive = true;
        Info.SetActive(false);
        activated = false;
    }

    void Update()
    {
        if (image.fillAmount == 1 && !activated)
        {
            activated = true;
            Info.SetActive(false);
        }

        if (image.fillAmount < 1 && activated)
        {
            activated = false;
        }

        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isInterracting = true;
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                isInterracting = false;
            }

            if (!isInterracting && image.fillAmount > 0)
            {
                image.fillAmount -= Time.deltaTime * fillSpeed * 1.5f;
                if (image.transform.localScale.x > startSize)
                {
                    image.transform.localScale = Vector2.MoveTowards(image.transform.localScale, new Vector2(image.transform.localScale.x - endSize, image.transform.localScale.x - endSize), fillSpeed * Time.deltaTime);
                }
            }

            if (canInterract && isInterracting)
            {
                image.fillAmount += Time.deltaTime * fillSpeed;
                if (image.transform.localScale.x < endSize)
                {
                    image.transform.localScale = Vector2.MoveTowards(image.transform.localScale, new Vector2(image.transform.localScale.x + endSize, image.transform.localScale.x + endSize), fillSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (image.fillAmount > 0 || image.transform.localScale.x != startSize)
            {
                image.fillAmount = 0;
                image.transform.localScale = new Vector2(startSize, startSize);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canInterract = true;
            if (isActive)
            {
                Info.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canInterract = false;
            if (isActive)
            {
                Info.SetActive(false);
            }
        }
    }
}
