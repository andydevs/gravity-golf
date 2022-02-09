using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SwingController : MonoBehaviour
{
    // Public gameobject
    public GameObject golfball;

    // Public parameters
    public float minSpeed = 20.0f;
    public float maxSpeed = 40.0f;
    public float maxDistance = 30.0f;

    // Private variables
    private PlayerInput input;
    private InputAction mouseState;
    private Vector2 downEvent;
    private Vector2 mousePos;
    private Vector2 argument;
    private bool pressed;

    /**
     * True if ball is in swing control 
     * (meaning someone is engaging the swing)
     */
    public bool InSwingControl 
    { 
        get { return pressed; } 
    }

    /**
     * The current swing speed vector
     */
    public Vector2 SwingSpeed
    {
        get { return Mathf.Lerp(minSpeed, maxSpeed, argument.magnitude) * argument.normalized; }
    }

    public void Start()
    {
        input = GetComponent<PlayerInput>();
        downEvent = Vector2.zero;
        pressed = false;
        mouseState = input.actions.FindAction("MouseButton", true);
        mouseState.started += OnMouseButtonStarted;
        mouseState.canceled += OnMouseButtonCancelled;
    }

    private void Update()
    {
        if (pressed)
        {
            mousePos = GetMousePos();
            argument = downEvent - GetMousePos();
            argument = Mathf.Clamp(argument.magnitude, 0, maxDistance)/maxDistance * argument.normalized;
        }
    }

    private void OnDrawGizmos()
    {
        if (pressed)
        {
            Gizmos.color = Color.HSVToRGB(argument.magnitude, 1, 1);
            Gizmos.DrawLine(downEvent, mousePos);
        }
    }

    private void OnMouseButtonStarted(InputAction.CallbackContext context)
    {
        pressed = true;
        downEvent = GetMousePos();
    }

    private void OnMouseButtonCancelled(InputAction.CallbackContext context)
    {
        golfball.SendMessage("Swing", SwingSpeed);
        pressed = false;
    }

    private Vector2 GetMousePos()
    {
        Vector2 screenSpaceMouse = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(screenSpaceMouse);
    }
}
