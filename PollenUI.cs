using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenUI : MonoBehaviour
{
    //This script is used to program UI that shows what type of pollen the Mega Man Player is shooting
    //It is quite simple, activating the UI images based on the value of the Mega Man player's pollenbad bool.
    public Mega_Man_Movement MMM;

    [SerializeField] GameObject[] PollenImage;
    void Update()
    {
        PollenImage[0].SetActive(MMM.pollenbad);
        PollenImage[1].SetActive(!MMM.pollenbad);
    }
}
