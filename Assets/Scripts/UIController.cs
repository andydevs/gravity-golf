using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Events
    public delegate void GameRestart();
    public static GameRestart OnGameRestart;

    // Game UI
    private GameObject winUI;
    private GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        winUI = transform.Find("Win Screen").gameObject;
        gameUI = transform.Find("In Game Screen").gameObject;

        // Make sure initial state is set
        winUI.SetActive(false);
        gameUI.SetActive(true);

        // Subscribe to player end
        PlayerController.OnPlayerEnd += OnGameEnd;

        // Run restart
        OnRestart();
    }

    void OnGameEnd(bool didit, int playerId, int strokes)
    {
        winUI.SetActive(true);
    }

    void OnRestart()
    {
        winUI.SetActive(false);
        OnGameRestart?.Invoke();
    }

    void OnQuit()
    {
        Application.Quit();
    }
}
