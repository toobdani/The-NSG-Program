using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPS_Movement : MonoBehaviour
{
    //This script is used to program the movement of the 3D character

    //This is the speed the player walks at
    public float MoveSpeed;

    //This stores the Transform of a empty object, which is used to make sure the player is moving forward relative to their rotated position.
    public Transform Orientation;

    //This is used to change the drag of the player's rigidbody, which affects the Rigidbody's speed.
    public float Drag;

    //These store the inputs of the player in the Horizontal and Vertical axis.
    float HInput;
    float VInput;

    //This stores the direction the player should be moving towards
    Vector3 moveDirection;

    //This stores the scripts Rigidbody
    Rigidbody myRigidbody;

    //This checks if the player has opened the inventory menu
    public bool openInventory;

    //This checks if the player can move
    public bool CanMove;

    //This stores the Inventory script
    [SerializeField] Inventory I;

    //This stores the camera's raycast
    [SerializeField] GameObject CR;

    //This checks if the player has pressed the I key
    public bool PressI;

    //This stores the AudioManager
    [SerializeField] audiomanager AM;

    //This stores the velocity the player moves at in the X and z axis
    [SerializeField] float xVel;
    [SerializeField] float zVel;

    //This checks if the player is moving or not
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        //This sets up the Player in the scene, attaching values to variables and making sure the player can move.

        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.freezeRotation = true;

        CanMove = true;

        PressI = true;
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() function sets up the speed max for the player and the movement inputs.
        FPSInput();
        SpeedContral();

        myRigidbody.drag = Drag;

        //Checks if the player is entering the inventory or closing it.
        if(PressI == true)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                openInventory = !(openInventory);
                if (openInventory == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    CanMove = false;
                    CR.SetActive(false);
                    I.OpenInventoryB(true);
                }
                else if (openInventory == false)
                {
                    PressI = false;
                    I.OpenInventoryB(false);
                }
            }
        }

        //This checks if the player is moving, and then players the footsteps audio when they are.
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            AM.Walking = true;
            moving = true;
        }
        else
        {
            AM.Walking = false;
            moving = false;
        }

        //When pressing Escape, the player gets brought back to the MainMenu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        //If the player can move, then the Movement functions are called in the FixedUpdate()
        if(CanMove == true)
        {
            PlayerMovement();
        }
    }

    private void FPSInput()
    {
        //Registers the player input
        HInput = Input.GetAxisRaw("Horizontal");
        VInput = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMovement()
    {
        //Calculates the player's movement
        moveDirection = Orientation.forward * VInput + Orientation.right * HInput;

        myRigidbody.AddForce(moveDirection.normalized * MoveSpeed, ForceMode.Force);
    }

    private void SpeedContral()
    {
        //Checks and maintains the speed of the player in movement.
        Vector3 SetVelocity = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);

        if(SetVelocity.magnitude > MoveSpeed)
        {
            Vector3 LimitVelocity = SetVelocity.normalized * MoveSpeed;
            myRigidbody.velocity = new Vector3(LimitVelocity.x, myRigidbody.velocity.y, LimitVelocity.z);
        }
    }
}
