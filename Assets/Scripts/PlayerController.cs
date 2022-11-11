using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // OnPlayer end delegate
    public delegate void PlayerEnd(bool didit, int playerId, int strokes);
    public static PlayerEnd OnPlayerEnd;

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
            foreach (Gravitator grav in planets.GetComponentsInChildren<Gravitator>())
            {
                grav.SetGolfbol(golfBall.GetComponent<Rigidbody2D>());
            }
        }
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
