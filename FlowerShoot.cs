using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlowerShoot : MonoBehaviour
{
    //This script is used to shoot pollen out of the flower gun, and is practically identical in code to the RockThrow script. 
    //The only difference is that the script shoots out a pollen bullet, and that's literally it. 

    [SerializeField] CameraRaycast CR;

    [SerializeField] AtariAblilitySave AAS;

    [SerializeField] Transform SpawnPoint;

    [SerializeField] float throwforwardforce;

    [SerializeField] GameObject PollenIntance;

    [SerializeField] float cooldownmax;
    [SerializeField] float currentcooldown;
    [SerializeField] bool shot;

    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] GameObject CanUseSymbol;

    [SerializeField] audiomanager AM;

    public bool CanShoot;

    [SerializeField] VoiceActing FirstShoot;
    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AAS.FlowerGun == true && CanShoot == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && shot == false)
            {
                ShootPollen();
            }
        }
    }
    private void FixedUpdate()
    {
        CooldownUI();
        if (AAS.tDRock == true && CanShoot == true)
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

    public void ShootPollen()
    {
        AM.RockThrow.SetActive(true);

        GameObject pollen = Instantiate(PollenIntance, SpawnPoint.transform.position, CR.GameCamera.transform.rotation);

        pollen.transform.localScale = pollen.transform.localScale;

        Vector3 ThrowForce = SpawnPoint.transform.forward * throwforwardforce + SpawnPoint.transform.up;

        pollen.GetComponent<Rigidbody>().AddForce(ThrowForce, ForceMode.Impulse);

        shot = true;

        if (FirstShoot != null)
        {
            FirstShoot.AddLine();
        }
    }

    void CooldownUI()
    {
        CanUseSymbol.SetActive(!shot);
        Timer.text = "" + Mathf.Round(currentcooldown * 10f) * 0.1f;
    }
}

