using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikevine : MonoBehaviour
{
    //This script was orignally used to delete the spike vines after being hit by pollen
    //This had to be removed as deleting the object caused issues with the collision detection.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EvilPollen")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Pollen")
        {
            Destroy(collision.gameObject);
        }
    }
}
