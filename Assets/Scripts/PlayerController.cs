using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // OnPlayer end delegate
    public delegate void PlayerEnd(bool didit, int playerId, int strokes);
    public static PlayerEnd OnPlayerEnd;

    // Player data per hole
    int playerId;
    int strokesThisGame;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        playerId = 0;
        strokesThisGame = 0;

        // Listen to OnStroke event from StrokeController
        GetComponentInChildren<StrokeController>().OnStroke += OnStroke;
    }

    void OnStroke()
    {
        strokesThisGame++;
        Debug.Log(strokesThisGame.ToString() + " Strokes This Game");
    }

    void OnHole()
    {
        Debug.Log("We did it!");
        OnPlayerEnd?.Invoke(true, playerId, strokesThisGame);
        Destroy(gameObject);
    }
}
