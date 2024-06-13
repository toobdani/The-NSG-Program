using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapThrow : MonoBehaviour
{
    //This script is used for the player to swap between the 3D Rock and 3D Flower abilities. 

    //This stores the AtariAbilitySave scriptable objects, so that the script can check when the two 3D abilities are unlocked.
    [SerializeField] AtariAblilitySave AAS;

    //This bool is used to check if the rock is selected or not
    [SerializeField] bool Rock;
    [SerializeField] RockThrow RT;
    [SerializeField] FlowerShoot FS;

    //This stores the UI for the two 3D abilties, and the 3D FlowerGun to activate and deactivate.
    [SerializeField] GameObject RockUI;
    [SerializeField] GameObject FlowerUI;
    [SerializeField] GameObject FlowerGun;

    // Start is called before the first frame update
    void Start()
    {
        //The start function sets things up so the Rock is the first one playable by the player
        Rock = true;
        FS.CanShoot = false;
        RT.CanRock = true;
        FlowerGun.SetActive(false);
        FlowerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() function checks when the player presses the Right click
        if(Input.GetKeyDown(KeyCode.Mouse1) && AAS.FlowerGun == true)
        {
            //When pressed the current active ability is swapped.
            Rock = !(Rock);

            switch(Rock)
            {
                //When Rock is true, then the Rock ability is activated.
                case true:
                    {
                        RT.CanRock = true;
                        RockUI.SetActive(true);
                        FS.CanShoot = false;
                        FlowerUI.SetActive(false);
                        FlowerGun.SetActive(false);
                    }
                    break;
                //When Rock is false, then the Flower Gun ability is activated. 
                case false:
                    {
                        FS.CanShoot = true;
                        FlowerUI.SetActive(true);
                        FlowerGun.SetActive(true);
                        RT.CanRock = false;
                        RockUI.SetActive(false);

                    }
                    break;
            }
        }   
    }
}
