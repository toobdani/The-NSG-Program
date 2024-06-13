using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    //This script is used to manage timelines in the game to create cutscenes.

    //This stores the Playable Director which contains the Timeline sequence.
    public PlayableDirector PlayD;
    //These bools are used to check if the timeline is paused, playing or in a Dialogue sequence.
    public bool play;
    public bool pause;
    public bool Dialogue;

    //This stores the Dialogue_Mangager script.
    [SerializeField] Dialogue_Manager DI_M;


    private void Update()
    {
        //The update() function checks whether the play, pause or Dialogue bools are true, and calls the appropriate functions based on this. 
        if(play == true)
        {
            play = false;
            Play();
        }

        if(pause == true)
        {
            pause = false;
            Pause();
        }

        if(Dialogue == true)
        {
            //When in Dialogue, the Timeline will not continue until the current Dialogue sequenece is complete.
            if(DI_M.inD == false)
            {
                Dialogue = false;
                play = true;
            }
        }
    }
    //This function plays the Timeline
    public void Play()
    {
        PlayD.Play();
    }

    //This function pauses the Timeline.
    public void Pause()
    {
        PlayD.Pause();
    }
}
