using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    // Events
    public delegate void GameRestart();
    public static GameRestart OnGameRestart;

    // Set par
    public int par = 3;

    // Game UI
    private GameObject winUI;
    private GameObject gameUI;
    private GameObject strokeNumberUI;
    private GameObject parNumberUI;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        winUI = transform.Find("Win Screen").gameObject;
        gameUI = transform.Find("In Game Screen").gameObject;
        strokeNumberUI = gameUI.transform.Find("Stroke Number").gameObject;
        parNumberUI = gameUI.transform.Find("Par Number").gameObject;

        // Make sure initial state is set
        winUI.SetActive(false);
        gameUI.SetActive(true);

        // Subscribe to player end
        PlayerController.OnPlayerEnd += OnGameEnd;
        PlayerController.OnPlayerStrokeUpdate += OnPlayerStrokeUpdate;

        // Run restart
        OnSetPar();
        OnRestart();
    }

    void OnGameEnd(bool didit, int playerId, int strokes)
    {
        winUI.SetActive(true);
    }

    void OnPlayerStrokeUpdate(int playerId, int strokes)
    {
        strokeNumberUI.GetComponent<TextMeshProUGUI>().text = "Strokes: " + strokes.ToString();
    }

    void OnSetPar()
    {
        parNumberUI.GetComponent<TextMeshProUGUI>().text = "Par: " + par;
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
