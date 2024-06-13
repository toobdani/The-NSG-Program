using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTrigger : MonoBehaviour
{
    //This script is attached to the FPS Player, and works to parent them to moving platforms when they are standing on them. 
    [SerializeField] GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MoveFloor")
        {
            Player.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MoveFloor")
        {
            Player.transform.parent = null;
        }
    }
}
