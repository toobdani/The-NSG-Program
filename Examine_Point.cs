using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Examine_Point : MonoBehaviour
{
    [SerializeField] GameObject ExamineObject;

    [SerializeField] GameObject PressE;

    [SerializeField] bool EnteredArea;

    [SerializeField] Camera AreaCamera;

    [SerializeField] Camera MainCamera;

    [SerializeField] FPS_Movement FPSM;

    [SerializeField] GameObject PlayerCylinder;

    [SerializeField] GameObject ExamineUI;

    // Start is called before the first frame update
    void Start()
    {
        AreaCamera.gameObject.SetActive(false);
        ExamineUI.SetActive(false);
        //ExamineObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Causes elements to occur based on whether the player is looking at the examination area.
        if (EnteredArea == true)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (FPSM.openInventory == false)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    EnteredArea = false;
                    AreaLeave();
                }
            }

            if (FPSM.openInventory == true)
            {
                ExamineUI.SetActive(false);
            }
            else
            {
                ExamineUI.SetActive(true);
            }
        }
        else
        {
            ExamineUI.SetActive(false);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PressE.SetActive(true);
        }
    }
    //Sets up the area examination when in the area's trigger.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TriggeredWithItem");
            if (Input.GetKey(KeyCode.E))
            {
                if(EnteredArea == false)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    EnteredArea = true;
                    PressE.SetActive(false);
                    AreaLook();
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PressE.SetActive(false);
        }
    }

    //This function sets up the variables of the Examination area and freezes the player's movement. In addition it makes the mouse visible on screen again.
    void AreaLook()
    {
        ExamineUI.SetActive(true);
        PlayerCylinder.SetActive(false);
        ExamineObject.SetActive(true);
        MainCamera.gameObject.SetActive(false);
        AreaCamera.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FPSM.GetComponent<Rigidbody>().useGravity = false;
        FPSM.GetComponent<CapsuleCollider>().enabled = false;
        FPSM.CanMove = false;
        //FPSM.Examining = true;
    }

    //This function closes the area examination and brings the player back to regular gameplay
    void AreaLeave()
    {
        ExamineUI.SetActive(false);
        PlayerCylinder.SetActive(true);
        ExamineObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        AreaCamera.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FPSM.GetComponent<Rigidbody>().useGravity = true;
        FPSM.GetComponent<CapsuleCollider>().enabled = true;
        FPSM.CanMove = true;
        //FPSM.Examining = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        EnteredArea = false;
    }
}
