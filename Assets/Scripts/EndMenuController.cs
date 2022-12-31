using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public string mainMenu = "Main Menu";

    void OnMain()
    {
        SceneManager.LoadScene(mainMenu);
    }

    void OnQuit()
    {
        Application.Quit(0);
    }
}
