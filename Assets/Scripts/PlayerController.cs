﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Events
    public delegate void PlayerEnd(bool didit, int playerId, int strokes);
    public static PlayerEnd OnPlayerEnd;
    public delegate void PlayerStrokeUpdate(int playerId, int strokes);
    public static PlayerStrokeUpdate OnPlayerStrokeUpdate;

    // Game objects
    public GameObject golfBallPrefab;
    private GameObject golfBall;
    private Transform tee;
    private GameObject planets;

    // Player data per hole
    int playerId;
    int strokesThisGame;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        playerId = 0;
        strokesThisGame = 0;

        // Get tee
        tee = GameObject.Find("Tee").transform;
        planets = GameObject.Find("Planets");

        // Hook into events
        UIController.OnGameRestart += OnSpawn;
    }

    public GameObject GolfBall
    {
        get { return golfBall; }
    }

    void OnSpawn()
    {
        if (!golfBall)
        {
            golfBall = Instantiate(golfBallPrefab, tee);
            golfBall.GetComponent<StrokeController>().OnStroke += OnStroke;
            golfBall.GetComponent<BallController>().OnBallEnd += OnBallEnd;
            foreach (Gravitator grav in planets.GetComponentsInChildren<Gravitator>())
            {
                grav.SetGolfbol(golfBall.GetComponent<Rigidbody2D>());
            }
        }
    }

    void OnStroke()
    {
        strokesThisGame++;
        OnPlayerStrokeUpdate?.Invoke(playerId, strokesThisGame);
    }

    void OnBallEnd(bool didit)
    {
        OnPlayerEnd?.Invoke(didit, playerId, strokesThisGame);
    }
}
