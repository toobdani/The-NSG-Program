using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mega_Man_Movement : MonoBehaviour
{
    //This script is used to move the Mega Man Player
    //The script uses similar code to that of DecalMovement, so to balance things this script will focus on the new elements only.


    public Rigidbody BoxRigid;
    public float Sped, TurnCheck, PosDisChecks, PlayDisChecks, rotationspeed;

    public bool MoveRight;

    public bool SettingUp;

    public Animator Fade;

    public GameObject OriginMark, TargetMark;

    private CornerTrigger OriginCT;
    private CornerTrigger TargetCT;
    public Vector3 originPos => OriginMark.transform.position;
    public Vector3 targetPos => TargetMark.transform.position;

    public Vector3 OriginPosCheck;

    public bool isMove, isRotate;

    public Vector3 SetNormal;
    public Vector3 CurrentNormal;
    public Vector3 SetLocation;

    public float lerpaddition;

    public float rotationLerp, Lerpaddition;
    public Vector3 MoveLocation, MoveNormal, StartLocation, StartNormal;
    [SerializeField] Quaternion StartRFrom, MoveRTo;

    public bool invertvalue;

    public bool AlreadyInteracted;

    public GameObject[] Markers;

    //This bool stores whether the player is shooting normal or evil pollen
    public bool pollenbad;

    public bool JumpAble, LeftAble, RightAble;


    [SerializeField] Vector3 MoveC_LTo, StartC_LFrom;
    [SerializeField] Quaternion MoveC_RTo, StartC_RFrom;
    [SerializeField] float MoveC_FOVTo, StartC_FOVFrom;

    [SerializeField] bool CameraChange;

    [SerializeField] Camera GameCamera;

    [SerializeField] bool CheckNormal;

    [SerializeField] CornerTrigger NoCT;

    //[SerializeField] int MoveorRot;


    [SerializeField] bool debug;

    //This array stores the decals for the left and right sprites of the player
    [SerializeField] GameObject[] Decals;

    //This array stores the different pollen types shot.
    [SerializeField] GameObject[] Pollen;

    //This array stores the points that pollen spawn at, which gets saved as the Spawn variable.
    [SerializeField] GameObject[] BulletSpawn;
    [SerializeField] GameObject Spawn;

    //This bool is used to check if the pollen is being shot from the left or right
    private bool PollenMoveRight;

    //This stores the current instance of pollen shot from the player
    private GameObject TempPollen;

    //These arrays store the regular and shooting sprites for the left and right decal sprites
    [SerializeField] GameObject[] LeftSprites;
    [SerializeField] GameObject[] RightSprites;

    //These are used for the cooldown between pollen shots.
    [SerializeField] bool CanShootPollen;
    [SerializeField] float Pollentimer;

    //This stores the Animator for the player character
    [SerializeField] Animator Anim;

    [SerializeField] DecalDeath DD;

    //This bool can be called from external scripts to pause player shooting.
    public bool CanShoot;
    // Update is called once per frame

    private void Start()
    {
        Anim.speed = 0;
        isMove = true;
        isRotate = false;
        if (debug == true)
        {
            if (CheckNormal == true)
            {
                SetNormal = gameObject.transform.forward;
            }
            else if (CheckNormal == false)
            {
                SetStart(false, Vector3.zero, Vector3.zero);
            }
        }

    }

    //This script functions practically the same as the Decal_Movement script, with some small changes
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }

        CurrentNormal = gameObject.transform.forward;
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


        OriginPosCheck = originPos;
        PosDisChecks = Vector3.Distance(originPos, targetPos);
        PlayDisChecks = Vector3.Distance(gameObject.transform.position, originPos);
        float axis = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Q))
        {
            DD.Die();
        }

        MoveRight = axis > 0 ? true : false;

        //The script checks the value of axis to determine which direction the player is facing, and as such which sprites should appear and where the pollen should be shot from
        if (axis != 0)
        {
            Decals[0].SetActive(MoveRight);
            Decals[1].SetActive(!MoveRight);

            switch (MoveRight)
            {
                case true:
                    {
                        Anim = RightSprites[0].GetComponent<Animator>();
                        Spawn = BulletSpawn[0];
                        PollenMoveRight = true;
                    }
                    break;
                case false:
                    {
                        Anim = LeftSprites[0].GetComponent<Animator>();
                        Spawn = BulletSpawn[1];
                        PollenMoveRight = false;
                    }
                    break;
            }
        }

        //The script checks whether the player is moving to determine if the animation should move
        if (axis != 0)
        {
            Anim.speed = 0.5f;
        }
        else
        {
            Anim.speed = 0;
        }
        bool RightorLeft = Decals[0].activeSelf;

        //When left clicking the script swaps what type of bullet is shot by the player
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            pollenbad = !(pollenbad);
        }

        //When pressing left click the player will shoot a bullet in whatever direction they're facing
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanShoot == true)
        {
            ShootPollen(RightorLeft, pollenbad);
        }
        if (isMove == true && isRotate == false)
        {
            Vector3 UpdateTarget = new Vector3(targetPos.x, gameObject.transform.position.y, targetPos.z);
            Vector3 UpdateOrigin = new Vector3(originPos.x, gameObject.transform.position.y, originPos.z);
            if (LeftAble == true && RightAble == true)
            {
                gameObject.transform.position = DirectionMove(axis, UpdateTarget, UpdateOrigin);
            }
            else if (LeftAble == false && RightAble == true)
            {
                if (MoveRight == true)
                {
                    gameObject.transform.position = DirectionMove(axis, UpdateTarget, UpdateOrigin);
                }
            }
            else if (LeftAble == true && RightAble == false)
            {
                if (MoveRight == false)
                {
                    gameObject.transform.position = DirectionMove(axis, UpdateTarget, UpdateOrigin);
                }
            }

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

    private Vector3 DirectionMove(float axis, Vector3 UpdateTarget, Vector3 UpdateOrigin)
    {
        return Vector3.MoveTowards(gameObject.transform.position, MoveRight ? UpdateTarget : UpdateOrigin, Mathf.Abs(axis) * Time.deltaTime * Sped);
    }

    private void FixedUpdate()
    {
        BoxRigid.AddForce(Physics.gravity * BoxRigid.mass, ForceMode.Acceleration);

        //The FixedUpdate is used for the cooldown between pollen shots
        if (CanShootPollen == false)
        {
            Pollentimer += 0.1f;
            if(Pollentimer >= 2)
            {
                CanShootPollen = true;
                Pollentimer = 0;
            }
        }
    }

    public void SetUpRotate(Vector3[] L, Vector3[] N, Quaternion[] Q, GameObject[] M)
    {
        float OriginDis = Vector3.Distance(gameObject.transform.position, L[0]);
        float TargetDis = Vector3.Distance(gameObject.transform.position, L[1]);

        bool NearOrigin = OriginDis < TargetDis ? true : false;

        AlreadyInteracted = true;
        isMove = false;
        isRotate = true;
        rotationLerp = .001f;

        MoveLocation = NearOrigin ? L[1] : L[0];
        MoveNormal = NearOrigin ? N[1] : N[0];

        StartLocation = NearOrigin ? L[0] : L[1];
        StartNormal = NearOrigin ? N[0] : N[1];

        MoveRTo = NearOrigin ? Q[1] : Q[0];
        StartRFrom = NearOrigin ? Q[0] : Q[1];

        Markers[0] = M[0];
        Markers[1] = M[1];
        Markers[2] = M[2];

        invertvalue = NearOrigin ? true : false;

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

    public void CornerRotation(float axis)
    {

        rotationLerp = Mathf.Clamp(rotationLerp + ((invertvalue ? axis : -axis) * Time.deltaTime * rotationspeed), 0, 1);

        gameObject.transform.forward = Vector3.Lerp(new Vector3(StartNormal.x, gameObject.transform.position.y, StartNormal.z), new Vector3(MoveNormal.x, gameObject.transform.position.y, MoveNormal.z), rotationLerp);

        gameObject.transform.position = Vector3.Lerp(new Vector3(StartLocation.x, gameObject.transform.position.y, StartLocation.z), new Vector3(MoveLocation.x, gameObject.transform.position.y, MoveLocation.z), rotationLerp);

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
            isMove = true;
            isRotate = false;
        }
        else if (can == false)
        {
            isMove = false;
            isRotate = false;
        }
    }

    IEnumerator PollenAnim(bool MRight)
    {
        //This function is used to swap sprites to the shooting sprite when the player shoots pollen.
        //This bases it off the direction is placed in. 
        switch(MRight)
        {
            case true:
                {
                    RightSprites[0].SetActive(false);
                    RightSprites[1].SetActive(true);
                    yield return new WaitForSecondsRealtime(0.5f);
                    RightSprites[0].SetActive(true);
                    RightSprites[1].SetActive(false);

                }
                break;
            case false:
                {
                    LeftSprites[0].SetActive(false);
                    LeftSprites[1].SetActive(true);
                    yield return new WaitForSecondsRealtime(0.5f);
                    LeftSprites[0].SetActive(true);
                    LeftSprites[1].SetActive(false);
                }
                break;
        }
    }

    public void ShootPollen(bool RightorLeft, bool PollenBad)
    {
        //This script spawns an instance of the pollen to be shot across the stage. 
        int pollennum;
        switch (PollenBad)
        {
            case false:
                {
                    pollennum = 0;
                }
                break;
            case true:
                {
                    pollennum = 1;
                }
                break;
        }
        //The type of pollen shot is based off the value of PollenBad
        switch (CanShootPollen)
        {
            case true:
                {
                    //When shooting pollen the function calls PollenAnim to change sprites.
                    CanShootPollen = false;
                    StartCoroutine(PollenAnim(RightorLeft));
                    //Pollen is spawned by instantiating one of the pollen prefabs, and stores the necessary values into the script.
                    TempPollen = Instantiate(Pollen[pollennum], Spawn.transform.position, Spawn.transform.rotation);
                    TempPollen.GetComponent<Pollen>().MoveRight = PollenMoveRight;
                    TempPollen.GetComponent<Pollen>().OriginMark = OriginMark;
                    TempPollen.GetComponent<Pollen>().TargetMark = TargetMark;
                    TempPollen = null;
                }
                break;
        }
    }

}
