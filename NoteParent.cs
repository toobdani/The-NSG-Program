using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteParent : MonoBehaviour
{
    //This script is used as the parent for the Note objects the player can pick up.

    //This stores the text and title for the note item
    public TextMeshProUGUI NoteText;
    public TextMeshProUGUI FileName;

    //This bool is true when the note should be set on screen.
    public bool SetNote;

    //This stores the animator for the Note UO
    [SerializeField] Animator Anim;
    //This stores the First Person movement
    [SerializeField] FPS_Movement FPSM;

    //This bool is true if the note displayed was taken from the 2D space.
    [SerializeField] bool flat;
    //This stores the instnace of the player that interacts with the note
    [SerializeField] GameObject P;

    //This stores the audiomanager script.
    [SerializeField] audiomanager AM;

    //The start function sets up the audiomanager.
    private void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
    }

    private void Update()
    {
        //if the note is set on screen and the played left clicks then the note will leave the screen.
        if(SetNote == true)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                AM.Dialogue.SetActive(true);
                AM.Dialogue.GetComponent<AudioSource>().Play();
                Anim.SetBool("EnterScreen", false);
                SetNote = false;
                //Once the note leaves this function provides movement back to the player that picked it up.
                if(flat == false)
                {
                    FPSM.CanMove = true;
                }
                else
                {
                    P.gameObject.GetComponent<DecalMovement>().isMove = true;
                    P = null;
                }
            }
        }
    }

    //This function is called by the Notes script, and displays the picked up note on screen.
    public void Set(bool f, GameObject g)
    {
        if(g != null)
        {
            P = g;
        }
        flat = f;
        if(flat == false)
        {
            FPSM.CanMove = false;
        }
        Anim.SetBool("EnterScreen", true);
        SetNote = true;
    }

}
