using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //This script is used to pick up items to add them to the player's inventory

    //This stores the ItemClass variable fo the item picekd up
    [SerializeField] ItemClass Item;

    //This stores the Inventory script
    [SerializeField] Inventory I;
    //This stores the Inventory object in the scene
    [SerializeField] GameObject Object;

    //This stores the PressE pop up
    [SerializeField] GameObject PressE;
    //This stores the audio manager script.
    [SerializeField] audiomanager AM;

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
    }

    //This script is used to pick up items from the scene and add them to the inventory
    public void EnterRay()
    {
        //When the raycast collides with the item it allows the player to add the item to their inventory by pressing E, which removes the item from the scene and adds it to their inventory.
            PressE.SetActive(true);
            Debug.Log("TriggeredWithItem");
            if(Input.GetKey(KeyCode.E))
            {
                if(Object.activeSelf == true)
                {
                    AM.Grab.SetActive(true);
                    AM.Grab.GetComponent<AudioSource>().Play();
                    I.Items.Add(Item);
                    Object.SetActive(false);
                    PressE.SetActive(false);
                }
            }
    }

}
