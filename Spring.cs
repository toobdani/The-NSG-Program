using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    //This script is used to create the Spring Mechanics. 
    //Springs can either increase the Atari player's height, or have the Mega Man player's pollen bounce off it.

    //This stores the Decal Player that the Spring will effect
    [SerializeField] DecalMovement DM;

    //This stores the regular jump height of the decal player
    [SerializeField] float RegularJump;
    //This stores the jump height that the player swaps to
    [SerializeField] float SwapJump;

    //These GameObjects store the new starting and ending positions of the pollen shot by the Mega Man player when it hits the spring.
    [SerializeField] GameObject NewEnd;
    [SerializeField] GameObject NewStart;

    //This bool, when true, means that the pollen is bouncing along the Y-axis, rather then the X.
    [SerializeField] bool Up;

    // Start is called before the first frame update
    void Start()
    {
        //In the Start() function this script stores the value of the player's regular jump height.
        if(DM != null)
        {
            RegularJump = DM.Jumpheight;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player triggers with the spring then their jump height increases
        if(other.gameObject.tag == "Player" && DM != null)
        {
            DM.Jumpheight = SwapJump;
        }

        //If the pollen hits the spring, then the script changes the position that the pollen moves between
        //This depends on the direction the pollen is moving between.
        if(other.gameObject.tag == "Pollen" || other.gameObject.tag == "EvilPollen")
        {
            other.gameObject.GetComponent<Pollen>().MoveUp = Up;
            switch (other.gameObject.GetComponent<Pollen>().MoveRight)
            {
                case true:
                    {
                        other.gameObject.GetComponent<Pollen>().OriginMark = NewStart;
                        other.gameObject.GetComponent<Pollen>().TargetMark = NewEnd;
                    }
                    break;
                case false:
                    {
                        other.gameObject.GetComponent<Pollen>().OriginMark = NewEnd;
                        other.gameObject.GetComponent<Pollen>().TargetMark = NewStart;
                    }
                    break;
            }
        }
    }

    //When the player exits the Trigger their jump height returns to normal. 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && DM != null)
        {
            DM.Jumpheight = RegularJump;
        }
    }

}
