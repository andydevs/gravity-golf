using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    // Data
    public string startLevel = "Level 1";

    // UIs
    private GameObject menuUI;
    private GameObject creditUI;

    void Start()
    {
        menuUI = transform.Find("Main Menu").gameObject;
        creditUI = transform.Find("Credits").gameObject;

        menuUI.SetActive(true);
        creditUI.SetActive(false);
    }

    void OnStart()
    {
        SceneManager.LoadScene(startLevel);
    }

    void OnCredits()
    {
        menuUI.SetActive(false);
        creditUI.SetActive(true);
    }

    void OnMenu()
    {
        menuUI.SetActive(true);
        creditUI.SetActive(false);
    }

    void OnQuit()
    {
        Application.Quit(0);
    }
}
