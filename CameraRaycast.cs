using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.Rendering.VolumeComponent;

public class CameraRaycast : MonoBehaviour
{
    //This script produces a raycast from the 3D player which allows them to interact with objects in the scene.

    //This stores the range of the raycast
    [SerializeField] float range = 100f;

    //This stores the Layers that the Raycast can interact with
    [SerializeField] LayerMask LM;
    //This stores the 3D Player's camera
    public Camera GameCamera;

    //This stores the item hit by the raycast
    [SerializeField] GameObject InventoryItem;
    //This stores the hit items default layer type
    [SerializeField] int LayerDefault;

    //This shows the Press E UI when highlighting an interactable object. 
    [SerializeField] GameObject PressE;

    //This stores the diffrent CrossHair icons
    public GameObject[] CrossHair;

    //This stores the regular and highlighted materials for the cracks.
    public Material[] Cracks;

    //This sets the type of object highlighted.
    private int Type;

    //This is used to store highlighted DecalProjectors
    private DecalProjector DP;

    //This checks if the player is 2D.
    public bool Set2D;

    //This stores the start and end point of the raycast for Debugging purposes.
    [SerializeField] GameObject EndMarker;
    public GameObject StartMarker;

    // Start is called before the first frame update
    void Start()
    {
        //This start by setting the default cross hair icon.
        SetCrossHair(true, false, false, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        //This casts a raycatst, which calls the functions of collided objects, based on the type of object hit.
        EndMarker.transform.localPosition = new Vector3(0, 0, (range / 10));
        RaycastHit hit;
        if(Set2D == false)
        {
            if (Physics.Raycast(GameCamera.transform.position, GameCamera.transform.forward, out hit, range, LM))
            {
                if (hit.collider.tag == "InventoryItem")
                {
                    HitInventory(hit);
                }
                else if (hit.collider.tag == "Crack")
                {
                    Type = 2;
                    PressE.SetActive(true);
                    DP = hit.collider.gameObject.GetComponent<DecalProjector>();
                    DP.material = Cracks[1];
                }
                else
                {
                    EndHighlight();
                }
            }
            else
            {
                //if the raycast stops colliding with a object, then it calls EndHighlight.
                EndHighlight();
            }

            //This shows the length of the raycast in the Editor.
            Debug.DrawRay(GameCamera.transform.position, GameCamera.transform.forward * range, Color.green);
        }
    }

    //This function checks what item has been hit by the raycast, and uses this to allow the player to interact with scripts attached to those items. 
    //The crosshair of the scene changes icon depending on the type of interaction.
    //These items get highlighted via the CheckItem() function.
    void HitInventory(RaycastHit hit)
    {
        Type = 1;
        if (hit.collider.gameObject.GetComponent<ItemPickup>() != null)
        {
            PressE.SetActive(true);
            CheckItem(hit, 1);

            SetCrossHair(false, true, false, false, false);

            if (InventoryItem != null)
            {
                InventoryItem.GetComponent<ItemPickup>().EnterRay();
            }
        }
        else if (hit.collider.gameObject.GetComponent<mouseclick>() != null)
        {
            PressE.SetActive(true);
            CheckItem(hit, 2);

            SetCrossHair(false, false, true, false, false);

            if (InventoryItem != null)
            {
                InventoryItem.GetComponent<mouseclick>().Place();
            }
        }
        else if (hit.collider.gameObject.GetComponent<Projector>() != null)
        {
            PressE.SetActive(true);
            CheckItem(hit, 2);
        }
        else if (hit.collider.gameObject.GetComponent<Notes>() != null)
        {
            PressE.SetActive(true);
            CheckItem(hit, 2);
            SetCrossHair(true, false, false, false, false);

            if (InventoryItem != null)
            {
                InventoryItem.GetComponent<Notes>().PickUpNote();
            }
        }
        else if(hit.collider.gameObject.GetComponent<RockHit>() != null || hit.collider.gameObject.GetComponent<WaterBlock>() != null)
        {
            CheckItem(hit, 2);
            SetCrossHair(false, false, false, true, false);
        }
        else if(hit.collider.gameObject.GetComponent<GiveRockAbility3D>() != null)
        {
            PressE.SetActive(true);
            CheckItem(hit, 2);
            SetCrossHair(true, false, false, false, false);

            if (InventoryItem != null)
            {
                InventoryItem.GetComponent<GiveRockAbility3D>().TakeAbility();
            }
        }
        else if(hit.collider.gameObject.GetComponent<TeleportPlant>() != null)
        {
            if (hit.collider.gameObject.GetComponent<TeleportPlant>().Grown == true)
            {
                PressE.SetActive(true);
                CheckItem(hit, 2);
                SetCrossHair(true, false, false, false, false);
                if (InventoryItem != null)
                {
                    InventoryItem.GetComponent<TeleportPlant>().PlantTeleport();
                }
            }
            else if(hit.collider.gameObject.GetComponent<TeleportPlant>().Grown == false)
            {
                CheckItem(hit, 2);
                SetCrossHair(false, false, false, false, true);
            }
        }
        else if (hit.collider.gameObject.GetComponent<StartTimeline>() != null)
        {
            PressE.SetActive(true);
            CheckItem(hit, 2);
            SetCrossHair(true, false, false, false, false);
            if(InventoryItem != null)
            {
                InventoryItem.GetComponent<StartTimeline>().CallTimeline();
            }
        }
    }

    //This function is used to set the icon of the cross hair depending on what the raycast hits.
    private void SetCrossHair(bool one, bool two, bool three, bool four, bool five)
    {
        CrossHair[0].SetActive(one);
        CrossHair[1].SetActive(two);
        CrossHair[2].SetActive(three);
        CrossHair[3].SetActive(four);
        CrossHair[4].SetActive(five);
    }

    //This function is called when the Raycast should stop hitting objects, and works to reset everything back to the ways they were before being hit.
    void EndHighlight()
    {
        switch (Type)
        {
            case 0:
                {
                    if (InventoryItem != null)
                    {
                        InventoryItem.layer = LayerDefault;

                        SetCrossHair(true, false, false, false, false);
                    }
                    if (DP != null)
                    {
                        DP.material = Cracks[0];
                        DP = null;
                    }
                    PressE.SetActive(false);
                    Type = 0;
                }
                break;
            case 1:
                {
                    if (InventoryItem != null)
                    {
                        InventoryItem.layer = LayerDefault;

                        SetCrossHair(true, false, false, false, false);
                    }
                    InventoryItem = null;
                    LayerDefault = 0;
                    PressE.SetActive(false);
                    Type = 0;
                }
                break;
            case 2:
                {
                    DP.material = Cracks[0];
                    DP = null;
                    PressE.SetActive(false);
                    Type = 0;
                }
                break;
        }
    }

    //This function works to highlight the object hit by the raycast
    //If the InventoryItem is null, then this stores the hit item as it.
    //If the InventoryItem is not null, then the function returns the old InventoryItem to normal, before storing the new collision as the InventoryItem.
    void CheckItem(RaycastHit Hit, int LNumber)
    {

        if(InventoryItem == null)
        {
            InventoryItem = Hit.collider.gameObject;
            LayerDefault = InventoryItem.layer;
            InventoryItem.gameObject.layer = LayerMask.NameToLayer("ItemSelection");
        }
        else if (Hit.collider.gameObject != InventoryItem)
        {
            InventoryItem.layer = LayerDefault;

            InventoryItem = Hit.collider.gameObject;
            LayerDefault = InventoryItem.layer;
            InventoryItem.gameObject.layer = LayerMask.NameToLayer("ItemSelection");
        }
    }


}
