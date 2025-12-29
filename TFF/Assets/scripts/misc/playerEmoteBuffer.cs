using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEmoteBuffer : MonoBehaviour
{
    public Sprite[] faces;
    public Sprite[] armsL;
    public Sprite[] armsR;
    public Sprite[] cosmetics;

    public GameObject targetFace;
    public GameObject targetCosmetics;
    public GameObject targetArmL;
    public GameObject targetArmR;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Default();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
    }

    void Default()
    {
        targetFace.GetComponent<SpriteRenderer>().sprite = faces[0];
        targetCosmetics.GetComponent<SpriteRenderer>().sprite = cosmetics[0];
        targetArmL.GetComponent<SpriteRenderer>().sprite = armsL[0];
        targetArmR.GetComponent<SpriteRenderer>().sprite = armsR[0];
    }
}
