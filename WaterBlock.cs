using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlock : MonoBehaviour
{
    //This script is attatched to the hole squirting water which affects the 2D platforms in the optional area. 


    //This stores the different decal platforms that show when the player hits the hole with the rock. 
    [SerializeField] GameObject[] RockShow;
    //This Stores the Water particles that appear from the hole in the wall. 
    [SerializeField] GameObject Water;
    //This stores the position that the rock will be placed in after colliding with the hole.
    [SerializeField] Vector3 RocPos;
    //This stores the instance of the rock that triggers the hole
    [SerializeField] GameObject RockObject;
    //This bool stores whether the hole has been hit or not.
    [SerializeField] bool BeenHit;

    // Update is called once per frame
    void Update()
    {
        //This checks the whether the object has been hit or not. 
        if(RockObject == null && BeenHit == true)
        {
            RockShow[0].SetActive(true);
            RockShow[1].SetActive(false);
            Water.SetActive(true);
            BeenHit = false;
        }
    }

    //This checks when the rock has collided with the hole in the wall, if it has then it sets the 2D scene up to be the unflooded platforms. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            RockObject = collision.gameObject;
            BeenHit = true;
            RockObject.transform.parent = gameObject.transform;
            RockObject.tag = "Untagged";
            RockObject.transform.localPosition = RocPos;
            RockObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            RockObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            RockObject.GetComponent<Rigidbody>().mass = 0;
            Water.SetActive(false);
            RockShow[0].SetActive(false);
            RockShow[1].SetActive(true);
        }
    }
}
