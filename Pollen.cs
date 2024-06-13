using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Pollen : MonoBehaviour
{
    //This script is used for the 2D pollen bullets which are shot by the Mega Man Player. 

    //This stores the positions that the bullet will move between
    public GameObject OriginMark, TargetMark;

    //This stores the position the bullet needs to move towards. 
    public Vector3 originPos => OriginMark.transform.position;
    public Vector3 targetPos => TargetMark.transform.position;

    //This checks if it is moving right
    public bool MoveRight;

    //This checks the speed of the object
    public float Speed;

    //This checks if the bullet is moving up.
    public bool MoveUp;

    void FixedUpdate()
    {
        //This script is attatched to the pollen bullet and allows them to constatly move across the walls, before deleting themselves
        Vector3 UpdateTarget;
        Vector3 UpdateOrigin;

        //The values of UpdateTarget and UpdateOrigin depened on whether the bullet is moving up or sideways.
        if (MoveUp == false)
        {
            UpdateTarget = new Vector3(targetPos.x, gameObject.transform.position.y, targetPos.z);
            UpdateOrigin = new Vector3(originPos.x, gameObject.transform.position.y, originPos.z);
        }
        else
        {
            UpdateTarget = new Vector3(targetPos.x, targetPos.y, gameObject.transform.position.z);
            UpdateOrigin = new Vector3(originPos.x, originPos.y, gameObject.transform.position.z);
        }

        //The object moves the same way as the Decal Player
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, MoveRight ? UpdateTarget : UpdateOrigin, Speed * Time.deltaTime);

        //When reaching the end of its movement, the bullet destroys itself
        if(gameObject.transform.position == UpdateTarget || gameObject.transform.position == UpdateOrigin)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When colliding with a wall or floor the pollen destroys itself.
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
