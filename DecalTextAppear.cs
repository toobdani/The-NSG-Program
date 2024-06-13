using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalTextAppear : MonoBehaviour
{
    [SerializeField] DecalProjector DP;

    public float FadeCount;
    public bool Triggered;

    public bool SetOn;

    // Start is called before the first frame update
    void Start()
    {
        DP.fadeFactor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FadeCount = DP.fadeFactor;

        if(SetOn == true)
        {
            SetOn = false;
            StartCoroutine(Fadein());
        }
    }
    //Causes a decal to fade in when entering the trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Pollen" || other.gameObject.tag == "EvilPollen")
        {
            if(Triggered == false)
            {
                StartCoroutine(Fadein());
            }
        }
    }

    //This IEnumerator is called to fade the Decal Projection on screen.
    public IEnumerator Fadein()
    {
        Triggered = true;
        //This will keep looping until the projection is fully faded onto the scene.
        if(DP.fadeFactor <= 1)
        {
            DP.fadeFactor += 0.01f;
        }
        yield return new WaitForSeconds(0.01f);

        if(DP.fadeFactor >= 1)
        {
            DP.fadeFactor = 1f;
        }
        else
        {
            StartCoroutine(Fadein());
        }
    }
}
