using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //This class stores the three variables used for dialogue: Name, which stores the name of who's speaking; Sentances which stores the dialogue said; Tag, which stores the tag used to trigger events at the end of the dialogue
    public string[] Name;

    public string[] Sentances;

    public string tag;
}
