using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCode : MonoBehaviour
{
    //This script is used to complete several different things within the game.

    //This stores the Inventory item vines collectable by the player
    [SerializeField] GameObject[] Vines;

    //This stores the item placement spots for the inventory vines
    [SerializeField] GameObject[] Planters;

    //This stores every Tutorial decal that appears thanks to this script. 
    [SerializeField] DecalTextAppear[] Decals;

    //This stores the first projector of the game, which shows the projector tutorial.
    [SerializeField] GameObject Projector;

    //This bool is set true when the game is played in the build
    public bool Build;

    //These are voice acting clips which are played under certain conditions in the game.
    [SerializeField] VoiceActing WhereamI;

    [SerializeField] VoiceActing VineTake;

    [SerializeField] VoiceActing TakeThis;
    // Start is called before the first frame update
    void Start()
    {
        //The start() function checks to see if the game is being played in the build or not
        if(Application.isEditor == false)
        {
            Build = true;
        }
        else
        {
            Build = false;
        }
        //The Start() function ends by playing the WhereamI line.
        WhereamI.AddLine();
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() function is spent checking to see if the conditions for the tutorial text to appear have been met.

        if (Decals[0] != null)
        {
            if (Projector.gameObject.layer == LayerMask.NameToLayer("ItemSelection"))
            {
                StartCoroutine(Decals[0].Fadein());
                Decals[0] = null;
            }
        }

        if (Decals[1] != null)
        {
            foreach (GameObject G in Vines)
            {
                if (G.gameObject.layer == LayerMask.NameToLayer("ItemSelection"))
                {
                    StartCoroutine(Decals[1].Fadein());
                    Decals[1] = null;
                }
            }
        }

        int activeI = 0;
        if (Decals[2] != null)
        {
            foreach(GameObject G in Vines)
            {
                if(G.gameObject.activeSelf == false)
                {
                    activeI++;

                    if (VineTake != null)
                    {
                        VineTake.AddLine();
                    }
                }

                if(activeI >= 2)
                {
                    StartCoroutine(Decals[2].Fadein());
                    Decals[2] = null;

                    if(TakeThis != null)
                    {
                        TakeThis.AddLine();
                    }
                }
            }
        }

        int secondacitveI = 0;
        if (Decals[3] != null)
        {
            foreach (GameObject G in Vines)
            {
                if (G.gameObject.activeSelf == false)
                {
                    secondacitveI++;
                }

                if (secondacitveI >= 3)
                {
                    StartCoroutine(Decals[3].Fadein());
                    Decals[3] = null;
                }
            }
        }

        if (Decals[4] != null)
        {
            foreach (GameObject G in Planters)
            {
                if (G.gameObject.layer == LayerMask.NameToLayer("ItemSelection"))
                {
                    StartCoroutine(Decals[4].Fadein());
                    Decals[4] = null;
                }
            }
        }

        if(Decals[5].FadeCount >= 1 && Decals[6].FadeCount < 1 && Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(Decals[6].Fadein());
            StartCoroutine(Decals[7].Fadein());
        }
    }
}
