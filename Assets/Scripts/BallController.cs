using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Ball end event
    public delegate void BallEnd(bool didit);
    public BallEnd OnBallEnd;

    // Camera follow rate
    float cameraFollowRate = 10.0f;

    void FixedUpdate()
    {
        // Set position of camera
        Camera.main.transform.position = 
            Vector3.Lerp(
                Camera.main.transform.position,
                new Vector3(
                    transform.position.x,
                    transform.position.y,
                    -10),
                cameraFollowRate*Time.fixedDeltaTime
            );
    }

    void OnHole()
    {
        Debug.Log("We did it!");
        OnBallEnd?.Invoke(true);
        Destroy(gameObject);
    }
}
