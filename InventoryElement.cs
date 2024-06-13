using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    //This script is used to store the information stored in the currently held Inventory Item

    //This stores the data displayed by the currently held inventory item
    public ItemClass BubbleItem;
    public Image Icon;
    public TextMeshProUGUI Name;

    //This stores the Inventory script.
    [SerializeField] Inventory I;

    // Update is called once per frame
    void Update()
    {
        //This script works to present the icon and data for the currently held inventory item
        if (I.Items[I.CurrentInventory].ItemName != "No Item")
        {
            Icon.gameObject.SetActive(true);
            Icon.sprite = I.Items[I.CurrentInventory].ItemIcon;
        }
        else
        {
            Icon.gameObject.SetActive(false);
        }
        Name.text = I.Items[I.CurrentInventory].ItemName;

    }
}
