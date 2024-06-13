using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{

    //These variables store the position for the start and end of the movement
    [SerializeField] Vector3 StartPos, EndPos;
    //These variables are used to save the current start and end of the movement in the script.
    [SerializeField] Vector3 StartSave, EndSave;
    //These variables store the speed of the movement and the count of the lerp.
    [SerializeField] float movelerp, movespeed;

    //This stores the time the platform has to store, and the current save of the wait.
    [SerializeField] float WaitTime;
    [SerializeField] float WaitSave;

    [SerializeField] bool CanMove;

    [SerializeField] bool GoneOnce;

    //This bool when set shows that the platform is a 3D platform.
    public bool Platform3D;
    //This stores the Start of the platform for the 3D platform.
    [SerializeField] Vector3 originalstart;

    //This stores the Invisible Walls of 3D object.
    [SerializeField] GameObject Inviswalls;

    [SerializeField] bool Changed;
    // Start is called before the first frame update
    void Start()
    {
        //The start function sets up the movement of the platform
        StartSave = StartPos;
        EndSave = EndPos;
        gameObject.transform.localPosition = StartPos;

        originalstart = StartPos;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The movement for the platform is called in the FixedUpdate() so that it is consistent within the Build.
            if (CanMove == true)
            {
                Movement();
            }
    }

    private void Update()
    {
        //The Update() function checks whether it is a 3D platform, and if it is, it works to either keep the 3D platforms invisible walls. 
        if(Platform3D == true)
        {
            Inviswalls.SetActive(gameObject.transform.localPosition != originalstart);
        }
    }

    //This script is used to create the moving platform, having them move between two locations in a loop
    public void Movement()
    {
        //This function checks if the platform has already reached the end of the movement
        if(GoneOnce == false)
        {
            movelerp = Mathf.Clamp(movelerp + 1 * Time.deltaTime * movespeed, 0, 1);

            gameObject.transform.localPosition = Vector3.Lerp(StartPos, EndPos, movelerp);
        }

        //This checks the value of MoveLerp, and checks whether the platform needs to wait after reaching the end position. 
        if (movelerp >= 1)
        {
            if(GoneOnce == false)
            {
                GoneOnce = true;
                if (StartPos == StartSave)
                {
                    StartPos = EndSave;
                    EndPos = StartSave;
                }
                else if (StartPos != StartSave)
                {
                    StartPos = StartSave;
                    EndPos = EndSave;
                }
            }

            if(GoneOnce == true && WaitTime <= WaitSave)
            {
                WaitTime += 0.1f;
            }
            else if(GoneOnce == true && WaitTime >= WaitSave)
            {
                WaitTime = 0f;
                movelerp = 0.1f;
                GoneOnce = false;
            }
        }
    }


    //The script parents the 2D rock to the platform when it enters the trigger, so there are no glitches with collision.
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Rock")
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
    
    //This function is called by other scripts to change the end position of the moving platform.
    public void ChangeEnd(Vector3 NewEnd)
    {
        if(Changed == false)
        {
            Changed = true;
            StartPos = originalstart;
            EndPos = NewEnd;
            EndSave = NewEnd;

            //The speed of the platform is decreased so that it isn't too fast. 
            movespeed = 0.2f;
        }
    }
}
