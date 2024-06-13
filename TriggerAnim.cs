using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    //This script is used to restart the boulders animation so the player doesn't need to wait. 

    //This stores the animator for the boulder
    [SerializeField] Animator Anim;
    //This bool stores the value that the animator should be set at. 
    [SerializeField] bool set;

    
    //When triggered this resets the position of the moving boulder.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Anim != null)
            {
                Anim.SetBool("Restart", set);
            }
        }
    }
}
