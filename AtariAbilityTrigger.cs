using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtariAbilityTrigger : MonoBehaviour
{
    [SerializeField] AtariAblilitySave AAS;

    [SerializeField] CornerTrigger CT;



    //When Entering the trigger the game will allow the Atari player to jump
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                //This script also sets it so the camera in the first area no longer moves around in scale.
                CT.CameraB = false;
                AAS.AtariJump = true;
                Destroy(gameObject);
            
        }
    }
}
