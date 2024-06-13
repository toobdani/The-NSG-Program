using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RockThrow : MonoBehaviour
{
    //This script is used to throw out 3D rocks.

    //This stores the CameraRaycast script
    [SerializeField] CameraRaycast CR;

    //These store the force at which the rock is thrown forward and up.
    [SerializeField] float throwforwardforce;
    [SerializeField] float throwupforce;

    //This stores the Rock prefab that spawns the rock and the current rock spawned.
    [SerializeField] GameObject RockIntance;
    [SerializeField] GameObject CurrentRock;

    //These are used to create a cooldown for the rock after the player throws the rock.
    [SerializeField] float cooldownmax;
    [SerializeField] float currentcooldown;
    [SerializeField] bool shot;

    //This displays the UI Timer that shows the cooldown
    [SerializeField] TextMeshProUGUI Timer;
    //This displays the icon that appears when the player can throw the rock
    [SerializeField] GameObject CanUseSymbol;

    //This stores the AtariAbilitySave scriptable object.
    [SerializeField] AtariAblilitySave AAS;

    //This stores the audiomanager script
    [SerializeField] audiomanager AM;

    //These are audio clips that get played when the player uses the ability.
    [SerializeField] VoiceActing KnockThingsOver;
    [SerializeField] VoiceActing Oww;

    //This tells the script if the player can throw the rock.
    public bool CanRock;

    //This sets up the Audio Manager at the Start
    private void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
    }
    // Update is called once per frame

    private void Update()
    {
        //The Update() function checks whether the player is able to shoot rocks. 
        if (AAS.tDRock == true && CanRock == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && shot == false)
            {
                Throw();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && shot == true && Oww != null)
            {
                //If the player spams the left click then the Oww click plays.
                Oww.AddLine();
            }
        }
    }
    void FixedUpdate()
    {
        //The Timer is called in the FixedUpdate() so its consistent.
        CooldownUI();
        if (AAS.tDRock == true && CanRock == true)
        {
            if (shot == true)
            {
                currentcooldown += 0.1f;
                if (currentcooldown >= cooldownmax)
                {
                    shot = false;
                    currentcooldown = 0;
                }
            }
        }
    }

    //The throw function deletes the current rock in the scene, before spawning a new one in, which is from the StartMarker stored in the Camera Raycast.
    void Throw()
    {
        AM.RockThrow.SetActive(true);
        if(KnockThingsOver != null)
        {
            KnockThingsOver.AddLine();
        }
        if (CurrentRock != null)
        {
            Destroy(CurrentRock);
        }
        GameObject Rock = Instantiate(RockIntance, CR.StartMarker.transform.position, CR.GameCamera.transform.rotation);

        Rock.transform.localScale = RockIntance.transform.localScale;

        Vector3 ThrowForce = CR.GameCamera.transform.forward * throwforwardforce + transform.up * throwupforce;

        Rock.GetComponent<Rigidbody>().AddForce(ThrowForce, ForceMode.Impulse);

        CurrentRock = Rock;

        shot = true;
    }

    void CooldownUI()
    {
        //This function is used to affect the Cooldown UI.
        CanUseSymbol.SetActive(!shot);
        Timer.text = "" + Mathf.Round(currentcooldown * 10f) * 0.1f;
    }
}
