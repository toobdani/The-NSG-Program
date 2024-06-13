using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue_Manager : MonoBehaviour
{
    //This script is used to create Dialogue sequences within the game.

    //This script stores Queues of each Sentance and Name listed in the Dialogue sequence.
    private Queue<string> Sentance;
    private Queue<string> Names;
    public string TextTag;

    //These store the aspects of the text UI on screen, such as the text itself and the animator for the text for it to slide on screen.
    [SerializeField]TextMeshProUGUI Dialogue;
    public Animator Anim;
    //This stores whatever 2D player is active during the Dialogue sequence.
    public DecalMovement DM;
    public Mega_Man_Movement MMM;

    //This bool is set to true whilst the game is in a Dialogue sequence.
    public bool inD;

    //This stores a GameObject that has DecalText which fade onto the scene.
    [SerializeField] GameObject FadeMove;

    //This stores the audiomanager to create a satisfying sound when the player clicks between lines. 
    [SerializeField] audiomanager AM;

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AControl").GetComponent<audiomanager>();

        //The start function starts by setting up the Sentance and Names queue
        Sentance = new Queue<string>();
        Names = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() function works to call the DisplayNextSentance() function when the player left clicks
        if(inD == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            DisplayNextSentance();
        }
    }

    public void StartDialogue(Dialogue D)
    {
        //This function is called to set up the dialogue, setting the Names and Sentances into queues, which is used in later functions
        Sentance.Clear();
        Names.Clear();

        Anim.SetBool("Open", true);

        //The function freezes the movement of the 2D Player during the function
        if(DM != null)
        {
            DM.isMove = false;
            DM.CutsceneJump = true;
        }

        if(MMM != null)
        {
            MMM.isMove = false;
            MMM.CanShoot = false;
        }

        TextTag = D.tag;

        inD = true;

        //The function uses foreach loops to add to the Sentance and Names queues.
        foreach(string S in D.Sentances)
        {
            Sentance.Enqueue(S);
        }
        foreach(string N in D.Name)
        {
            Names.Enqueue(N);
        }

        //The function ends by typing the first sentance in the DisplayNextSentance() function
        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        AM.Dialogue.SetActive(true);
        AM.Dialogue.GetComponent<AudioSource>().Play();
        //This function works to begin typing the next line of dialogue
        //The function checks if there are any sentances left to type, and if not EndDialogue is called.
        if (Sentance.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            //The function stores the next line of dialogue and stops all Coroutines, before calling the Type Sentance Coroutine
            string sentance = Sentance.Dequeue();
            string name = Names.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentance(name, sentance));
        }
    }

    IEnumerator TypeSentance(string N, string S)
    {
        //This works to type the current sentance out char by char.
        Dialogue.text = N + ": ";

        foreach(char letter in S.ToCharArray())
        {
            Dialogue.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void EndDialogue()
    {
        //This function ends the dialogue scene, and will trigger events based on the instance's Tag.
        Anim.SetBool("Open", false);

        //This function completes different functions after the dialogue sequence is complete, before setting the player movement back to normal
        if(TextTag == "F")
        {
            DM.isMove = true;
            DM.CutsceneJump = false;
            DM = null;
            inD = false;
            StartCoroutine(FadeMove.GetComponent<DecalTextAppear>().Fadein());
            
        }
        if(TextTag == "S")
        {
            DM.isMove = true;
            DM.CutsceneJump = false;
            inD = false;
            DM = null;
        }
        if(TextTag == "M")
        {
            MMM.isMove = true;
            MMM.CanShoot = true;
            MMM = null;
        }
        TextTag = null;

        inD = false;
        return;
    }
}
