using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwingController : MonoBehaviour
{
    public float strokeSpeed = 1.0f;

    private Rigidbody2D rigidbody2D_;

    public bool IsInStroke { get { return rigidbody2D_.simulated; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        rigidbody2D_.simulated = false;
    }

    void Update()
    {
        if (rigidbody2D_.simulated)
        {
            Debug.Log("Speed: " + rigidbody2D_.velocity.magnitude);
        }
    }

    public void OnSwing()
    {
        // Get mouse poisition
        Vector2 screenSpaceMouse = Mouse.current.position.ReadValue();
        Vector2 worldSpaceMouse = Camera.main.ScreenToWorldPoint(screenSpaceMouse);
        Debug.Log(worldSpaceMouse);

        // Determine argument
        Vector2 argument = worldSpaceMouse - (Vector2)transform.position;
        argument.Normalize();

        // Simulate physics
        if (!rigidbody2D_.simulated)
        {
            rigidbody2D_.velocity = strokeSpeed * argument;
            rigidbody2D_.simulated = true;
        }
    }
}
