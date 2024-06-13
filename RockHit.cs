using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHit : MonoBehaviour
{
    //This script is used on objects which can be knocked over by the 3D Rock

    //This stores the instances animator
    [SerializeField] Animator ObjectAnim;
    //This checks if the object has been hit
    [SerializeField] bool BeenHit;
    //Some knocked over objects need to change the 2D platforms near them, and these one have this bool true.
    [SerializeField] bool Change2D;
    //This stores the different start cracks swapped between after hitting the object.
    [SerializeField] GameObject[] Cracks;
    //This stores the new Death trigger for the area
    [SerializeField] DecalDeath Death;
    //This stores the 2D area's DecalMovement script
    [SerializeField] DecalMovement DM;
    //This stores the scene's camera
    [SerializeField] Camera SCamera;

    //When this is true it means that the object's collider should turn to a trigger after being hit.
    [SerializeField] bool Triggerit;

    //This shows the object is a 3D switch
    [SerializeField] Switch3D SthreeD;

    //This stores a voice acting line that will be played after the player hits the platform
    [SerializeField] VoiceActing Line;

    //This bool is true when the 2D player is true, and is used to make sure the object doesn't get knocked over whilst the player is 2D.
    [SerializeField] bool Got2D;

    //When hit by a rock the CollisionEnter() function causes the FallingDown() function to be called.
    //If it is a 3D switch then it calls the Switch3D script's HitSwitch() function.
    private void OnCollisionEnter(Collision collision)
    {
        if(DM == null)
        {
            Got2D = false;
        }
        else
        {
            Got2D = DM.gameObject.activeSelf;
        }
        if(collision.gameObject.tag == "Rock")
        {
            if(BeenHit == false && Got2D == false)
            {
                BeenHit = true;
                if(SthreeD == null)
                {
                    Fallingdown();
                }
                else
                {
                    gameObject.tag = "Untagged";
                    SthreeD.HitSwitch();
                }
            }
        }
    }

    //This function plays the knocked over items animation, and if needed will change the 2D areas design.
    public void Fallingdown()
    {
        if(Line != false)
        {
            Line.AddLine();
        }
        gameObject.tag = "Untagged";
        if (Change2D == true)
        {
            ChangeTrigger();
        }
        ObjectAnim.SetBool("Hit", true);
        if(Triggerit == true)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }    
    }

    //This function changes the 2D area that is assigned to the knocked over item.
    void ChangeTrigger()
    {
        Cracks[0].SetActive(false);
        Cracks[1].SetActive(true);

        DM.DD = Death;
        DM.GameCamera = SCamera;
    }
}
