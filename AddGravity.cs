using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    //This script is used to add gravity to objects in the game.
    //It has extra functions for when it attached to a 2D Rock

    //This stores the Rigidbody of the object
    [SerializeField] Rigidbody BoxRigid;

    //This stores the EventsCode script
    [SerializeField] EventsCode E;

    //This stores the current position of the rock in the x and z axis
    [SerializeField] float x;
    [SerializeField] float z;
    //This checks when the Rock has collided with a wall
    [SerializeField] bool WallHit;

    //When this is true it means this script will also be used to calculate collisions
    [SerializeField] bool CollideCalc;

    //This stores the DecalArea of the current instance of the rock
    [SerializeField] GameObject DecalArea;

    // Update is called once per frame
    void Update()
    {
        //The update function is used to keep the player in the x and z axis positions stored in x and z when it collides with a wall.
        if(WallHit == true && CollideCalc == false)
        {
            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
        }

    }


    //The Gravity is called in the FixedUpdate to keep it consistent.
    private void FixedUpdate()
    {
        BoxRigid.AddForce(Physics.gravity * BoxRigid.mass, ForceMode.Acceleration);
    }


    //The Collision for this is only used for the 2D rocks.
    private void OnCollisionEnter(Collision collision)
    {
        if(CollideCalc == false)
        {
            //It checks if the rock has collided with a rock, and if it has then it freezes the position of the rock in the x and y axis.
            if (collision.gameObject.tag == "Wall")
            {
                x = gameObject.transform.position.x;
                z = gameObject.transform.position.z;
                WallHit = true;
            }

            //When colliding with the floor the rock no longer is parented to a moveplatform, and instead gets parented to the decalarea
            if (collision.gameObject.tag == "Floor")
            {
                WallHit = false;
                gameObject.transform.parent = DecalArea.transform.parent;
            }
        }
    }
}
