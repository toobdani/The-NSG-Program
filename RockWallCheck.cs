using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWallCheck : MonoBehaviour
{
    //This checks when the rock has been attached to a MovingPaltofrm and collides with a wall, so it unparents itself. 

    [SerializeField] GameObject Rock;

    [SerializeField] bool Left;
    [SerializeField] bool Right;

    [SerializeField] GameObject AttatchedMove;
    [SerializeField] GameObject NormalParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            if (Rock.transform.parent == AttatchedMove.transform)
            {
                Rock.transform.parent = NormalParent.transform;
            }
        }
    }
}
