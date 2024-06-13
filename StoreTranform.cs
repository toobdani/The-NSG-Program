using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TransformSave", order = 3, fileName = "Store Transform")]
public class StoreTranform : ScriptableObject
{
    //This is a scriptable object used to easily copy and paste the Tranforms of objects in the game. 
    public StoreClass[] Save;
}
