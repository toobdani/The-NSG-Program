using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlatforms : MonoBehaviour
{
    //This script is for the variaty of moving platforms in the game which only move when a switch is pressed. 

    //These Variables store the Start and Ending Positions of the Platform. 
    [SerializeField] Vector3 StartPos, EndPos;
    //these floats store the Lerp of the movement and the speed.
    [SerializeField] float movelerp, movespeed;

    //This bool is called in the Switch script and registers if the Switch has been pressed. 
    public bool Pressed;

    // Start is called before the first frame update
    void Start()
    {
        //This sets the position of the platform to be the StartPos
        gameObject.transform.localPosition = StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        //This checks the value of Pressed and calls functions based of it. 
        switch (Pressed)
        {
            case true:
                {
                    HeldMovement();
                }
                break;
            case false:
                {
                    ReleasedMovement();
                }
                break;
        }
        
    }

    //This function is called when the switch is pressed and moves the platform up.
    public void HeldMovement()
    {
        if(movelerp < 1)
        {
            movelerp = Mathf.Clamp(movelerp + 1 * Time.deltaTime * movespeed, 0, 1);

            gameObject.transform.localPosition = Vector3.Lerp(StartPos, EndPos, movelerp);
        }
    }

    //This function is called when the switch is released, which moves the platform back to the start position.
    public void ReleasedMovement()
    {
        if (movelerp > 0)
        {
            movelerp = Mathf.Clamp(movelerp - 1 * Time.deltaTime * movespeed, 0, 1);

            gameObject.transform.localPosition = Vector3.Lerp(StartPos, EndPos, movelerp);
        }
    }

    //Like with the MovingPlatform the script is desigend so that the rock stays parented to it. 
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
