using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pollencollision : MonoBehaviour
{
    //This script is used to check the collision of the pullet bullets spawned by the 3D Flower gun.

    //These variables are used to give a time limit before the bullets delete themselves.
    public float currenttime;
    [SerializeField] float maxtime = 50f;

    //The FixedUpdate() calls the calculation of the timer for this bullet.
    private void FixedUpdate()
    {
        addTime();
    }

    //When hitting an object the bullet will destroy itself whilst checking what type of object it hid.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "InventoryItem")
        {
            if (collision.gameObject.GetComponent<TeleportPlant>() != null)
            {
                if (collision.gameObject.GetComponent<TeleportPlant>().Grown == false)
                {
                    collision.gameObject.GetComponent<TeleportPlant>().GrowPlant();
                }
            }
            if(collision.gameObject.GetComponent<Switch3D>() != null)
            {
                collision.gameObject.GetComponent<Switch3D>().HitSwitch();
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //This function is used to delete the bullet instance after a certain amount of time.
    public void addTime()
    {
        if (currenttime >= maxtime)
        {
            Destroy(gameObject);
        }
        else
        {
            currenttime += 0.1f;
        }
    }
}
