using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField] GameObject DestroyGObject;
    //This script destroys a object when the player enters its trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (DestroyGObject != null)
            {
                Destroy(DestroyGObject);
                Destroy(gameObject);
            }
        }
    }
}
