using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class GenreSwap : MonoBehaviour
{
    //This is the script that is used to swap the player between the 2D and 3D states.

    //These GameObjects store the movement for the different gameplay styles.
    [SerializeField] DecalMovement DM;
    [SerializeField] Mega_Man_Movement MMM;

    //This stores the Fade animator when swapping the 2D styles. 
    [SerializeField] Animator Fade;
    //These store the variables needed for the different gameplay swaps, such as the markers the player will be moving between, the player and the cracks that are used for the transition
    [SerializeField] GameObject[] Markers;
    [SerializeField] GameObject MainPlayer;
    [SerializeField] GameObject Crack1, Crack2, TwoDCrack1, TwoDCrack2, Crack1Trigger, Crack2Trigger, TwoDCrack1Trigger, TwoDCrack2Trigger;
    [SerializeField] GameObject DecalArea;

    //These store the position and normal the 2D player will start at.
    [SerializeField] Vector3 newnormal, newpos;

    //This stores position and rotation the 3D player will start at.
    [SerializeField] Vector3 PlayerPos;
    [SerializeField] Quaternion PlayerRot;

    //This bool is used to check if the crack is swapping between the 2D or 3D gameplay
    [SerializeField] bool TwoDorThreeD;

    //This bool is set to true as the swap is being set up
    [SerializeField] bool SettingUp;

    //This bool is true when the player needs to start in their natural position
    [SerializeField] bool NaturalPos;

    //This stores a Dialogue instance that could play at the start of the 2D segment set up
    [SerializeField] TriggerDialogue TD;

    //This stores the Dialogue_Manager script
    [SerializeField] Dialogue_Manager DI_M;

    //This stores the Press E UI pop up
    [SerializeField] GameObject PressE;

    //This stores the Inventory UI
    [SerializeField] GameObject InventoryItem;

    //This stores the 3D player's CameraRaycast script
    [SerializeField] CameraRaycast CR;

    //This stores the DecalProject projection the script's instance's crack
    [SerializeField] DecalProjector DP;

    //This stores the Cross Hair appearing in the centre screen.
    [SerializeField] GameObject Cursor;

    //If the area needs to change the scene's gravity then it changes it using the SettingGravity script, based on the value of GravityChange.
    [SerializeField] string GravityChange;
    [SerializeField] SettingGravity SG;

    //This stores the positing and rotating that the 2D camera will need to be in at the start of play, if the SetCamera bool is true.
    [SerializeField] bool SetCamera;
    [SerializeField] Vector3 CameraPos;
    [SerializeField] Quaternion CameraRot;
    [SerializeField] GameObject FlatCamera;

    
    //This stores the audiomanager script
    [SerializeField] audiomanager AM;

    //This stores the 2D rock used in the 2D area
    [SerializeField] GameObject Rock;

    //This stores the PollenUI that appears with the Mega_Man Player
    [SerializeField] PollenUI pollenui;

    //This stores the Projector item that may be projecting the 2D scene
    [SerializeField] Projector P;

    //When this bool is true, it means the 2D player spawned is the Mega_Man. 
    [SerializeField] bool StartMegaMan;

    //This stores a voice line that can play before or after the transition
    [SerializeField] VoiceActing VA;

    //The Start() function stores the value of AM.
    private void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
    }

    //The Update() function checks if the player has highlighted the 3D crack, and if so waits for them to press E so it can set up the 2D scene
    private void Update()
    {
        if (TwoDorThreeD == true)
        {
            if (DP.material == CR.Cracks[1])
            {

                if (Input.GetKey(KeyCode.E))
                {
                    if (SettingUp == false)
                    {
                        PressE.SetActive(false);
                        AM.Crack.SetActive(true);
                        SettingUp = true;
                        switch (TwoDorThreeD)
                        {
                            case true:
                                {

                                    StartCoroutine(Setup2D());

                                }
                                break;
                            case false:
                                {
                                        StartCoroutine(Setup3D());

                                }
                                break;
                        }
                    }

                }
            }
        }
    }

    //When entering the Trigger Press E is set to true.
    private void OnTriggerEnter(Collider other)
    {
        if (TwoDorThreeD == false)
        {
            if (other.gameObject.tag == "Player")
            {
                PressE.SetActive(true);
            }
        }
    }

    //This script will allow the player to swap between the 3D or 2D space when pressing E.
    private void OnTriggerStay(Collider other)
    {
        if (TwoDorThreeD == false)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (SettingUp == false)
                    {
                        PressE.SetActive(false);
                        SettingUp = true;
                        AM.Crack.SetActive(true);
                        switch (TwoDorThreeD)
                        {
                            case true:
                                {
                                    StartCoroutine(Setup2D());
                                }
                                break;
                            case false:
                                {
                                        StartCoroutine(Setup3D());

                                }
                                break;
                        }
                    }

                }
            }
        }
    }

    //When exiting the trigger PressE is set to false. 
    private void OnTriggerExit(Collider other)
    {
        PressE.SetActive(false);
    }


    //Sets up the swap from the 3D gameplay to the 2D gameplay.
    public IEnumerator Setup2D()
    {
        //This removes the UI in the 3D space before fading into the gampelay transition
        CR.Set2D = true;
        Cursor.SetActive(false);
        DP.material = CR.Cracks[0];
        if(VA != null)
        {
            VA.AddLine();
        }
        Fade.SetBool("Fade", true);
        InventoryItem.SetActive(false);

        if (DM != null)
        {
            DM.LetMove(false);
        }
        if (MMM != null)
        {
            MMM.LetMove(false);
        }

        yield return new WaitForSeconds(0.4f);

        //This then deactivates the 3D Player and cracks before activating the 2D cracks
        MainPlayer.SetActive(false);
        Crack1.SetActive(false);
        Crack2.SetActive(false);
        DecalArea.SetActive(true);
        if (TwoDCrack1.activeSelf == true)
        {
            TwoDCrack1Trigger.SetActive(true);
        }
        TwoDCrack2Trigger.SetActive(true);

        //The script checks which player type needs to be activated, and activates the necessary player character.
        if (DM != null && StartMegaMan == false)
        {
            DM.gameObject.SetActive(true);
            DM.OriginMark = Markers[0];
            DM.TargetMark = Markers[1];
            DM.SetStart(NaturalPos, newnormal, newpos);
        }

        if (MMM != null && StartMegaMan == true)
        {
            MMM.gameObject.SetActive(true);
            MMM.OriginMark = Markers[0];
            MMM.TargetMark = Markers[1];
            MMM.SetStart(NaturalPos, newnormal, newpos);
        }

        //This sets the 2D camera if it is needed.
        if (SetCamera == true)
        {
            FlatCamera.transform.localPosition = CameraPos;
            FlatCamera.transform.localRotation = CameraRot;
        }

        //This sets up the gravity change if it is needed.
        if (SG != null)
        {
            SG.GravityChange(GravityChange);
            if (DM != null)
            {
                DM.transform.rotation = PlayerRot;
            }
            if (MMM != null)
            {
                MMM.transform.rotation = PlayerRot;
            }
        }
        if(FlatCamera != null)
        {
            FlatCamera.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        Fade.SetBool("Fade", false);

        //The function finishes by removing the fade and letting the player begin gameplay.
        if (MMM != null)
        {
            pollenui.gameObject.SetActive(true);
            pollenui.MMM = MMM;
        }
        yield return new WaitForSeconds(0.4f);

        if (TD == null || TD.gameObject.activeSelf == false)
        {
            if (DM != null)
            {
                DM.LetMove(true);
            }

            if (MMM != null)
            {
                MMM.LetMove(true);
            }
        }
        else
        {
            if (DM != null)
            {
                DI_M.DM = DM;
                DI_M.StartDialogue(TD.D);
                TD.gameObject.SetActive(false);
            }

            if (MMM != null)
            {
                DI_M.MMM = MMM;
                DI_M.StartDialogue(TD.D);
                TD.gameObject.SetActive(false);
            }
        }
        SettingUp = false;
        gameObject.SetActive(false);
    }



    //Sets up the swap from the 2D gameplay to the 3D gameplay
    public IEnumerator Setup3D()
    {
        //This functions starts with a fade before deactivating the 2D character and activating the 3D character.
        Fade.SetBool("Fade", true);
        yield return new WaitForSeconds(0.4f);
        MainPlayer.SetActive(true);
        Crack1.SetActive(true);
        Crack2.SetActive(true);
        Crack2Trigger.SetActive(true);
        Crack1Trigger.SetActive(true);
        TwoDCrack1.SetActive(true);
        MainPlayer.transform.position = PlayerPos;
        MainPlayer.transform.rotation = PlayerRot;

        //The function resets every aspects of the 2D players so there are no glitches when entering the 2D area again.
        if (DM != null)
        {
            DM.LetMove(false);
            DM.transform.parent = null;
            DM.gameObject.SetActive(false);
        }
        if(MMM != null)
        {
            MMM.LetMove(false);
            MMM.transform.parent = null;
            MMM.gameObject.SetActive(false);
        }

        if (SG != null)
        {
            SG.GravityChange("Regular");
        }

        if (Rock != null)
        {
            Rock.transform.parent = null;
            Rock.SetActive(false);
        }
        if (MMM != null)
        {
            pollenui.gameObject.SetActive(false);
        }

        if(FlatCamera != null)
        {
            FlatCamera.SetActive(false);
        }

        if(DM != null)
        {
            DM.LeftAble = true;
            DM.RightAble = true;
        }
        if(MMM != null)
        {
            MMM.LeftAble = true;
            MMM.RightAble = true;
        }
        yield return new WaitForSeconds(1f);
        //The function ends by reactivating all the UI and either keeps the 2D area active depending on the Projector.
        CR.Set2D = false;
        Cursor.SetActive(true);
        InventoryItem.SetActive(true);
        Fade.SetBool("Fade", false);
        if (VA != null)
        {
            VA.AddLine();
        }
        SettingUp = false;
        gameObject.SetActive(false);

        if (P == null)
        {
            DecalArea.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (P != null)
        {
            if (P.Keepon == false)
            {
                DecalArea.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(0.4f);

    }

    public void Start3D()
    {
        StartCoroutine(Setup2D());
    }

 
}

