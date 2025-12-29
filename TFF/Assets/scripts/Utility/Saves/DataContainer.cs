using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataContainer
{
    [Header("General Data")]
    public int levelsAvailable;
    public int pointsAmount;

    [Header("Secret levels")]
    public bool first;
    public bool second;
    public bool third;

    [Header("Weapons")]
    public bool rootBlade;


    public DataContainer(playerAttack player)
    {
        //weapons
        rootBlade = player.hasRootBlade;
    }
}
