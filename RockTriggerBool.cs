using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RockTriggerBool : MonoBehaviour
{
    //This script is used to make the player die after leaving the boulder's confines. 

    //This bool is used to affect the value of the gate animators bool
    public bool Set;
    //This stores the death trigger for the boulder
    [SerializeField] DecalDeath DD;

    //This bool checks when the player is in the collision of the boulder
    [SerializeField] bool InCollide;

    //This stores the animator for the Gate
    [SerializeField] Animator Gate;
    //This bool, when true, means that the trigger instance is used to open the gate.
    [SerializeField] bool SetGate;

    //This stores the Atari Player
    [SerializeField] GameObject Player;

    private void Update()
    {
        //The Update() function is used to open the gate, if the instance is the right trigger for it.
        if(Player != null)
        {
            if(Player.activeSelf == true)
            {
                if (Gate != null && SetGate == true)
                {
                    Gate.SetBool("Button", !(Set));
                }
            }
        }
    }

    //When the player enters the trigger, the script registers it has now collided.
    private void OnTriggerEnter(Collider other)
    {
           if (other.gameObject.tag == "Player")
           {
               InCollide = true;
           }
    }

    //When staying the trigger the script registers that the player has collided with the boulder.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InCollide = true;
        }
    }

    //If the player exits the trigger of the boulder then they get killed.
    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "Player")
         {
               if(InCollide == true)
               {
                   DD.Die();
                   InCollide = false;
               }
         }
    }
}
