using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveRockAbility3D : MonoBehaviour
{
    //This script works to activate either the 3D Rock or 3D Flower abilities.

    //This stores the Transform of the object that this script is attached to
    [SerializeField] Transform T;
    //This stores the AtariAbilitySave script
    [SerializeField] AtariAblilitySave AAS;

    //This stores the 3D Rock abilities UI.
    [SerializeField] GameObject RockUI;

    //This stores some tutorial text which fades on screen after the player unlocks the ability.
    [SerializeField] DecalTextAppear DTA;

    //This stores what ability gets awakened.
    [SerializeField] bool ItemType;

    //This stores a voice acting clip that plays after the player unlocks the ability.
    [SerializeField] VoiceActing GainAbility;

    // Start is called before the first frame update
    void Start()
    {
        //The start() funtion works to set up the itemtype, and to see what type of UI needs to be shown
        if(ItemType == false)
        {
            RockUI.SetActive(false);
        }
        T = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() Function is used to make the object that activates the ability spin around.
        T.Rotate(0, 0.3f, 0);
    }

    public void TakeAbility()
    {
        //When the item is picked up by the player it will activate whichever ability the instance is there to awake.
        if (Input.GetKey(KeyCode.E))
        {
            switch(ItemType)
            {
                case false:
                    {
                        AAS.tDRock = true;
                        RockUI.SetActive(true);
                        DTA.SetOn = true;
                        if(GainAbility != null)
                        {
                            GainAbility.AddLine();
                        }
                        Destroy(gameObject);
                    }
                    break;
                case true:
                    {
                        AAS.FlowerGun = true;
                        DTA.SetOn = true;
                        Destroy(gameObject);
                    }
                    break;
            }

        }
    }
}
