using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    //This script is used to attatch a dialogue variable to a gameobject
    public Dialogue D;
    [SerializeField] Dialogue_Manager DI_M;
    [SerializeField] TimelineManager TM;

    public void CallD()
    {
        if(DI_M != null)
        {
            DI_M.StartDialogue(D);
            TM.pause = true;
            TM.Dialogue = true;

        }    
    }
}
