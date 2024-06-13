using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera : MonoBehaviour
{
    //This works to rotate the First Person Character's camera view and to move the player in the direction the camera is facing. 

    //These store the sensitivety of the mouse in the X and Y axis.
    public float XSensitivity;
    public float YSensitivity;

    //This stores the Transform that affects the forward orientation of the player's movement. 
    public Transform Orientation;

    //These store the current rotation of the camera.
    float xRotation;
    float yRotation;

    //This stores the player's FPS_Movement script.
    [SerializeField] FPS_Movement FPSM;

    // Start is called before the first frame update
    void Start()
    {
        //The Start() function removes the mouse from the screen.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This script allows the FPS camera to rotate around a locked range.
        if(FPSM.CanMove == true)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * XSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * YSensitivity;

            yRotation += mouseX;
            xRotation -= mouseY;

            //Makes it so you can't look up or down more then 90 degrees
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Sets up the rotation of the camera and its orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
