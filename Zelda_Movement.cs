using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zelda_Movement : MonoBehaviour
{
    //This script is the for Zelda Gameplay style, which never made it past the prototyping stages of development.
    //This will want to be set in a full game, but is currently unneeded, as such, there will not be many notes.


    public Rigidbody BoxRigid;
    public float Sped, TurnCheck, PosDisChecks, PlayDisChecks, rotationspeed, PlayYDisChecks, PosYDisChecks;

    public bool MoveRight, MoveUp;

    public bool SettingUp;

    public Animator Fade;

    public GameObject OriginMarkX, TargetMarkX;
    public GameObject OriginMarkY, TargetMarkY;

    private CornerTrigger OriginCT;
    private CornerTrigger TargetCT;

    private CornerTrigger OriginCTY;
    private CornerTrigger TargetCTY;
    public Vector3 originPosX => OriginMarkX.transform.position;
    public Vector3 targetPosX => TargetMarkX.transform.position;

    public Vector3 originPosY => OriginMarkY.transform.position;
    public Vector3 targetPosY => TargetMarkY.transform.position;

    public Vector3 OriginPosCheck;
    public Vector3 OriginYPosCheck;

    //This checks if the player is moving and rotating the X and Y axis.
    public bool isMoveX, isRotateX;
    public bool isMoveY, isRotateY;

    public Vector3 SetNormal;
    public Vector3 CurrentNormal;
    public Vector3 SetLocation;

    public float lerpaddition;

    public float rotationLerp, Lerpaddition, rotationLerpY;
    public Vector3 MoveLTo, MoveNTo, StartLFrom, StartNFrom;
    public Vector3 MoveLToY, MoveNToY, StartLFromY, StartNFromY;
    [SerializeField] Quaternion StartRFrom, MoveRTo;
    [SerializeField] Quaternion StartRFromY, MoveRToY;

    public bool invertvalue, invertvalueY;

    public bool AlreadyInteracted;

    public GameObject[] Markers, MarkersY;


    public bool JumpAble, LeftAble, RightAble;


    [SerializeField] Vector3 MoveC_LTo, StartC_LFrom;
    [SerializeField] Quaternion MoveC_RTo, StartC_RFrom;
    [SerializeField] float MoveC_FOVTo, StartC_FOVFrom;

    [SerializeField] bool CameraChange;

    [SerializeField] Camera GameCamera;

    [SerializeField] bool CheckNormal;

    [SerializeField] CornerTrigger NoCT;

    //[SerializeField] int MoveorRot;

    [SerializeField] bool CheckMove;

    [SerializeField] bool debug;

    //This stores the decals of the player
    [SerializeField] GameObject[] Decals;

    //This is true when the player swings their sword.
    public bool Sword;

    //This is called when the player has finished swinging their sword. 
    private bool SwungSword;

    // Update is called once per frame

    private void Start()
    {
        isMoveX = true;
        isRotateX = false;        
        
        isMoveY = true;
        isRotateY = false;

        Decals[0].SetActive(true);
        Decals[1].SetActive(false);

        Sword = false;

        CheckMove = true;
        if (debug == true)
        {
            if (CheckNormal == true)
            {
                SetNormal = gameObject.transform.forward;
            }
            else if (CheckNormal == false)
            {
                //SetStart(false, Vector3.zero, Vector3.zero);
            }
        }

    }

    //This script is basically the same as DecalMovement, with the only difference being that it can move in the X and Y axis.
    void Update()
    {
        CurrentNormal = gameObject.transform.forward;
        if (OriginMarkX.GetComponent<CornerTrigger>() != null)
        {
            OriginCT = OriginMarkX.GetComponent<CornerTrigger>();
        }
        else
        {
            OriginCT = NoCT.GetComponent<CornerTrigger>();
        }
        if (TargetMarkX.GetComponent<CornerTrigger>() != null)
        {
            TargetCT = TargetMarkX.GetComponent<CornerTrigger>();
            Debug.Log(TargetCT.TurnCheck[0]);
        }
        else
        {
            TargetCT = NoCT.GetComponent<CornerTrigger>();
        }

        if(OriginMarkY.GetComponent<CornerTrigger>() != null)
        {
            OriginCTY = OriginMarkY.GetComponent<CornerTrigger>();
        }
        else
        {
            OriginCTY = NoCT.GetComponent<CornerTrigger>();
        }
        if (TargetMarkY.GetComponent<CornerTrigger>() != null)
        {
            TargetCTY = TargetMarkY.GetComponent<CornerTrigger>();
        }
        else
        {
            TargetCTY = NoCT.GetComponent<CornerTrigger>();
        }

        /*if (CheckMove == true)
        {
            if (isMoveX == true)
            {
                MoveorRot = 1;
            }
            else if (isRotateX == true)
            {
                MoveorRot = 2;
            }
        }*/

        //The player can swing their sword when left clicking
        if(Input.GetKeyDown(KeyCode.Mouse0) && SwungSword == false)
        {
            SwungSword = true;
            StartCoroutine(SwordSwipe());
        }




        OriginPosCheck = originPosX;
        PosDisChecks = Vector3.Distance(originPosX, targetPosX);
        PlayDisChecks = Vector3.Distance(new Vector3(gameObject.transform.position.x, originPosX.y, originPosX.z), originPosX);

        OriginYPosCheck = originPosY;
        PosYDisChecks = Vector3.Distance(originPosY, targetPosY);
        PlayYDisChecks = Vector3.Distance(new Vector3(originPosY.x, gameObject.transform.position.y, originPosY.z), originPosY);


        //To move the player in the two axis, the script needs to call two seperate functions for the movement
        MoveX();
        MoveY();

    }

    private void FixedUpdate()
    {
    }

    private void MoveX()
    {
        float axisX = Input.GetAxis("Horizontal");
        MoveRight = axisX > 0 ? true : false;
        if (isMoveX == true && isRotateX == false)
        {
            Vector3 UpdateTarget = new Vector3(targetPosX.x, gameObject.transform.position.y, targetPosX.z);
            Vector3 UpdateOrigin = new Vector3(originPosX.x, gameObject.transform.position.y, originPosX.z);

            if (LeftAble == true && RightAble == true)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axisX) * Time.deltaTime * Sped);

            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axisX) * Time.deltaTime * Sped);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axisX) * Time.deltaTime * Sped);
                }
            }

            if (Vector3.Distance(gameObject.transform.position, UpdateOrigin) > (Vector3.Distance(UpdateOrigin, UpdateTarget) - TargetCT.TurnCheck[1]))
            {
                if (TargetMarkX.GetComponent<CornerTrigger>() != null)
                {
                    TargetMarkX.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, UpdateOrigin) < OriginCT.TurnCheck[0])
            {
                if (OriginMarkX.GetComponent<CornerTrigger>() != null)
                {
                    OriginMarkX.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
        }
        else if (isMoveX == false && isRotateX == true)
        {
            if (LeftAble == true && RightAble == true)
            {
                CornerRotationX(axisX);
            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    CornerRotationX(axisX);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    CornerRotationX(axisX);
                }
            }
        }

    }

    private void MoveY()
    {
        float axisY = Input.GetAxis("Vertical");
        MoveUp = axisY > 0 ? true : false;
        if (isMoveY == true && isRotateY == false)
        {
            Vector3 UpdateTargetY = new Vector3(gameObject.transform.position.x, targetPosY.y, targetPosY.z);
            Vector3 UpdateOriginY = new Vector3(gameObject.transform.position.x, originPosY.y, originPosY.z);

            if (LeftAble == true && RightAble == true)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveUp ? UpdateTargetY : UpdateOriginY, Mathf.Abs(axisY) * Time.deltaTime * Sped);

            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTargetY : UpdateOriginY, Mathf.Abs(axisY) * Time.deltaTime * Sped);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTargetY : UpdateOriginY, Mathf.Abs(axisY) * Time.deltaTime * Sped);
                }
            }

            if (Vector3.Distance(gameObject.transform.position, UpdateOriginY) > (Vector3.Distance(UpdateOriginY, UpdateTargetY) - TargetCTY.TurnCheck[1]))
            {
                if (TargetMarkY.GetComponent<CornerTrigger>() != null)
                {
                    TargetMarkY.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
            else if (Vector3.Distance(gameObject.transform.position, UpdateOriginY) < OriginCTY.TurnCheck[0])
            {
                if (OriginMarkY.GetComponent<CornerTrigger>() != null)
                {
                    OriginMarkY.GetComponent<CornerTrigger>().SetupRotate();
                }
            }
        }
        else if (isMoveY == false && isRotateY == true)
        {
            if (LeftAble == true && RightAble == true)
            {
                CornerRotationY(axisY);
            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    CornerRotationY(axisY);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    CornerRotationY(axisY);
                }
            }
        }

    }
    public void SetUpRotateX(Vector3[] L, Vector3[] N, Quaternion[] Q, GameObject[] M)
    {

        float OriginDis = Vector3.Distance(gameObject.transform.position, L[0]);
        float TargetDis = Vector3.Distance(gameObject.transform.position, L[1]);

        bool NearOrigin = OriginDis < TargetDis ? true : false;

        AlreadyInteracted = true;
        isMoveX = false;
        isRotateX = true;
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

    public void SetUpRotateY(Vector3[] L, Vector3[] N, Quaternion[] Q, GameObject[] M)
    {

        float OriginDis = Vector3.Distance(gameObject.transform.position, L[0]);
        float TargetDis = Vector3.Distance(gameObject.transform.position, L[1]);

        bool NearOrigin = OriginDis < TargetDis ? true : false;

        AlreadyInteracted = true;
        isMoveY = false;
        isRotateY = true;
        rotationLerpY = .001f;

        MoveLToY = NearOrigin ? L[1] : L[0];
        MoveNToY = NearOrigin ? N[1] : N[0];

        StartLFromY = NearOrigin ? L[0] : L[1];
        StartNFromY = NearOrigin ? N[0] : N[1];

        MoveRToY = NearOrigin ? Q[1] : Q[0];
        StartRFromY = NearOrigin ? Q[0] : Q[1];

        MarkersY[0] = M[0];
        MarkersY[1] = M[1];
        MarkersY[2] = M[2];

        invertvalueY = NearOrigin ? true : false;

    }

    public void SetUpCameraChange(Vector3[] L, Quaternion[] Q, float[] FOV)
    {
        MoveC_LTo = MoveRight ? L[1] : L[0];
        StartC_LFrom = MoveRight ? L[0] : L[1];

        MoveC_RTo = MoveRight ? Q[1] : Q[0];
        StartC_RFrom = MoveRight ? Q[0] : Q[1];

        MoveC_FOVTo = MoveRight ? FOV[1] : FOV[0];
        StartC_FOVFrom = MoveRight ? FOV[0] : FOV[1];

        CameraChange = true;
    }

    public void CornerRotationX(float axis)
    {

        rotationLerp = Mathf.Clamp(rotationLerp + ((invertvalue ? axis : -axis) * Time.deltaTime * rotationspeed), 0, 1);

        gameObject.transform.forward = Vector3.Lerp(new Vector3(StartNFrom.x, gameObject.transform.position.y, StartNFrom.z), new Vector3(MoveNTo.x, gameObject.transform.position.y, MoveNTo.z), rotationLerp);

        gameObject.transform.position = Vector3.Lerp(new Vector3(StartLFrom.x, gameObject.transform.position.y, StartLFrom.z), new Vector3(MoveLTo.x, gameObject.transform.position.y, MoveLTo.z), rotationLerp);

        gameObject.transform.rotation = Quaternion.Lerp(StartRFrom, MoveRTo, rotationLerp);

        if (CameraChange == true)
        {
            CameraChangeFunction(axis);
        }

        if (rotationLerp >= 1 || rotationLerp <= 0)
        {
            switch (invertvalue)
            {
                case true:
                    {
                        if (rotationLerp >= 1)
                        {

                            OriginMarkX = Markers[1];
                            TargetMarkX = Markers[2];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMarkX = Markers[0];
                            TargetMarkX = Markers[1];
                        }
                    }
                    break;
                case false:
                    {
                        if (rotationLerp >= 1)
                        {
                            OriginMarkX = Markers[0];
                            TargetMarkX = Markers[1];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMarkX = Markers[1];
                            TargetMarkX = Markers[2];
                        }
                    }
                    break;
            }


            Markers[0] = null;
            Markers[1] = null;
            Markers[2] = null;

            CameraChange = false;

            transform.parent = null;
            isMoveX = true;
            isRotateX = false;
            rotationLerp = .001f;
        }
    }

    public void CornerRotationY(float axis)
    {

        rotationLerpY = Mathf.Clamp(rotationLerpY + ((invertvalueY ? axis : -axis) * Time.deltaTime * rotationspeed), 0, 1);

        gameObject.transform.forward = Vector3.Lerp(new Vector3(gameObject.transform.position.x, StartNFromY.y, StartNFromY.z), new Vector3(gameObject.transform.position.x, MoveNToY.y, MoveNToY.z), rotationLerpY);

        gameObject.transform.position = Vector3.Lerp(new Vector3(gameObject.transform.position.x, StartLFromY.y, StartLFromY.z), new Vector3(gameObject.transform.position.x, MoveLToY.y, MoveLToY.z), rotationLerpY);

        gameObject.transform.rotation = Quaternion.Lerp(StartRFromY, MoveRToY, rotationLerpY);


        if (rotationLerpY >= 1 || rotationLerpY <= 0)
        {
            switch (invertvalueY)
            {
                case true:
                    {
                        if (rotationLerpY >= 1)
                        {

                            OriginMarkY = MarkersY[1];
                            TargetMarkY = MarkersY[2];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMarkY = MarkersY[0];
                            TargetMarkY = MarkersY[1];
                        }
                    }
                    break;
                case false:
                    {
                        if (rotationLerp >= 1)
                        {
                            OriginMarkY = MarkersY[0];
                            TargetMarkY = MarkersY[1];
                        }
                        if (rotationLerp <= 0)
                        {
                            OriginMarkY = MarkersY[1];
                            TargetMarkY = MarkersY[2];
                        }
                    }
                    break;
            }


            MarkersY[0] = null;
            MarkersY[1] = null;
            MarkersY[2] = null;

            transform.parent = null;
            isMoveY = true;
            isRotateY = false;
            rotationLerpY = .001f;
        }
    }

    public void CameraChangeFunction(float axis)
    {
        GameCamera.transform.localPosition = Vector3.Lerp(StartC_LFrom, MoveC_LTo, rotationLerp);
        GameCamera.transform.localRotation = Quaternion.Lerp(StartC_RFrom, MoveC_RTo, rotationLerp);
        GameCamera.fieldOfView = Mathf.Lerp(StartC_FOVFrom, MoveC_FOVTo, rotationLerp);
    }

    public void SetStart(bool newpos, Vector3 Newnormal, Vector3 NewPos)
    {
        if (newpos == false)
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

    public void LetMove(bool can)
    {
        if (can == true)
        {
            isMoveX = true;
            isRotateX = false;
        }
        else if (can == false)
        {
            isMoveX = false;
            isRotateX = false;
        }
    }

    //This IEnumerator works to swipe the players sword before returning to normal
    IEnumerator SwordSwipe()
    {
        Decals[0].SetActive(false);
        Decals[1].SetActive(true);
        Sword = true;
        yield return new WaitForSeconds(0.5f);
        Sword = false;
        Decals[0].SetActive(true);
        Decals[1].SetActive(false);
        SwungSword = false;
    }

    
}
