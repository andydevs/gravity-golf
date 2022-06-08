using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Helper variables
    int strokesThisGame;

    // Handles to child components
    StrokeController strokec;
    SwingController swingc;
    GameObject goffbol;

    // Start is called before the first frame update
    void Start()
    {
        strokec = GetComponentInChildren<StrokeController>();
        strokec.OnStroke += OnStroke;
        strokesThisGame = 0;
        swingc = GetComponentInChildren<SwingController>();
        goffbol = transform.Find("Goffbol").gameObject;
    }

    void OnStroke()
    {
        strokesThisGame++;
        Debug.Log(strokesThisGame.ToString() + " Strokes This Game");
    }

    void OnHole()
    {
        Debug.Log("We did it!");
        swingc.enabled = false;
        Destroy(goffbol);
    }
}
