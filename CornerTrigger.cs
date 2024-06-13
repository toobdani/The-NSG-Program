using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTrigger : MonoBehaviour
{
    //This script works to setup the corner rotations for the 2D player

    //This stores Atari Player for the corner
    public DecalMovement DM;
    //This stores the Mega Man Player for the corner
    public Mega_Man_Movement MMM;
    //This sets the Zelda Player for the Corner
    public Zelda_Movement ZM;

    //This decides if the rotation is in the X or Y axis
    public bool XorY;

    //Stores the values used to calculate the corner rotation
    public Vector3[] NormalSave;
    public Vector3 SetNormal;
    public Vector3[] LocationSave;
    public Quaternion[] RotationSave;

    //This stores the different markers for the corner, and the speed at which the platform rotates.
    public GameObject[] Markers;
    public float NewSpeed;

    //These store the rotating values of the Camera if needed.
    public bool CameraB, DoBoolOnce;
    public Vector3[] CLocationSave;
    public Quaternion[] CRotationSave;
    public float[] CFOVSave;

    //This checks the how close the player needs to be before they can rotate.
    public float[] TurnCheck;

    //This stores the distance between theOrigin object and the Target
    public float OriginDis;
    public float TargetDis;

    //This decides if the camera rotation should be done via local or world transforms
    public bool LocalR;

    //This bool is true when the Atari and Mega Man players are playable in the same 2D space.
    [SerializeField] bool TwoObject;

    //This stores new rotation values for the 2D Mega Man player if that player is found within the same space as an Atari player.
    public Vector3[] SecondNormalSave;
    public Vector3[] SecondLocationSave;
    public Quaternion[] SecondRotationSave;

    public void SetupRotate()
    {
        //Checks which script it is setting the script for, and then calls that scripts SetUpRotate Function
        if(DM != null)
        {
            if(DM.gameObject.activeSelf == true)
            {
                DM.rotationspeed = NewSpeed;
                DM.SetUpRotate(LocationSave, NormalSave, RotationSave, Markers);

                if (CameraB == true)
                {
                    if (DoBoolOnce == true)
                    {
                        CameraB = false;
                    }
                    DM.SetUpCameraChange(CLocationSave, CRotationSave, CFOVSave, LocalR);
                }
            }
        }
        if(MMM != null)
        {
            if (MMM.gameObject.activeSelf == true)
            {
                MMM.rotationspeed = NewSpeed;
                if(TwoObject == false)
                {
                    MMM.SetUpRotate(LocationSave, NormalSave, RotationSave, Markers);
                }
                else
                {
                    MMM.SetUpRotate(SecondLocationSave, SecondNormalSave, SecondRotationSave, Markers);
                }

                if (CameraB == true)
                {
                    if (DoBoolOnce == true)
                    {
                        CameraB = false;
                    }
                    MMM.SetUpCameraChange(CLocationSave, CRotationSave, CFOVSave);
                }
            }
        }
        else if (ZM != null)
        {
            ZM.rotationspeed = NewSpeed;
            
            //Exclusive to the Zelda game play, the script checks whether the corner is rotating in the x or y axis.
            switch(XorY)
            {
                case true:
                    {
                        ZM.SetUpRotateX(LocationSave, NormalSave, RotationSave, Markers);
                    }
                    break;
                case false:
                    {
                        ZM.SetUpRotateY(LocationSave, NormalSave, RotationSave, Markers);
                    }
                    break;
            }
        }

    }
}
