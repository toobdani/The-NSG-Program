using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //This script is used to store the items of the inventory that the player is holding

    //This stores a List of every item in the inventory
    public List<ItemClass> Items;

    //This stores the size of the list of Inventory Items
    public int InventoryCount;

    //This stores which element of the Items List is currently selected
    public int CurrentInventory;

    //This stores the Animation for the Current item icon
    [SerializeField] GameObject AnimObject;

    //This stores the ButtonLocation script
    [SerializeField] ButtonLocation BL;

    //This stores the Button prefab used to open the menu
    [SerializeField] GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        //this sets the icon of the current item to be that which is held currently by the player
        AnimObject.GetComponent<InventoryElement>().BubbleItem = Items[CurrentInventory];
    }

    // Update is called once per frame
    void Update()
    {
        //This script is used to store the inventory and what item is currently being held
        AnimObject.GetComponent<InventoryElement>().BubbleItem = Items[CurrentInventory];
        InventoryCount = Items.Count;

        //The script takes in the mouse scroll data to quickly swap between inventory items
        Vector2 MouseScroll = Input.mouseScrollDelta;
        float MouseScrolly = MouseScroll.y;

        //The player can either press the square brackets or the mouse scroll to swap between inventory items
        if (Input.GetKeyDown(KeyCode.LeftBracket) || MouseScrolly < 0)
        {
            ChangeNumber(false);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket) || MouseScrolly > 0)
        {
            ChangeNumber(true);
        }

    }

    //When the inventory is opened by the 3D player the script makes a Inventory Item button for each element stored in the inventory, and adds it to an list stored in the ButtonLocation script. 
    public void OpenInventoryB(bool open)
    {
        switch(open)
        {
            case true:
                {
                    int Count = 0;
                    foreach (ItemClass I in Items)
                    {
                        BL.Buttons.Add(Instantiate(Button, BL.DefaultPos.GetComponent<RectTransform>().position, BL.DefaultPos.GetComponent<RectTransform>().rotation));
                        BL.Buttons[Count].GetComponent<RectTransform>().SetParent(BL.gameObject.GetComponent<RectTransform>(), true);
                        BL.Buttons[Count].GetComponent<RectTransform>().localScale = BL.InventoryItem.GetComponent<RectTransform>().localScale;
                        BL.Buttons[Count].gameObject.GetComponent<ButtonInstance>().BubbleItem = Items[Count];
                        BL.Buttons[Count].gameObject.GetComponent<ButtonInstance>().InventoryNumber = Count;
                        Count += 1;
                    }
                    BL.Move = 1;
                }
                break;
            case false:
                {

                    //When this function is called to close the inventory the ButtonLocation script is called to close the inventory
                    BL.Move = 4;
                }
                break;
        }    
       
    }


    void ChangeNumber(bool Up)
    {
        //This function is used to add or decrease the value of the inventory, so the player can swap between items
        switch(Up)
        {
            case true:
            {
                    if(CurrentInventory == (InventoryCount - 1))
                    {
                        CurrentInventory = 0;
                    }
                    else
                    {
                        CurrentInventory += 1;
                    }
            }
            break;
            case false:
            {
                    if (CurrentInventory == 0)
                    {
                        CurrentInventory = (InventoryCount - 1);
                    }
                    else
                    {
                        CurrentInventory -= 1;
                    }
                }
            break;
        }
    }

}
