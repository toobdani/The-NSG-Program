using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoiceActingManager : MonoBehaviour
{
    //This stores List of the AudioClips that play for the Voice Acting and the text that the character is saying.
    public List<AudioClip> Lines;
    public List<string> Sentance;

    //This stores the AudioSource that plays the Voice clips
    [SerializeField] AudioSource AS;

    //This bool is set true when the audio clip is playing
    [SerializeField] bool Playing;

    //This is the Text for the Text for the Voice Acting
    [SerializeField] TextMeshProUGUI Dialogue;

    //If required, a voice clip can skip right to the newest line.
    [SerializeField] bool Skip;

    //The FixedUpdate is used to check when an voice clip has ended, and whether a new line needs to be played.
    private void FixedUpdate()
    {
        if(Playing == true)
        {
            if(AS.isPlaying != true || Skip == true)
            {
                if (Lines.Count > 0)
                {
                    AS.clip = Lines[0];
                    Dialogue.text = Sentance[0];

                    Lines.RemoveAt(0);
                    Sentance.RemoveAt(0);
                    AS.Play();
                }
                else
                {
                    Dialogue.text = null;
                    AS.clip = null;
                    Playing = false;
                }
                Skip = false;
            }
        }
    }

    //This function is called by the VoiceActing script, and adds the set Voice clip to the list of voice clips. 
    public void AddLine(AudioClip AC, string S, bool skip)
    {
        //When the voice clip is needed to skip then the audio list skips straight to the newly added voice line.
        switch(skip)
        {
            case true:
                {
                    Lines.Clear();
                    Sentance.Clear();
                    Lines.Add(AC);
                    Sentance.Add(S);
                    Playing = true;
                    Skip = true;
                }
                break;
            case false:
                {
                    Lines.Add(AC);
                    Sentance.Add(S);
                    Playing = true;
                }
                break;
        }
    }
}
