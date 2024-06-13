using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDestroy : MonoBehaviour
{
    [SerializeField] Zelda_Movement Zm;

    //This script is attatched to objects which can be destroyed by the sword object, destroying them if the sword enters there collision
    private void OnCollisionEnter(Collision collision)
    {
        if(Zm.Sword == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Zm.Sword == true)
        {
            Destroy(gameObject);
        }
    }
}
