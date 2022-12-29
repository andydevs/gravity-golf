using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    // Events
    public delegate void GameRestart();
    public static GameRestart OnGameRestart;
    public delegate void EnableControls();
    public static EnableControls OnEnableControls;

    // Set par
    public int par = 3;
    public string next = "Level 2";

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
        strokeNumberUI = gameUI.transform.Find("Data/Stroke Number").gameObject;
        parNumberUI = gameUI.transform.Find("Data/Par Number").gameObject;

        // Make sure initial state is set
        winUI.SetActive(false);
        gameUI.SetActive(true);

        // Subscribe to player end
        PlayerController.OnPlayerEnd += OnGameEnd;
        PlayerController.OnPlayerStrokeUpdate += OnPlayerStrokeUpdate;

        // Run restart
        OnSetPar();
        OnGameStart();
    }

    void OnDestroy()
    {
        PlayerController.OnPlayerEnd -= OnGameEnd;
        PlayerController.OnPlayerStrokeUpdate -= OnPlayerStrokeUpdate;
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

    void OnNext()
    {
        SceneManager.LoadScene(next);
    }

    void OnRestart()
    {
        winUI.SetActive(false);
        OnGameRestart?.Invoke();
    }

    void OnGameStart()
    {
        // Run level show animation routine
        StartCoroutine(LevelShowAnimation());
    }

    IEnumerator LevelShowAnimation()
    {
        // Params
        float speed = 5.0f;
        float tolerance = 0.001f;

        // Show flag and wait (routine)
        Transform hole = GameObject.FindGameObjectWithTag("Hole").transform;
        Camera.main.transform.position = new Vector3(
            hole.position.x,
            hole.position.y,
            -10
        );
        Camera.main.orthographicSize = 20;
        yield return new WaitForSeconds(2.0f);

        // Zoom out to level (routine) and wait (routine)
        Transform levelHook = GameObject.FindGameObjectWithTag("CameraHook").transform;
        while (
            Vector3.Distance(Camera.main.transform.position, levelHook.position) > tolerance
            && Mathf.Abs(Camera.main.orthographicSize - 80.0f) > tolerance
        )
        {
            Camera.main.transform.position = Vector3.Lerp(
                Camera.main.transform.position,
                new Vector3(levelHook.position.x, levelHook.position.y, -10),
                Time.fixedDeltaTime * speed);
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize, 80, 
                Time.fixedDeltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1.0f);

        // Spawn ball and wait (routine)
        OnGameRestart();
        while (Mathf.Abs(Camera.main.orthographicSize - 20.0f) > tolerance)
        {
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize, 20.0f,
                Time.fixedDeltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);

        // Enable control
        OnEnableControls?.Invoke();
    }

    void OnQuit()
    {
        Application.Quit();
    }
}
