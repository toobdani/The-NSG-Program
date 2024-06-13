using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class audiomanager : MonoBehaviour
{
    //This script is used to manage the audio of the game.

    //This is the audiosource for the background music
    [SerializeField] AudioSource Music;
    //This is an array of songs that the game swaps between after a song ends
    [SerializeField] AudioClip[] Songs;
    //This stores the titles of all these songs.
    [SerializeField] string[] SongTitle;
    //This stores the current number of song played, and the total song count
    [SerializeField] int SongNumber;
    [SerializeField] int TotalSongs;

    //This is the name of the song credit which displays on screen when a new song plays
    [SerializeField] Animator NameDisplayer;
    [SerializeField] TextMeshProUGUI NameText;

    //This bool checks if the player is moving
    public bool Walking;
    //These are the sound effects played by the game.
    public GameObject Grab;
    public GameObject WalkingObject;
    public GameObject Place;
    public GameObject RockPlace;
    public GameObject RockDirection;
    public GameObject RockThrow;
    public GameObject Jump;
    public GameObject Crack;
    public GameObject Dialogue;
    public GameObject Projector;

    //This bool is true when it is time to swap to a new song.
    [SerializeField] bool swapsong;

    // Start is called before the first frame update
    void Start()
    {
        //The start function stores the total count of the songs to play before playing the first song
        TotalSongs = Songs.Length - 1;
        ChangeSong(0);
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() function waits for the current song to stop playing befor eit swaps to a new song, or if the player presses P
        if(Music.gameObject.activeSelf != false)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                swapsong = true;
            }
            if (!Music.isPlaying || swapsong == true)
            {
                swapsong = false;
                ChangeSong(SongNumber += 1);
            }
        }
        //The footsteps sound effect is effected by if the player is walking or not.
        WalkingObject.SetActive(Walking);

        //Every other audio clip is activated by other scripts that want to play the song.
        if(Grab.activeSelf == true && !Grab.GetComponent<AudioSource>().isPlaying)
        {
            Grab.SetActive(false);
        }        
        
        if(Place.activeSelf == true && !Place.GetComponent<AudioSource>().isPlaying)
        {
            Place.SetActive(false);
        }

        if (RockPlace.activeSelf == true && !RockPlace.GetComponent<AudioSource>().isPlaying)
        {
            RockPlace.SetActive(false);
        }        
        
        if (RockDirection.activeSelf == true && !RockDirection.GetComponent<AudioSource>().isPlaying)
        {
            RockDirection.SetActive(false);
        }        
        
        if (RockThrow.activeSelf == true && !RockThrow.GetComponent<AudioSource>().isPlaying)
        {
            RockThrow.SetActive(false);
        }        
        
        if (Jump.activeSelf == true && !Jump.GetComponent<AudioSource>().isPlaying)
        {
            Jump.SetActive(false);
        }        
        
        if (Crack.activeSelf == true && !Crack.GetComponent<AudioSource>().isPlaying)
        {
            Crack.SetActive(false);
        }  
        
        if (Projector.activeSelf == true && !Projector.GetComponent<AudioSource>().isPlaying)
        {
            Projector.SetActive(false);
        }        
        
        if (Dialogue.activeSelf == true && !Dialogue.GetComponent<AudioSource>().isPlaying)
        {
            Dialogue.SetActive(false);
        }
    }

    //This function is called to swap the currently played song
    public void ChangeSong(int Number)
    {
        Music.Pause();
        if(Number > TotalSongs)
        {
            SongNumber = 0;
        }
        else
        {
            SongNumber = Number;
        }
        Music.clip = Songs[SongNumber];
        Music.Play();
        StartCoroutine(SongNameShow());
    }

    //This has a UI popup appear showing the credit behind the playing song. 
    public IEnumerator SongNameShow()
    {
        NameText.text = SongTitle[SongNumber];
        NameDisplayer.SetBool("Show", true);
        yield return new WaitForSeconds(5f);
        NameDisplayer.SetBool("Show", false);
    }
}
