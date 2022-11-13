using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public delegate void BallEnd(bool didit);
    public BallEnd OnBallEnd;

    void OnHole()
    {
        Debug.Log("We did it!");
        OnBallEnd?.Invoke(true);
        Destroy(gameObject);
    }
}
