using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWait : MonoBehaviour
{
    //This function is used to play a random Voice clip when the player stands still for too long.

    //This takes the 3D player's script to check how long they have been standing still for.
    [SerializeField] FPS_Movement FPSM;

    //This stores and array of VoiceActing which are randomely chosen from
    [SerializeField] VoiceActing[] VA;

    //This stores the timings of the wait. 
    [SerializeField] float waitmax;
    [SerializeField] float currentwait;


    //The FixedUpdate() waits for the player to stand still long enough before playing one of the voice clips at random.
    private void FixedUpdate()
    {
        if(FPSM.moving == false && FPSM.gameObject.activeSelf == true)
        {
            if (currentwait >= waitmax)
            {
                currentwait = 0;
                int arraynum = Random.Range(0, 2);
                VA[arraynum].AddLine();
            }
            else
            {
                currentwait += 0.1f;
            }
        }
        else
        {
            currentwait = 0;
        }
    }
}
