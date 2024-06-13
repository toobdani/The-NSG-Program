using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSprings : MonoBehaviour
{
    //This script was the orignal way I aimed to swap springs based on the size of the 3D vines, before I realised I could just use the Vines script made for the inventory puzzle. 
    //Because of this, this script ended up being unused. 

    [SerializeField] GameObject[] Springs;
    [SerializeField] plantgrow PG;

    //The Update() function activates and deactivates springs based on the size of the vine. 
    private void Update()
    {
        switch(PG.Spring)
        {
            case false:
                {
                    Springs[0].SetActive(true);
                    Springs[1].SetActive(false);
                }
                break;
            case true:
                {
                    Springs[1].SetActive(true);
                    Springs[0].SetActive(false);
                }
                break;
        }
    }
}
