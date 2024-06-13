using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //Stores the trigger for the switch
    [SerializeField] SwitchTrigger ST;

    //Registers if the swithc has been hit.
    public bool Pressed;

    //These store the animators for the Gate and Switch
    [SerializeField] Animator GateAnim;

    [SerializeField] Animator SwitchAnim;

    //This value changes the type of switch that the instance is.
    [SerializeField] int SwitchType;

    //This stores potential HoldPlatforms that can be affected by the sript.
    [SerializeField] HoldPlatforms[] HP;

    //These variables store whether the button needs to wait before resetting it's value.00
    [SerializeField] bool Waitbool;
    [SerializeField]float waittracker;
    [SerializeField] float WaitMax;

    //This registers if the Gate starts open in the game.
    [SerializeField] bool StartShut;

    //These variables are used for the function when changing the end position of a moving platform.
    [SerializeField] MovingPlatform2D MoveTD;
    [SerializeField] Vector3 NewEnd;

    public bool Changed;
    public bool ChangeMove;

    //These arrays are used to store arrays of Gates that are affected by the switch and whether they start shut.
    [SerializeField] Animator[] GateArray;
    [SerializeField] bool[] StartShutArray;
    // Update is called once per frame
    void Update()
    {
        //A switch statement that swaps between the differentely used buttons in the scene
        switch (SwitchType)
        {
            case 0:
                {
                    //This calls the default Gate Opening
                    SwitchAnim.SetBool("Button", ST.Hit);
                    //The Button for Gates
                    if (StartShut == false)
                    {
                        GateAnim.SetBool("Button", ST.Hit);
                    }
                    else
                    {
                        GateAnim.SetBool("Button", !ST.Hit);
                    }
                    Pressed = ST.Hit;
                }
                break;
            case 1:
                {
                    //The calls the HoldPlatform variaty of switch.
                    SwitchAnim.SetBool("Button", ST.Hit);
                    foreach (HoldPlatforms hp in HP)
                    {
                        hp.Pressed = ST.Hit;
                    }
                    Pressed = ST.Hit;

                }
                break;
            case 2:
                {
                    //The Button opens gate and moves platform
                    SwitchAnim.SetBool("Button", ST.Hit);
                    GateAnim.SetBool("Button", ST.Hit);

                    foreach (HoldPlatforms hp in HP)
                    {
                        hp.Pressed = ST.Hit;
                    }
                    Pressed = ST.Hit;

                }
                break;
            case 3:
                {
                    //This button activates as true for a few seconds, before deactivating.
                    //This type is used for HoldPlatforms.
                    if (ST.Hit == true && Waitbool == false)
                    {
                        Waitbool = true;
                        SwitchAnim.SetBool("Button", true);
                    }
                    if(Waitbool == true)
                    {
                        foreach (HoldPlatforms hp in HP)
                        {
                            hp.Pressed = true;
                        }
                        Pressed = true;
                    }
                    if(Waitbool == true && waittracker >= WaitMax)
                    {
                        Waitbool = false;
                        waittracker = 0;

                        SwitchAnim.SetBool("Button", false);

                        foreach (HoldPlatforms hp in HP)
                        {
                            hp.Pressed = false;
                        }
                        Pressed = false;
                    }

                }
                break;
            case 4:
                {
                    //This button activates as true for a few seconds, before deactivating.
                    //This type is used for Gates
                    if (ST.Hit == true && Waitbool == false)
                    {
                        Waitbool = true;
                        Pressed = true;
                        SwitchAnim.SetBool("Button", true);
                        GateAnim.SetBool("Button", true);
                    }
                    if (Waitbool == true && waittracker >= WaitMax)
                    {
                        Waitbool = false;
                        waittracker = 0;

                        SwitchAnim.SetBool("Button", false);
                        GateAnim.SetBool("Button", false);
                        Pressed = false;
                    }
                }
                break;
            case 5:
                {
                    //This type only animates the switch.
                    SwitchAnim.SetBool("Button", ST.Hit);
                    Pressed = ST.Hit;
                }
                break;
            case 6:
                {
                    //This type open multiple gates at once. 
                    SwitchAnim.SetBool("Button", ST.Hit);
                    int i = 0;
                    foreach(Animator A in GateArray)
                    {
                        if (StartShutArray[i] == false)
                        {
                            A.SetBool("Button", ST.Hit);
                        }
                        else if (StartShutArray[i] == true)
                        {
                            A.SetBool("Button", !ST.Hit);
                        }
                        i++;
                    }
                    Pressed = ST.Hit;
                }
                break;
        }

    }

    //The wait Timers are called in the FixedUpdate()
    private void FixedUpdate()
    {
        if(Waitbool == true)
        {
            waittracker += 0.1f;
        }
    }

    //This function changes the end position of a Moving Platform.
    //This is stored in a seperate function in order to make sure it only happens once.
    public void HitandChange()
    {
        if(Changed == false)
        {
            Changed = true;
            MoveTD.ChangeEnd(NewEnd);
        }
    }
}
