using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    //This bool is called in the Switch script to check if the player has hit the trigger.
    public bool Hit;

    //This object stores the object that has triggered with the switch.
    public GameObject TriggerObject;

    //This stores the Switch script attatched to the parent gameobject.
    [SerializeField] Switch S;
    private void Update()
    {
        //The game checks to see if the switch has been hit and whether the TriggerObject is null or not active, before resetting the values of Hit and TriggerObject
        //This is done so the script can register when an object is deleted.
        if (Hit == true && (TriggerObject == null || TriggerObject.activeSelf == false))
        {
            Hit = false;
            TriggerObject = null;
        }
    }

    //On Trigger the switch sets Hit to true and stores the triggered object as TriggerObject.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Rock" || other.gameObject.tag == "Pollen" || other.gameObject.tag == "EvilPollen")
        {
            Hit = true;
            TriggerObject = other.gameObject;

            //If the switch is made to swap the end position of a moving platform then the Switch scripts HitandChange() function will be called. 
            if(S != null)
            {
                if (S.Changed == false && S.ChangeMove)
                {
                    S.HitandChange();
                }
            }
        }
    }

    //On leaving the trigger the values of Hit and TriggerObject are reset.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == TriggerObject)
        {
            Hit = false;
            TriggerObject = null;
        }
    }
}
