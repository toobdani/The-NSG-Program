using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlant : MonoBehaviour
{
    //This script is placed on objects that are affected by the 3D pollen bullets

    //This stores the different objects that get swapped by the bullet
    [SerializeField] GameObject[] SwapObject;

    //This bool is set to true if the object is a Teleport plant, and makes it so the plant cannot grow again once hit.
    public bool Grown;

    //These store the 3D player and Fade UI, for when the player teleports
    [SerializeField] GameObject Player;
    [SerializeField] Animator Fade;

    //These store the new position and rotation of the player after telelporting.
    [SerializeField] Vector3 NewPos;
    [SerializeField] Quaternion NewRot;

    //This bool is set to true whilst the player is teleporting
    [SerializeField] bool Teleporting;

    //This bool, when true, means it is a vine which can be grown using the same script.
    [SerializeField] bool Vine;

    //This function is called when either a sprout or vine gets hit by the pollen bullet.
    public void GrowPlant()
    {
        //If it is a sprout, then that means the object will grow into a teleport plant, which allows the player to teleport between areas.
        if(Vine == false)
        {
            SwapObject[0].SetActive(true);
            SwapObject[1].SetActive(true);
            SwapObject[2].SetActive(false);
            gameObject.SetActive(false);
        }
        else if(Vine == true)
        {
            //If it is a vine, then that means the object will swap to a different vine size.
            SwapObject[0].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //If the player interacts with the TeleportPlant, then they can be teleported.
    public void PlantTeleport()
    {
        if (Input.GetKey(KeyCode.E) && Teleporting == false)
        {
            Teleporting = true;
            StartCoroutine(TeleportPlayer());
        }
    }

    //This IEnumerator works to warp the player to a new location.
    IEnumerator TeleportPlayer()
    {
        Fade.SetBool("Fade", true);
        yield return new WaitForSecondsRealtime(1f);
        Player.transform.position = NewPos;
        Player.transform.rotation = NewRot;
        Fade.SetBool("Fade", false);
        Teleporting = false;
    }
}
