using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StrokeController : MonoBehaviour
{
    // Events
    public delegate void StrokeEvent();
    public StrokeEvent OnStroke;

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
        planetMask = LayerMask.GetMask("Planet");
        rigidbody2D_.simulated = false;
        canReceiveSwingCommands = true;
    }

    public void Swing(Vector2 argument)
    {
        if (canReceiveSwingCommands)
        {
            StartCoroutine(StrokeControl(argument));
        }
    }

    IEnumerator StrokeControl(Vector2 strokeSpeed)
    {
        // Begin Stroke
        Debug.Log("Stroke began!");          // Log beginning of stroke
        canReceiveSwingCommands = false;     // Disable swing command
        rigidbody2D_.velocity = strokeSpeed; // Start simulation with given initial velocity
        rigidbody2D_.simulated = true;

        // Wait for stroke end condition
        yield return new WaitUntil(() =>
               collider2D_.IsTouchingLayers(planetMask)           // Touching planet
            && rigidbody2D_.velocity.magnitude < strokeEndSpeed); // Slow enough

        // End Stroke
        rigidbody2D_.simulated = false; // End simulation
        canReceiveSwingCommands = true; // Enable swing command
        Debug.Log("Stroke ended!");     // Log End of Stroke
        OnStroke?.Invoke();             // Have a stroke
    }
}
