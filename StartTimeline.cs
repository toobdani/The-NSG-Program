using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartTimeline : MonoBehaviour
{
    //This script is used to start a Timeline sequence

    //This stores the PressE UI Pop-up
    [SerializeField] GameObject PressE;

    //This stores the TimelineManager script for the cutscene called
    [SerializeField] TimelineManager TM;

    //This bool is true whilst the Timeline is playing
    [SerializeField] bool playing;

    //This int stores if the player has been talked to already
    [SerializeField] int talked;

    //This stores the 2D players movement
    [SerializeField] DecalMovement DM;

    //This is used to call the first Dialogue sequence of the Timeline
    [SerializeField] Dialogue D;
    [SerializeField] Dialogue_Manager DIM;

    //This stores the Decal Text that appears because of the Timeline.
    [SerializeField] DecalTextAppear Decal;

    //CallTimeline() is called when the player presses E whilst triggering with the script
    //This function plays the Timeline.
    public void CallTimeline()
    {
        if (Input.GetKey(KeyCode.E) && playing == false)
        {
            PressE.SetActive(false);
            playing = true;
            TM.Play();
            gameObject.tag = "Untagged";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playing == false)
        {
            PressE.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E) && playing == false)   
            {
                PressE.SetActive(false);
                playing = true;
                //This checks if the player has already experienced the Timeline sequence. 
                if(talked == 0)
                {
                    talked = 1;
                    TM.play = true;
                    DM.isMove = false;
                    DM.CutsceneJump = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PressE.SetActive(false);
        }
    }

    //This function ends the timeline, and is called at the end of the timeline sequence.
    public void EndTimeline()
    {
        DM.CutsceneJump = false;
        DM.isMove = true;
        StartCoroutine(Decal.Fadein());
    }
}
