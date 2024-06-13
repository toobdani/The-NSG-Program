using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoiceActing : MonoBehaviour
{
    //This script is used to play Voice lines in the scene

    //This variables stores the VoiceActingManager script
    [SerializeField] VoiceActingManager VAM;
    //This stores the string of the sentance said by the voice clip played
    [SerializeField] string sentance;
    //This stores the actual voice line played.
    [SerializeField] AudioClip Line;

    //This bool is set to true when the voice clip is triggered. 
    [SerializeField] bool triggered;

    //These bools decide whether the voice clip should skip to the newest clip, or for the clip to not delete itself.
    [SerializeField] bool Skip;
    [SerializeField] bool Keep;

    private void Start()
    {
        VAM = GameObject.FindGameObjectWithTag("AControl").GetComponent<VoiceActingManager>();
    }
    //When entering the trigger the script calls the AddLine() function
    private void OnTriggerEnter(Collider other)
    {
        if(triggered == false)
        {
            triggered = true;
            AddLine();
        }
    }

    //This function can be called by triggers or other scripts, and adds the Voice Clip to the list in the VoiceActingManager script. 
    public void AddLine()
    {
        VAM.AddLine(Line, sentance, Skip);
        if(Keep == false)
        {
            Destroy(gameObject);
        }
    }
}
