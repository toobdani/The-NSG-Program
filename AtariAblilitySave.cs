using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AtariSave", order = 2, fileName = "Atari Save")]
public class AtariAblilitySave : ScriptableObject
{
    //This is the scriptable object which stores the bools which allow the player to use the different abilities.
    //These abilities include the 2D players jump and rock placing, and the 3D players rock throw and flower gun.

    public bool AtariJump, AtariRock, tDRock, FlowerGun;
    void Awake()
    {
        //Each of the bools are set to false upon the game initialy beginning.
        AtariJump = false;
        AtariRock = false;
        tDRock = false;
        FlowerGun = false;
    }
}
