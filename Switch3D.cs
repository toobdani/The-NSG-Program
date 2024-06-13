using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3D : MonoBehaviour
{
    //This script is used for the 3D switches in the environment that can open both 3D and 2D gates. 

    //This stores the animator of the Gates in the 3D space
    [SerializeField] Animator GateAnim;
    //This stores the material that the switch changes to when its been hit.
    [SerializeField] Material ActiveGate;

    //This tracks whether the switch has been hit
    [SerializeField] bool BeenHit;
    //This bool tracks whether the switch is used to open a 3D or 2D gate.
    [SerializeField] bool Decal;

    //The update function checks to see if the button is used to keep the 2D Gate open
    private void Update()
    {
        if(Decal == true && BeenHit == true)
        {
            GateAnim.SetBool("Button", true);
        }
    }

    //When being hit by the 2D pollen, this script calls the HitSwitch() function.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pollen" || other.gameObject.tag == "EvilPollen")
        {
            if(BeenHit == false)
            {
                HitSwitch();
            }
        }
    }

    //The HitSwitch() function is used to open a gate in either the 3D of 2D space, dependent of the value of decal.
    public void HitSwitch()
    {
        BeenHit = true;
        switch(Decal)
        {
            case false:
                {
                    GateAnim.SetBool("Opengate", true);
                }
                break;
            case true:
                {
                    GateAnim.SetBool("Button", true);
                }
                break;
        }
        //When a 3D switch is hit, the material of the switch changes to a green.
        gameObject.GetComponent<MeshRenderer>().material = ActiveGate;
    }
}
