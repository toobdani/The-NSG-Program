using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveRock : MonoBehaviour
{
    //This script is called by the Timeline in Segement 3 to activate the player's 2D rock ability. 
    [SerializeField] AtariAblilitySave AAS;
    [SerializeField] DecalMovement DM;

    public void GiveRockFunction()
    {
        AAS.AtariRock = true;
        DM.isMove = true;
        DM.CutsceneJump = false;
    }
}
