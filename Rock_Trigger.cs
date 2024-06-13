using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Rock_Trigger : MonoBehaviour
{
    //This script creates triggers which check whether the 2D Rock can be placed in a spot or not.

    //This stores the decal projector of the player
    [SerializeField]DecalProjector DP;
    //This is the material the DecalProjector swaps to when it can be placed
    [SerializeField]Material Good;
    //This is the material the DecalProjetor swaps to when it can't be placed
    [SerializeField]Material Bad;

    //CanPlace is the bool states when the player can place the rock or not.
    public bool CanPlace = true;

    // Update is called once per frame
    void Update()
    {
        //The material of the projector is effected by the value of CanPlace
        if(CanPlace == true)
        {
            DP.material = Good;
        }
        else if(CanPlace == false)
        {
            DP.material = Bad;
        }
    }

    //This script is used on the triggers that spawn the rock object, checking whether the rock can be placed.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "MoveFloor" || other.gameObject.tag == "Wall")
        {
            CanPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "MoveFloor" || other.gameObject.tag == "Wall" )
        {
            DP.material = Good;
            CanPlace = true;
        }
    }
}
