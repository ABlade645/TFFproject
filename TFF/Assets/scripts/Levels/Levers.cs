using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Levers : MonoBehaviour
{
    public LeverBool leverA;
    public LeverBool leverB;
    public LeverBool leverC;
    public LeverBool leverD;
    public int numberOfLevers;
    public bool access;
    public GameObject roadBlock;


    void Start()
    {
        access = false;
    }

    void Update()
    {
        if (access == true)
        {
            Destroy(roadBlock);
        }

        if (numberOfLevers == 1)
        {
            if (leverA.GetComponent<LeverBool>().activated == true)
            {
                access = true;
            }
        }
        else if (numberOfLevers == 2)
        {
            if (leverA.GetComponent<LeverBool>().activated == true && leverB.GetComponent<LeverBool>().activated == true)
            {
                access = true;
            }
        }
        else if (numberOfLevers == 3)
        {
            if (leverA.GetComponent<LeverBool>().activated == true && leverB.GetComponent<LeverBool>().activated == true && leverC.GetComponent<LeverBool>().activated == true)
            {
                access = true;
            }
        }
        else if (numberOfLevers == 4)
        {
            if (leverA.GetComponent<LeverBool>().activated == true && leverB.GetComponent<LeverBool>().activated == true && leverC.GetComponent<LeverBool>().activated == true && leverD.GetComponent<LeverBool>().activated == true)
            {
                access = true;
            }
        }
    }
}
