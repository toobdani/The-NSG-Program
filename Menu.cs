using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //This script is used for the buttons in the Main Menu

    //This stores the Buttons of the Main Menu
    [SerializeField] GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        Button.SetActive(false);
    }

    public void begingame()
    {
        SceneManager.LoadScene(1);
    }

    public void endgame()
    {
        Application.Quit();
    }
}
