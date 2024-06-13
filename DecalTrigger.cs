using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalTrigger : MonoBehaviour
{
    //This script can work on both the Atari Player and the Mega Man Player
    [SerializeField] AtariAblilitySave AAS;

    [SerializeField] DecalMovement DM;

    [SerializeField] Mega_Man_Movement MMM;

    //These bools set where the trigger are set.
    [SerializeField]bool Jump, Left, Right;

    //This is true when the player is standing on a moving platform.
    [SerializeField] bool OnMoveFloor;

    //This script is used on the three triggers surrouding the 2D player and will check whether they are colliding with a wall or the floor.

    //If this is attatched to the right or left trigger this will cause the player to stop moving in that direction, whilst the bottom trigger will check if the player can jump.

    private void OnTriggerStay(Collider other)
    {
        if (Jump == true)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "MoveFloor" || other.gameObject.tag == "Rock")
            {
                if (AAS.AtariJump == true && DM != null)
                {
                    DM.JumpAble = true;
                }

                if(other.gameObject.tag == "Rock" && DM != null)
                {
                    DM.RockOn = true;
                }

                if(other.gameObject.tag == "MoveFloor")
                {
                    OnMoveFloor = true;
                    if(DM != null)
                    {
                        DM.gameObject.transform.parent = other.gameObject.transform;
                    }
                    if(MMM != null)
                    {
                        MMM.gameObject.transform.parent = other.gameObject.transform;
                    }
                    
                }
            }
        }
        else if (Left == true)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall" || other.gameObject.tag == "Rock" || other.gameObject.tag == "MoveFloor")
            {

     
                if(DM != null)
                {
                    DM.LeftAble = false;
                }
                if(MMM != null)
                {
                    MMM.LeftAble = false;
                }
            }
        }
        else if (Right == true)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall" || other.gameObject.tag == "Rock" || other.gameObject.tag == "MoveFloor")
            {
                if (DM != null)
                {
                    DM.RightAble = false;
                }
                if (MMM != null)
                {
                    MMM.RightAble = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Jump == true)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "MoveFloor" || other.gameObject.tag == "Rock")
            {
                if (AAS.AtariJump == true && DM != null)
                {
                    DM.JumpAble = false;
                }

                if (other.gameObject.tag == "Rock" && DM != null)
                {
                    DM.RockOn = false;
                }

                if (other.gameObject.tag == "MoveFloor")
                {
                    OnMoveFloor = false;
                    if (DM != null)
                    {
                        DM.gameObject.transform.parent = null;
                    }
                    if (MMM != null)
                    {
                        MMM.gameObject.transform.parent = null;
                    }

                }
            }
        }
        else if(Left == true)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall" || other.gameObject.tag == "MoveFloor" || other.gameObject.tag == "Rock")
            {
                if (DM != null)
                {
                    DM.LeftAble = true;
                }
                if (MMM != null)
                {
                    MMM.LeftAble = true;
                }
            }
        }
        else if(Right == true)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall" || other.gameObject.tag == "MoveFloor" || other.gameObject.tag == "Rock")
            {
                if (DM != null)
                {
                    DM.RightAble = true;
                }
                if (MMM != null)
                {
                    MMM.RightAble = true;
                }
            }
        }
    }
}
