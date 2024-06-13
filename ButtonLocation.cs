using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ButtonLocation : MonoBehaviour
{
    //This script is used to store the positions and list for each button presented in the Inventory Menu when it's open.

    //This stores a List of each button in the menu
    public List<GameObject> Buttons;
    //This stores the position that each button needs to move towards
    public GameObject[] ButtonPos;
    //This is used to call how the buttons should move 
    public int Move;
    //This stores the number of buttons present in the List.
    [SerializeField] int BCount;
    //This stores the speed at which the buttons should move
    [SerializeField] float Movespeed;
    //This stores the defualt position the buttons should move back to
    public GameObject DefaultPos;

    //This stores the 3D Player
    [SerializeField] FPS_Movement FPSM;

    //This stores the InventoryItem that appears on screen when the Inventory menu isn't open
    public GameObject InventoryItem;

    //This stores the Text that displays telling the player how to close the menu
    [SerializeField] GameObject Close;

    //This stores the CrossHairs in the game
    [SerializeField] GameObject CR;

    // Start is called before the first frame update
    void Start()
    {
        //The start function starts by making sure the Inventory text is not active.
        Close.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The FixedUpdate() is used to check where in the movement process the menu is at
        switch (Move)
        {
            case 1:
                {
                    //When Move is 1 it means the menu was just opened, it works to count each item stored in the list
                    InventoryItem.SetActive(false);
                    Close.SetActive(true);
                    BCount = 0;
                    foreach (GameObject G in Buttons)
                    {
                        BCount += 1;
                    }
                    Move = 2;
                }
                break;
            case 2:
                {
                    //When Move is 2 it means the buttons are moving over the scene
                    MoveButtons(BCount);
                }
                break;
            case 4:
                {
                    //When Move is 4 it means the buttons are moving back to close the menu
                    MoveButtonsBack(BCount);
                }
                break;
            case 5:
                {
                    //When Move is 5 it sets up the game to go back to movement, with the menu being reset and closed
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    FPSM.CanMove = true;
                    CR.SetActive(true);

                    if (Buttons.Count > 0)
                    {
                        foreach (GameObject G in Buttons)
                        {
                            Destroy(G);
                        }
                        Buttons.Clear();
                    }
                    InventoryItem.SetActive(true);
                    Close.SetActive(false);
                    Move = 0;
                    FPSM.PressI = true;
                    
                }
                break;
        }
    }

    void MoveButtons(int bCount)
    {
        //This function moves each button towards their position one at a time. When the furthest button reaches its position it means the movement is complete
        for (int i = 0; i < bCount; i++)
        {
            if (i > 0)
            {
                Buttons[i].GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(Buttons[i].GetComponent<RectTransform>().localPosition, ButtonPos[i].GetComponent<RectTransform>().localPosition, Movespeed);
            }
        }

        int value = bCount -= 1;
        if (Buttons[value].GetComponent<RectTransform>().localPosition == ButtonPos[value].GetComponent<RectTransform>().localPosition || Buttons.Count == 1)
        {
            Move = 3;
        }
    }

    void MoveButtonsBack(int bCount)
    {
        //This function moves each button back towards the start when the menu is being closed, when the furtherst button reaches the start position it means the menu has been closed.
        for (int i = 0; i < bCount; i++)
        {
            if (i > 0)
            {
                Buttons[i].GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(Buttons[i].GetComponent<RectTransform>().localPosition, DefaultPos.GetComponent<RectTransform>().localPosition, Movespeed);
            }
        }

        int value = bCount -= 1;
        if (Buttons[value].GetComponent<RectTransform>().localPosition == DefaultPos.GetComponent<RectTransform>().localPosition || Buttons.Count == 1)
        {
            Move = 5;
        }
    }
}
