using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInstance : MonoBehaviour
{
    //This is used to store values to the instances of the Inventory Buttons, giving the player the stored item when clicked on

    //These are used to display the information stored in the ItemClass variable for the object.
    public ItemClass BubbleItem;
    public Image Icon;
    public TextMeshProUGUI Name;

    //These store the Iventory and ButtonLocation scripts
    [SerializeField] Inventory I;
    [SerializeField] ButtonLocation Bl;

    //This stores what value of the Inventory list that this item is at.
    public int InventoryNumber;

    // Start is called before the first frame update
    void Start()
    {
        //The Start() function attaches the Inventory and ButtonLocation script to their variables.
        I = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        Bl = GameObject.FindGameObjectWithTag("Button").GetComponent<ButtonLocation>();
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() function works to display the values stored in the variables to the button
        if (I.Items[InventoryNumber].ItemName != "No Item")
        {
            Icon.gameObject.SetActive(true);
            Icon.sprite = I.Items[InventoryNumber].ItemIcon;
        }
        else
        {
            //If the Item is "No Item" then that means no icon is displayed
            Icon.gameObject.SetActive(false);
        }
        Name.text = I.Items[InventoryNumber].ItemName;
    }

    public void ClickInventory()
    {
        //When the player clicks the button, the value of the Inventory script's CurrentInventory is set to the InventoryNumber stored in the instance.
        I.CurrentInventory = InventoryNumber;
    }
}
