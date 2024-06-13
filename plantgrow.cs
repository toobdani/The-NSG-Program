using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class plantgrow : MonoBehaviour
{
    //This script is used for 2D vines that can be grown by the Mega Man Player's 2D pollen bullets.
    //This also has an alternative for the vines that block the path that can be shrunk by EvilPollen

    //This stores the decal material for the grown vine. 
    [SerializeField] Material Plants;
    //These Vector3s store the size and center of the vine's box collider after it is shot. 
    [SerializeField] Vector3 BoxSize;
    [SerializeField] Vector3 BoxCenter;

    //This stores the DecalProjector used for the 2D vine.
    [SerializeField] DecalProjector DP;
    //This stores the 2D vine's box collider.
    [SerializeField] BoxCollider BC;
    //These Vector3s store the position and scale that the 2D vine should swap to. 
    [SerializeField] Vector3 Scale;
    [SerializeField] Vector3 Pos;
    //This bool when ticked, means that the vine should be shrunk by the player's evil pollen.
    [SerializeField] bool Spikes;

    //This bool is set true after the regular vine is grown.
    public bool Spring;
    private void OnCollisionEnter(Collision collision)
    {
        //The collision checks to see if the vine needs to be grown or shrunk, and does this by checking the value of Spikes.
        if (collision.gameObject.tag == "Pollen")
        {
            Destroy(collision.gameObject);

            if(Spikes == false)
            {
                DP.material = Plants;
                BC.center = BoxCenter;
                BC.size = BoxSize;

                gameObject.transform.localPosition = Pos;
                gameObject.transform.localScale = Scale;
                Spring = true;
            }

        }
        else if(collision.gameObject.tag == "EvilPollen")
        {
            Destroy(collision.gameObject);

            if(Spikes == true)
            {
                DP.material = Plants;
                BC.center = BoxCenter;
                BC.size = BoxSize;

                gameObject.transform.localPosition = Pos;
                gameObject.transform.localScale = Scale;
            }
        }
    }
}
