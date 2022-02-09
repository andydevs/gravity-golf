using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StrokeController : MonoBehaviour
{
    // Event delegates
    public delegate void StrokeEvent();
    public static StrokeEvent OnStroke;

    // Public variables
    public float strokeEndSpeed = 0.05f;

    // Components
    private Rigidbody2D rigidbody2D_;
    private Collider2D collider2D_;

    // Helper variables
    private int planetMask;
    private bool canReceiveSwingCommands;

    // Public properties
    public bool IsInStroke { get { return rigidbody2D_.simulated; } }

    // Start is called before the first frame update
    void Start()
    {
        collider2D_  = GetComponent<Collider2D>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        planetMask   = LayerMask.GetMask("Planet");
        rigidbody2D_.simulated = false;
        canReceiveSwingCommands = true;
    }

    public void Swing(Vector2 argument)
    {
        if (canReceiveSwingCommands)
        {
            StartCoroutine(SwingControl(argument));
        }
    }

    IEnumerator SwingControl(Vector2 strokeSpeed)
    {
        // Disable swing command
        canReceiveSwingCommands = false;

        // Start simulation with given initial velocity
        rigidbody2D_.velocity = strokeSpeed;
        rigidbody2D_.simulated = true;

        // Wait for ending stroke
        bool endStroke = false;
        while (!endStroke)
        {
            // Wait for next frame
            yield return null;

            // Update end stroke
            endStroke = collider2D_.IsTouchingLayers(planetMask)         // Touching planet
                    && rigidbody2D_.velocity.magnitude < strokeEndSpeed; // Slow Enough
        }

        // End simulation
        rigidbody2D_.simulated = false;

        // Enable swing command
        canReceiveSwingCommands = true;

        // Stroke event
        OnStroke();
    }
}
