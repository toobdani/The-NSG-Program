using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalDeath : MonoBehaviour
{
    [SerializeField]GenreSwap GS;

    //When entering the trigger the player will be kicked back to the 3D section of gameplay.
    //This script can be called by other scripts to quickly leave the 2D space.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(GS.Setup3D());
    }
}
