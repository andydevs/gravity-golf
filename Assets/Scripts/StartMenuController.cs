using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public string startLevel = "Level 1";

    void OnStart()
    {
        SceneManager.LoadScene(startLevel);
    }

    void OnQuit()
    {
        Application.Quit(0);
    }
}
