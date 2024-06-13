using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    //This script is used when calling Notes to be displayed to the player

    //These strings store the text and name of the Note
    [SerializeField] string NoteText;
    [SerializeField] string FileName;

    //This stores the NoteParent script
    [SerializeField] NoteParent NP;


    //When this bool is true it means the note is located in the 2D space.
    [SerializeField]bool flat;

    //This stores the PressE Pop-up
    [SerializeField] GameObject PressE;

    //This stores the gameobject that triggers the note.
    [SerializeField] GameObject P;


    //This function is called by the camera raycast to display a note UI.
    public void PickUpNote()
    {
        if(Input.GetKey(KeyCode.E))
        {
            NP.NoteText.text = NoteText;
            NP.FileName.text = FileName;
            NP.Set(false, null);
            Destroy(gameObject);
        }
    }

    //This function is allows the player to display the note when pressing E within the 2D space's trigger.
    private void OnTriggerStay(Collider other)
    {
        if (flat == true)
        {
            if (other.gameObject.tag == "Player")
            {
                PressE.SetActive(true);
                P = other.gameObject;
                if (Input.GetKey(KeyCode.E))
                {
                    P.gameObject.GetComponent<DecalMovement>().isMove = false;
                    NP.NoteText.text = NoteText;
                    NP.FileName.text = FileName;
                    NP.Set(true, P);
                    PressE.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }

    //When exiting the trigger the value of P and PressE are reset.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PressE.SetActive(false);
            P = null;
        }
    }
}
