using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interractableTextShowUp : MonoBehaviour
{
    public Interractable interractable;
    public float maxWaitTime;
    public bool isPermanent;
    public bool usesSprite;
    public float gradientSpeed;
    public float raiseSpeed;
    public float yOffset;

    float startYOffset;
    float waitTime;
    bool begin;
    Text text;
    Image sprite;

    private void Start()
    {
        startYOffset = transform.position.y;

        if (usesSprite)
        {
            sprite = GetComponent<Image>();
        }
        if (!usesSprite)
        {
            text = GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (interractable.activated)
        {
            interractable.image.fillAmount = 0;
            interractable.isActive = false;
            if (!isPermanent)
            {
                waitTime = maxWaitTime;
            }
            interractable.activated = false;

            begin = true;
        }

        if (begin && !usesSprite)
        {
            MoveUp();

            //Transparency-------------------------------------------------------
            if (text.color.a < 1)
            {
                text.color += new Color(text.color.r, text.color.g, text.color.b, Time.deltaTime * gradientSpeed);
            }
            //-------------------------------------------------------------------

            if (transform.position.y == startYOffset + yOffset && text.color.a == 1)
            {
                begin = false;
            }
        }

        if (begin && usesSprite)
        {
            MoveUp();

            //Transparency-------------------------------------------------------
            if (sprite.color.a < 1)
            {
                sprite.color += new Color(sprite.color.r, sprite.color.g, sprite.color.b, Time.deltaTime * gradientSpeed);
            }
            //-------------------------------------------------------------------

            if (transform.position.y == startYOffset + yOffset && sprite.color.a == 1)
            {
                begin = false;
            }
        }

        if (!isPermanent && !begin)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                if (usesSprite)
                {
                    MoveDown();
                    if (sprite.color.a > 0)
                    {
                        sprite.color -= new Color(sprite.color.r, sprite.color.g, sprite.color.b, Time.deltaTime * gradientSpeed * 2);
                    }
                }

                if (!usesSprite)
                {
                    MoveDown();
                    if (text.color.a > 0)
                    {
                        text.color -= new Color(text.color.r, text.color.g, text.color.b, Time.deltaTime * gradientSpeed * 2);
                    }
                }
            }
        }
    }

    void MoveUp()
    {
        //position-----------------------------------------------------------
        if (transform.position.y < startYOffset + yOffset)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(transform.position.x, startYOffset + yOffset), raiseSpeed * Time.deltaTime);
        }
        if (transform.position.y > startYOffset + yOffset)
        {
            transform.position = new Vector2(transform.position.x, startYOffset + yOffset);
        }
        //-------------------------------------------------------------------
    }

    void MoveDown()
    {
        //position-----------------------------------------------------------
        if (transform.position.y > startYOffset)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(transform.position.x, startYOffset), raiseSpeed * Time.deltaTime);
        }
        if (transform.position.y < startYOffset)
        {
            transform.position = new Vector2(transform.position.x, startYOffset);
        }
        //-------------------------------------------------------------------
    }
}
