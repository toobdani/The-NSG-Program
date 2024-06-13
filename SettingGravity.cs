using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGravity : MonoBehaviour
{
    //This script is used to change the gravatational direction of the scene. 
    
    //This Vector3 stores the default values of the gravity.
    [SerializeField] Vector3 RegularPhysics;

    //This GameObject stores the player character
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //In the start function the defualt gravity settings are stored in RegularPhysics. 
        RegularPhysics = Physics.gravity;


    }

    //This function is called by the GenreChange script to set the gravity for the 2D Space.
    public void GravityChange(string direction)
    {
        //When called this function stores a string, which calls different gravity directions based on the value. 
        //The only one that was used in the game was XPos.
        switch(direction)
        {
            case "Zpos":
                {
                    Physics.gravity = new Vector3(0, 0, 9.81f);
                }
                break;
            case "Zneg":
                {
                    Physics.gravity = new Vector3(0, 0, -9.81f);
                }
                break;
            case "Xpos":
                {
                    Physics.gravity = new Vector3(9.81f, 0, 0);
                }
                break;
            case "Xneg":
                {
                    Physics.gravity = new Vector3(-9.81f, 0, 0);
                }
                break;
            case "Regular":
                {
                    Physics.gravity = RegularPhysics;
                }
                break;
        }
    }
}
