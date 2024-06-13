using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    //The bool for the variables for the projector, being to check when the projector is on and the 2D area that appears.
    [SerializeField] bool On;
    [SerializeField] GameObject OnItem;

    [SerializeField] bool PressedE;

    [SerializeField] GameObject ProjectorLight;

    [SerializeField] audiomanager AM;

    //The audio lines for the projector when it's turned on and off. 
    [SerializeField] VoiceActing[] OnandOff;

    //When this bool is set to true, the 2D area will stay on even if the player is swapping between styles. 
    public bool Keepon;

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
    }

    // Update is called once per frame
    void Update()
    {
        //When the projector is on, then the light and the 2D area appear. 
        ProjectorLight.SetActive(On);
        //This checks if the 2D area is deactivated, and if it is then the values for On and Keepon are false. 
        if(OnItem.activeSelf == false)
        {
            On = false;
            Keepon = false;
        }

        //The effects of the projector can be set by the player when they are looking at the projector.
        //The script checks to see if the Projector is highlighted, based on whether the layer of the object is ItemSelected.
        if (gameObject.layer == LayerMask.NameToLayer("ItemSelection"))
        {
            //If the player presses E whilst the projector is highlighted then the script will change the value of the On bool.
            if (Input.GetKeyDown(KeyCode.E) && PressedE == false)
            {
                AM.Projector.SetActive(true);
                AM.Projector.GetComponent<AudioSource>().Play();
                On = !(On);
                PressedE = true;
                switch (On)
                {
                    case true:
                        {
                            OnItem.SetActive(true);
                            Keepon = true;
                            OnandOff[0].AddLine();
                        }
                        break;
                    case false:
                        {
                            OnItem.SetActive(false);
                            Keepon = false;
                            OnandOff[1].AddLine();
                        }
                        break;
                }
            }

            if(Input.GetKeyUp(KeyCode.E))
            {
                PressedE = false;
            }
        }
        else
        {
            PressedE = false;
        }
    }

}
