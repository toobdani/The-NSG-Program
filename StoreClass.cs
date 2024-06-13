using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//This is a class used to store the values needed for corner rotations in the 2D space.
public class StoreClass
{
    public string Name;
    public Vector3 Pos;
    public Quaternion Rot;
    public Vector3 Scale;
    public Vector3 Norm;
    public float FOV;
}
