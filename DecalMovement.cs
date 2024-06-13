using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class DecalMovement : MonoBehaviour
{
    //This script is the main movement script for the Atari player

    //This stores the Rigidbody of the Atari Player
    public Rigidbody BoxRigid;
    //This is a collection of floats, which store the speed of the player, the distance of the two markers and the speed of the rotation.
    public float Sped, PosDisChecks, PlayDisChecks, rotationspeed;

    public float Jumpheight;

    //This bool checks if the player is currently moving right.
    public bool MoveRight;

    //When this bool is true, it means the camera rotates in the their local positions.
    bool LocalR;

    //These GameObjects are the ones which the player moves between
    public GameObject OriginMark, TargetMark;

    //These store the CornerTrigger Scripts for the two GameObjects stored in OriginMark
    private CornerTrigger OriginCT;
    private CornerTrigger TargetCT;
    //These Vector3 variables automatically update to be the value of the positions of the OriginMark and TargetMark scripts. 
    public Vector3 originPos => OriginMark.transform.position;
    public Vector3 targetPos => TargetMark.transform.position;

    //This is a debugging variable used to quickly check the value stored in originPos.
    public Vector3 OriginPosCheck;

    //These bools are used to check if the player can move or rotate. Only one can be true at a time.
    public bool isMove, isRotate;

    //These variables are used for debugging purposes, either to set the normal and location of the player, or to update what the current normal is.
    public Vector3 SetNormal;
    public Vector3 CurrentNormal;
    public Vector3 SetLocation;

    //This float stores the lerp value at which the platform moves when rotating around corners.
    public float rotationLerp;
    //These Vectors store the start and end values for the player's corner movement, storing the start and end for the Location and Normal.
    public Vector3 MoveLTo, MoveNTo, StartLFrom, StartNFrom;
    //These Quaternion store the start and ending rotation position of the player around corners. 
    [SerializeField]Quaternion StartRFrom, MoveRTo;

    //This bool determines whether the value of the movement around corner needs to be inverted or not. 
    public bool invertvalue;

    //This array stores the markers used to set the next markers after rotating
    public GameObject[] Markers;

    //These bools are used to check if the player is able to jump or move left or right.
    public bool JumpAble, LeftAble, RightAble, CutsceneJump;

    //These variables stores values for the camrea around rotations, and the FOV of the camera.
    [SerializeField] Vector3 MoveC_LTo, StartC_LFrom;
    [SerializeField]Quaternion MoveC_RTo, StartC_RFrom;
    [SerializeField] float MoveC_FOVTo, StartC_FOVFrom;

    //This bool is true when the camera values change after a corner rotation
    [SerializeField] bool CameraChange;

    //This stores the camera for the 2D section
    public Camera GameCamera;

    //This stores the GameObject that will be used if the corner has no script attached
    [SerializeField] CornerTrigger NoCT;

    //This bool stores whether the rock should be active or not
    [SerializeField]bool RockActive;

    //This int stores whether the player is moving or rotating
    [SerializeField] int MoveorRot;

    //This bool determines whether the script should check if they are rotating or moving. 
    [SerializeField] bool CheckMove;

    //This bool checks if the player is setting a rock in the 2D space. 
    [SerializeField] bool SettingRock;
    
    //This GameObject is the actual rock stored in the script.
    [SerializeField] GameObject ActualRock;
    
    //This is a group of the Rock_Trigger script, which are to check whether the player can place a rock
    [SerializeField] Rock_Trigger LeftRock, RightRock, CurrentRock;

    //This bool is set to true when the player is standing on the rock.
    public bool RockOn;

    [SerializeField] EventsCode E;
    
    //This checks if the player's rock is inverted with selecting where to place the rock.
    [SerializeField] bool Invert;

    //This bool checks if the player is moving on the floor.
    [SerializeField] bool FLoor;

    //This bool is used to check what type of direction the rock needs to be in
    [SerializeField] int RockType;

    //This is used to check whether abilities are activated in AtariAbilitySave.
    [SerializeField]AtariAblilitySave AAS;

    //This int is used to set what direction the rock should fall in.
    [SerializeField] int rockgravity;

    //This stores the death trigger for the player
    public DecalDeath DD;
    
    //This stores the audiomanager for the player's movement. 
    [SerializeField] audiomanager AM;


    private void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();

        //The game starts off by having the player moving, and has the script check the current move type of the player for debugging purposes. 
        isMove = true;
        isRotate = false;

        CheckMove = true;

    }
    void Update()
    {
        //When pressing Escape the game will take the player back to the main menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }

        //When pressing Q the script automatically kills the player, so they can restart the current 2D section.
        if (Input.GetKeyDown(KeyCode.Q) && DD != null)
        {
            DD.Die();
        }

        //Checks the current normal of the player.
        CurrentNormal = gameObject.transform.forward;

        //Checks if the Markers have CornerTrigger components
        //If they don't have any attached, then the CT variable will contain NoCT.
        if (OriginMark.GetComponent<CornerTrigger>() != null)
        {
            OriginCT = OriginMark.GetComponent<CornerTrigger>();
        }
        else
        {
            OriginCT = NoCT.GetComponent<CornerTrigger>();
        }
        if (TargetMark.GetComponent<CornerTrigger>() != null)
        {
            TargetCT = TargetMark.GetComponent<CornerTrigger>();
        }
        else
        {
            TargetCT = NoCT.GetComponent<CornerTrigger>();
        }

        //This checks whether the player is rotating or moving and stores it in the MoveorRot int.
        if (CheckMove == true)
        {
            if (isMove == true)
            {
                MoveorRot = 1;
            }
            else if (isRotate == true)
            {
                MoveorRot = 2;
            }
        }

        //The script calls different movement functions based off the value of FLoor
        //This either means they will be moving in the Y-axis on the floor or X-axis on the wall.
        if (FLoor == false)
        {
            RegularGravity();
        }
        else if (FLoor == true)
        {
            FloorGravity();
        }

        //This changes the rotation of the rock if it is the one that is on the floor.
        if (ActualRock.activeSelf == false)
        {
            switch (RockType)
            {
                case 1:
                    {
                        ActualRock.transform.rotation = new Quaternion(0, 0.66320771f, 0, 0.748435378f);
                    }
                    break;
            }
        }

        //Allows the player to place the rock when clicking the left click
        if (Input.GetKeyDown(KeyCode.Mouse1) && AAS.AtariRock == true)
        {
            RockActive = !RockActive;

            //This also works to turn off the rock selection.
            if (RockActive == true)
            {
                RockSet(true);
            }
            else if (RockActive == false)
            {
                RockSet(false);
            }
        }

        //Allows the player to place the rock before them.
        else if (isMove == false && isRotate == false && SettingRock == true)
        {
            //This changes the direction of the RockTrigger depending on what direction the player is pressing.
            //If the player can place a rock down the triggers will stay green, if they can't it goes red. 
            if (Input.GetKeyDown(KeyCode.A))
            {
                AM.RockDirection.SetActive(true);
                AM.RockDirection.GetComponent<AudioSource>().Play();
                if (Invert == false)
                {
                    LeftRock.gameObject.SetActive(true);
                    RightRock.gameObject.SetActive(false);
                    CurrentRock = LeftRock;
                }
                else
                {
                    LeftRock.gameObject.SetActive(false);
                    RightRock.gameObject.SetActive(true);
                    CurrentRock = RightRock;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                AM.RockDirection.SetActive(true);
                AM.RockDirection.GetComponent<AudioSource>().Play();
                if (Invert == false)
                {
                    LeftRock.gameObject.SetActive(false);
                    RightRock.gameObject.SetActive(true);
                    CurrentRock = RightRock;
                }
                else
                {
                    LeftRock.gameObject.SetActive(true);
                    RightRock.gameObject.SetActive(false);
                    CurrentRock = LeftRock;
                }
            }

            //If the player can place the rock and places enter then the rock ability will be placed. 
            if (CurrentRock.CanPlace == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    RockFreeze();
                    AM.RockPlace.SetActive(true);
                    //When placing the rock the script resets all it's Rigidbody values so that it doesn't cause glitches. 
                    ActualRock.transform.position = CurrentRock.gameObject.transform.position;
                    ActualRock.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    if (RockType == 0)
                    {
                        ActualRock.transform.rotation = CurrentRock.gameObject.transform.rotation;
                    }
                    ActualRock.SetActive(true);
                    LeftRock.gameObject.SetActive(false);
                    RightRock.gameObject.SetActive(false);
                    RockSet(false);
                }
            }
        }
    }

    //The FixedUpdate calls the gravity calculations for the character
    //So that it stays consistent between the Editor and Build.
    private void FixedUpdate()
    {
        BoxRigid.AddForce(Physics.gravity * BoxRigid.mass, ForceMode.Acceleration);
    }


    public void RegularGravity()
    {
        //Calculates the players distance from the markers they are moving towards.
        OriginPosCheck = originPos;
        PosDisChecks = Vector3.Distance(originPos, targetPos);
        PlayDisChecks = Vector3.Distance(new Vector3(gameObject.transform.position.x, originPos.y, originPos.z), originPos);
        //Stores the input the player makes in the horizontal axis.
        float axis = Input.GetAxis("Horizontal");

        //Checks if the player is moving left or right based on the value of axis.
        MoveRight = axis > 0 ? true : false;

        //If the player presses whilst on the ground they will jump. 
        if (Input.GetKeyDown(KeyCode.Space) && JumpAble == true && SettingRock == false && CutsceneJump == false)
        {
            AM.Jump.SetActive(true);
            JumpAble = false;
            BoxRigid.AddForce(Vector3.up * Jumpheight, ForceMode.Acceleration);

            //If the player is standing on a rock it will despawn. 
            if (RockOn == true)
            {
                ActualRock.SetActive(false);
                RockOn = false;
            }
        }

        //If the player can move then they will move between the two markers on the wall.
        if (isMove == true && isRotate == false)
        {
            Vector3 UpdateTarget = new Vector3(targetPos.x, gameObject.transform.position.y, targetPos.z);
            Vector3 UpdateOrigin = new Vector3(originPos.x, gameObject.transform.position.y, originPos.z);

            MoveCall(axis, UpdateTarget, UpdateOrigin);

            //Calculates the distance the player is at from the corners, and if close enough causes the player to rotate around the corner. 
            if (Vector3.Distance(gameObject.transform.position, UpdateOrigin) > (Vector3.Distance(UpdateOrigin, UpdateTarget) - TargetCT.TurnCheck[1]))
            {
                if (TargetMark.GetComponent<CornerTrigger>() != null)
                {
                    TargetMark.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, UpdateOrigin) < OriginCT.TurnCheck[0])
            {
                if (OriginMark.GetComponent<CornerTrigger>() != null)
                {
                    OriginMark.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
        }
        //If the player is rotatating will check if they can move left or right, before rotating around the corner.
        else if (isMove == false && isRotate == true)
        {
            if (LeftAble == true && RightAble == true)
            {
                CornerRotation(axis);
            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    CornerRotation(axis);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    CornerRotation(axis);
                }
            }

        }
    }

    public void FloorGravity()
    {
        //Calculates the players distance from the markers they are moving towards.
        OriginPosCheck = originPos;
        PosDisChecks = Vector3.Distance(originPos, targetPos);
        PlayDisChecks = Vector3.Distance(new Vector3(originPos.x, gameObject.transform.position.y, originPos.z), originPos);

        //Stores the input the player makes in the horizontal axis.
        float axis = Input.GetAxis("Horizontal");

        //Checks if the player is moving left or right based on the value of axis.
        MoveRight = axis > 0 ? true : false;

        //If the player presses whilst on the ground they will jump. 
        if (Input.GetKeyDown(KeyCode.Space) && JumpAble == true && SettingRock == false && CutsceneJump == false)
        {
            AM.Jump.SetActive(true);
            JumpAble = false;
            BoxRigid.AddForce(Vector3.left * Jumpheight, ForceMode.Acceleration);

            //If the player is standing on a rock it will despawn. 
            if (RockOn == true)
            {
                ActualRock.SetActive(false);
                RockOn = false;
            }
        }

        //If the player can move then they will move between the two markers on the wall.
        if (isMove == true && isRotate == false)
        {
            Vector3 UpdateTarget = new Vector3(gameObject.transform.position.x, targetPos.y, targetPos.z);
            Vector3 UpdateOrigin = new Vector3(gameObject.transform.position.x, originPos.y, originPos.z);

            MoveCall(axis, UpdateTarget, UpdateOrigin);

            //Calculates the distance the player is at from the corners, and if close enough causes the player to rotate around the corner. 
            if (Vector3.Distance(gameObject.transform.position, UpdateOrigin) > (Vector3.Distance(UpdateOrigin, UpdateTarget) - TargetCT.TurnCheck[1]))
            {
                if (TargetMark.GetComponent<CornerTrigger>() != null)
                {
                    TargetMark.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, UpdateOrigin) < OriginCT.TurnCheck[0])
            {
                if (OriginMark.GetComponent<CornerTrigger>() != null)
                {
                    OriginMark.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
        }
        //If the player is rotatating will check if they can move left or right, before rotating around the corner.
        else if (isMove == false && isRotate == true)
        {
            if (LeftAble == true && RightAble == true)
            {
                CornerRotationY(axis);
            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    CornerRotationY(axis);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    CornerRotationY(axis);
                }
            }

        }

    }

    //This function sets in what direction the rock should be affected with gravity for, based on the values of rockgravity.
    void RockFreeze()
    {
        switch(rockgravity)
        {
            case 0:
                {
                    ActualRock.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                }
                break;
            case 1:
                {
                    ActualRock.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                }
                break;
        }
    }

    void MoveCall(float axis, Vector3 UpdateTarget, Vector3 UpdateOrigin)
    {
        //Checks if the player can move left or right
        if (LeftAble == true && RightAble == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axis) * Time.deltaTime * Sped);
        }
        else if (LeftAble == false && RightAble == true)
        {
            if (MoveRight == true)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axis) * Time.deltaTime * Sped);
            }
        }
        else if (LeftAble == true && RightAble == false)
        {
            if (MoveRight == false)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axis) * Time.deltaTime * Sped);
            }
        }
    }

    //Sets the values of the variables used in corner rotation calculation.
    public void SetUpRotate(Vector3[] L, Vector3[] N,Quaternion[] Q, GameObject[] M)
    {

        float OriginDis = Vector3.Distance(gameObject.transform.position, L[0]);
        float TargetDis = Vector3.Distance(gameObject.transform.position, L[1]);

        bool NearOrigin = OriginDis < TargetDis ? true : false;

        isMove = false;
        isRotate = true;
        rotationLerp = .001f;

        MoveLTo = NearOrigin ? L[1] : L[0];
        MoveNTo = NearOrigin ? N[1] : N[0];

        StartLFrom = NearOrigin ? L[0] : L[1];
        StartNFrom = NearOrigin ? N[0] : N[1];

        MoveRTo = NearOrigin ? Q[1] : Q[0];
        StartRFrom = NearOrigin ? Q[0] : Q[1];

        Markers[0] = M[0];
        Markers[1] = M[1];
        Markers[2] = M[2];

        invertvalue = NearOrigin ? true : false;
        
    }

    //If the camera needs to move will set up the variables that will be used.
    public void SetUpCameraChange(Vector3[] L, Quaternion[] Q, float[]FOV, bool Localr)
    {
        MoveC_LTo = MoveRight ? L[1] : L[0];
        StartC_LFrom = MoveRight ? L[0] : L[1];

        MoveC_RTo = MoveRight ? Q[1] : Q[0];
        StartC_RFrom = MoveRight ? Q[0] : Q[1];

        MoveC_FOVTo = MoveRight ? FOV[1] : FOV[0];
        StartC_FOVFrom = MoveRight ? FOV[0] : FOV[1];

        LocalR = Localr;

        CameraChange = true;
    }

    //Calculates the corner rotation
    public void CornerRotation(float axis)
    {
        //This functions moves the player around corners using a Lerp which takes in the value of axis. Once the player finishes the Lerp the script sets everything up for the player to move along the next wall type
        //Before resetting all values stored in SetupRotate. CornerRotationY does this exact same thing, just in the Y Axis.
        rotationLerp = Mathf.Clamp(rotationLerp + ((invertvalue ? axis:-axis) * Time.deltaTime * rotationspeed), 0, 1);
        
        gameObject.transform.forward = Vector3.Lerp(new Vector3(StartNFrom.x, gameObject.transform.position.y, StartNFrom.z), new Vector3(MoveNTo.x, gameObject.transform.position.y, MoveNTo.z), rotationLerp);

        gameObject.transform.position = Vector3.Lerp(new Vector3(StartLFrom.x, gameObject.transform.position.y, StartLFrom.z), new Vector3(MoveLTo.x, gameObject.transform.position.y, MoveLTo.z), rotationLerp);

        gameObject.transform.rotation = Quaternion.Lerp(StartRFrom, MoveRTo, rotationLerp);

        //If the camera needs to change with the rotation, then CameraChangeFunction is called. 
        if(CameraChange == true)
        {
            CameraChangeFunction(axis);
        }

        //Checks to see if the rotation is complete.
        if (rotationLerp >= 1 || rotationLerp <= 0)
        {
            switch (invertvalue)
            {
                case true:
                    {
                        if (rotationLerp >= 1)
                        {

                            OriginMark = Markers[1];
                            TargetMark = Markers[2];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMark = Markers[0];
                            TargetMark = Markers[1];
                        }
                    }
                    break;
                case false:
                    {
                        if (rotationLerp >= 1)
                        {
                            OriginMark = Markers[0];
                            TargetMark = Markers[1];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMark = Markers[1];
                            TargetMark = Markers[2];
                        }
                    }
                    break;
            }


            Markers[0] = null;
            Markers[1] = null;
            Markers[2] = null;

            CameraChange = false;

            transform.parent = null;
            isMove = true;
            isRotate = false;
            rotationLerp = .001f;
        }
    }

    public void CornerRotationY(float axis)
    {
        rotationLerp = Mathf.Clamp(rotationLerp + ((invertvalue ? axis : -axis) * Time.deltaTime * rotationspeed), 0, 1);

        gameObject.transform.forward = Vector3.Lerp(new Vector3(gameObject.transform.position.x, StartNFrom.y, StartNFrom.z), new Vector3(gameObject.transform.position.x, MoveNTo.y, MoveNTo.z), rotationLerp);

        gameObject.transform.position = Vector3.Lerp(new Vector3(gameObject.transform.position.x, StartLFrom.y, StartLFrom.z), new Vector3(gameObject.transform.position.x, MoveLTo.y, MoveLTo.z), rotationLerp);

        gameObject.transform.rotation = Quaternion.Lerp(StartRFrom, MoveRTo, rotationLerp);


        if (CameraChange == true)
        {
            CameraChangeFunction(axis);
        }


        //Checks to see if the rotation is complete.
        if (rotationLerp >= 1 || rotationLerp <= 0)
        {
            switch (invertvalue)
            {
                case true:
                    {
                        if (rotationLerp >= 1)
                        {

                            OriginMark = Markers[1];
                            TargetMark = Markers[2];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMark = Markers[0];
                            TargetMark = Markers[1];
                        }
                    }
                    break;
                case false:
                    {
                        if (rotationLerp >= 1)
                        {
                            OriginMark = Markers[0];
                            TargetMark = Markers[1];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMark = Markers[1];
                            TargetMark = Markers[2];
                        }
                    }
                    break;
            }


            Markers[0] = null;
            Markers[1] = null;
            Markers[2] = null;

            CameraChange = false;

            transform.parent = null;
            isMove = true;
            isRotate = false;
            rotationLerp = .001f;
        }
    }

    //Calculates the camera rotation
    public void CameraChangeFunction(float axis)
    {
        GameCamera.transform.localPosition = Vector3.Lerp(StartC_LFrom, MoveC_LTo, rotationLerp);
        if(LocalR == false)
        {
            GameCamera.transform.rotation = Quaternion.Lerp(StartC_RFrom, MoveC_RTo, rotationLerp);
        }
        GameCamera.fieldOfView = Mathf.Lerp(StartC_FOVFrom, MoveC_FOVTo, rotationLerp);
    }

    //This function determines where the 2D player should start, and is called by the GenreSwap script.
    public void SetStart(bool newpos, Vector3 Newnormal, Vector3 NewPos)
    {
        if(newpos == false)
        {
            gameObject.transform.forward = SetNormal;
            gameObject.transform.position = SetLocation;
        }
        else if (newpos == true)
        {
            gameObject.transform.forward = Newnormal;
            gameObject.transform.position = NewPos;
        }
    }

    //Used to set the value of the 2D player's movement
    public void LetMove(bool can)
    {
        if(can == true)
        {
            isMove = true;
            isRotate = false;
        }
        else if(can == false)
        {
            isMove = false;
            isRotate = false;
        }
    }

    //Called to set up the rock ability or deactivate it.
    void RockSet(bool Set)
    {
        if(Set == true)
        {
            isMove = false;
            isRotate = false;
            CheckMove = false;
            SettingRock = true;
            if(MoveRight == true)
            {
                LeftRock.gameObject.SetActive(true);
                CurrentRock = LeftRock;
            }
            else if(MoveRight == false)
            {
                RightRock.gameObject.SetActive(true);
                CurrentRock = RightRock;
            }
        }
        else if(Set == false)
        {
            SettingRock = false;
            switch (MoveorRot)
            {
                case 1:
                    {
                        isMove = true;
                    }
                    break;
                case 2:
                    {
                        isRotate = true;
                    }
                    break;
            }


            LeftRock.gameObject.SetActive(false);
            LeftRock.CanPlace = true;
            RightRock.gameObject.SetActive(false);
            RightRock.CanPlace = true;
            CurrentRock = null;
            CheckMove = true;
        }
    }



}
