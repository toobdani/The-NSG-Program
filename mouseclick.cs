using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseclick : MonoBehaviour
{
    //This script is used to place items stored in the Inventory down into the scene.

    //These store the object and name of the item that can be placed. 
    [SerializeField] GameObject[] AppearObjects;
    [SerializeField] ItemClass[] AppearNames;

    //This stores the ItemClass variable for the item placed in the scene. 
    [SerializeField] ItemClass TempItem;
    //this checks if an item has already been placed in the area.
    [SerializeField] bool Clicked;
    //This bool is set to true when an item is being placed or removed, so the player can't 
    [SerializeField] bool DoEvent;

    //These store the scripts needed for placing these items down
    [SerializeField] Inventory I;
    [SerializeField] audiomanager AM;
    [SerializeField] InventoryElement IB;

    //This audio clip plays if the player trys to place items when they aren't holding anything
    [SerializeField] VoiceActing Nothingtoplace;

    // Start is called before the first frame update
    void Start()
    {
        //The start function sets things up so none of the AppearItems are active during the start.
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
        foreach (GameObject I in AppearObjects)
        {
            I.SetActive(false);
        }
    }

    public void Place()
    {
        //This function is called by the camera Raycast, and allows the player to either add or remove an inventory item in the space. 
        if (Input.GetKeyDown(KeyCode.E))
        {
            AM.Place.SetActive(true);
            AM.Place.GetComponent<AudioSource>().Play();
                if (Clicked == false && DoEvent == false)
                {
                    GiveVine();
                }
                else if (Clicked == true && DoEvent == false)
                {
                    TakeVine();
                }
        }
    }

    void GiveVine()
    {
        //This function checks what item the player is trying to place compared to the items that can be placed.
        int Count = 0;
        bool Delete = false;
        if (I.Items.Count > 1)
        {
            foreach (GameObject I in AppearObjects)
            {
                if (IB.BubbleItem.ItemName == AppearNames[Count].ItemName)
                {
                    //If the player can place the item then it will added to the scene
                    I.SetActive(true);
                    TempItem = AppearNames[Count];
                    Clicked = !(Clicked);
                    DoEvent = true;
                    Delete = true;
                }
                else
                {
                    I.SetActive(false);
                }
                Count += 1;
            }

            if (Delete == true)
            {
                //When placing an item it is removed from the player's inventory
                I.CurrentInventory = 0;
                DeleteItem();
            }
            DoEvent = false;
        }
        else
        {
            Nothingtoplace.AddLine();
        }
    }

    void TakeVine()
    {
        //This function is used to add a vine back to the player's inventory
            DoEvent = true;
            AppearObjects[0].SetActive(false);
            AppearObjects[1].SetActive(false);
            AppearObjects[2].SetActive(false);

            I.Items.Add(TempItem);
            TempItem = null;
            Clicked = false;
            DoEvent = false;
    }

    void DeleteItem()
    {
        //This function is used when adding a vine to the scene, removing it from the inventory list
        int i = 0;
        int Deletei = 0;
        bool Delete = false;
        foreach(ItemClass IT in I.Items)
        {
            if(IT.ItemName == TempItem.ItemName)
            {
                Deletei = i;
                Delete = true;
            }
            i += 1;
        }

        if (Delete == true)
        {
            I.Items.Remove(I.Items[Deletei]);
        }
    }
    
}
