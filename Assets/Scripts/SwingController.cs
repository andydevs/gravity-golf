using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwingController : MonoBehaviour
{
    // Event delegates
    public delegate void StrokeEvent();
    public static StrokeEvent OnStroke;

    // Public variables
    public float strokeInitialSpeed = 1.0f;
    public float strokeEndSpeed = 0.05f;

    // Components
    private Rigidbody2D rigidbody2D_;
    private Collider2D collider2D_;
    private PlayerInput playerInput_;

    // Helper variables
    private int planetMask;

    // Public properties
    public bool IsInStroke { get { return rigidbody2D_.simulated; } }

    // Start is called before the first frame update
    void Start()
    {
        playerInput_ = GetComponent<PlayerInput>();
        collider2D_ = GetComponent<Collider2D>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        rigidbody2D_.simulated = false;
        planetMask = LayerMask.GetMask("Planet");
    }

    // TODO: Update this control to click and drag
    public void OnMouseClick()
    {
        // Get mouse poisition
        Vector2 screenSpaceMouse = Mouse.current.position.ReadValue();
        Vector2 worldSpaceMouse = Camera.main.ScreenToWorldPoint(screenSpaceMouse);

        // Determine argument
        Vector2 argument = worldSpaceMouse - (Vector2)transform.position;
        argument.Normalize();

        // Start Swing Control
        StartCoroutine(SwingControl(argument));
    }

    IEnumerator SwingControl(Vector2 argument)
    {
        // Disable controls
        playerInput_.enabled = false;

        // Start simulation with given initial velocity
        rigidbody2D_.velocity = strokeInitialSpeed * argument;
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

        // Enable controls
        playerInput_.enabled = true;

        // Stroke event
        OnStroke();
    }
}
