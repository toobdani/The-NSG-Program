using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    //This function is used to swap between decal platforms based on the item placed in an inventory spot.

    //These store the inventory item and the decal item
    [SerializeField] GameObject VineObject;
    [SerializeField] GameObject DecalObject;

    // Update is called once per frame
    void Update()
    {
        //The script checks to see if the appropriate inventory item is active, and if so activates the 2D decal platform.
        if(VineObject.activeSelf == false)
        {
            DecalObject.SetActive(false);
        }
        else if(VineObject.activeSelf == true)
        {
            DecalObject.SetActive(true);
        }
    }
}
