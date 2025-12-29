using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moratAnimation : MonoBehaviour
{
    Animator anim;
    DialogManager script;
    public GameObject gObject;
    public int indexOfDialogue;
    bool canChange;

    private void Start()
    {
        anim = GetComponent<Animator>();
        script = gObject.GetComponent<DialogManager>();
        canChange = true;
    }

    void Update()
    {
        if (indexOfDialogue == 0)
        {
            if (script.isTalking == true && canChange == true)
            {
                anim.CrossFade("moratTalkShocked",0);                
                new WaitForSeconds(0.15f);
                anim.CrossFade("moratShocked", 0);
                canChange = false;
            }
        }

        if (indexOfDialogue == 1)
        {
            if (script.isTalking == true && canChange == true)
            {
                anim.CrossFade("moratTalkAwkward", 0);
                anim.CrossFade("moratAwkward", 0);
                canChange = false;
            }
        }

        if (indexOfDialogue == 2)
        {
            if (script.isTalking == true && canChange == true)
            {
                anim.CrossFade("moratTalk", 0);
                anim.CrossFade("Morat", 0);
                canChange = false;
            }
        }

        if (indexOfDialogue == 3)
        {
            if (script.isTalking == true && canChange == true)
            {
                anim.CrossFade("moratTalk", 0);
                anim.CrossFade("Morat", 0);
                canChange = false;
            }
        }

        if (indexOfDialogue == 4)
        {
            if (script.isTalking == true && canChange == true)
            {
                anim.CrossFade("moratTalk", 0);
                anim.CrossFade("Morat", 0);
                canChange = false;
            }
        }

        if (indexOfDialogue == 5)
        {
            if (script.isTalking == true && canChange == true)
            {
                anim.CrossFade("moratTalk", 0);
                anim.CrossFade("Morat", 0);
                canChange = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (script.isTalking == true && script.canSkip == true)
            {
                indexOfDialogue++;
                canChange = true;
            }
        }
    }
}
