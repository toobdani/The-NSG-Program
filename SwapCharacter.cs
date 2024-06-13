using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    //This script is used in the final segment of the game to have the player swap between the 2D gameplay styles on the fly.

    //This array stores the different gameplay characters
    [SerializeField] GameObject[] Character;
    //This stores the position that the different player types will spawn at when swapping
    [SerializeField] Vector3[] Position;
    //This stores the normal of the different player types after swapping.
    [SerializeField] Vector3[] Normal;

    //When entering a trigger the script will swap the player character over to the opposite player
    private void OnTriggerEnter(Collider other)
    {
        //When the Atari Player enters the trigger, it will swap to the Mega Man player.
        if(other.gameObject == Character[0])
        {
            Character[0].SetActive(false);
            Character[1].SetActive(true);
            //When swapping the script automatically updates the positions the player character will be moving between, so they don't glitch.
            Character[1].GetComponent<Mega_Man_Movement>().OriginMark = Character[0].GetComponent<DecalMovement>().OriginMark;
            Character[1].GetComponent<Mega_Man_Movement>().TargetMark = Character[0].GetComponent<DecalMovement>().TargetMark;
            Character[1].transform.position = Position[1];
            Character[1].transform.forward = Normal[1];
        }
        else if(other.gameObject == Character[1])
        {        
            //When the Mega Man Player enters the trigger, they will swap to the Atari Player. 
            Character[1].SetActive(false);
            Character[0].SetActive(true);
            Character[0].GetComponent<DecalMovement>().OriginMark = Character[1].GetComponent<Mega_Man_Movement>().OriginMark;
            Character[0].GetComponent<DecalMovement>().TargetMark = Character[1].GetComponent<Mega_Man_Movement>().TargetMark;
            Character[0].transform.position = Position[0];
            Character[0].transform.forward = Normal[0];
        }
    }
}
