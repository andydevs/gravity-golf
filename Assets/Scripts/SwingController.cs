using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SwingController : MonoBehaviour
{
    public float maxDistance = 30.0f;

    private PlayerInput input;
    private InputAction mouseState;
    private Vector2 downEvent;
    private Vector2 mousePos;
    private Vector2 argument;
    private bool pressed;

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

    private void OnMouseButtonStarted(InputAction.CallbackContext context)
    {
        downEvent = GetMousePos();
        pressed = true;
    }

    private void OnMouseButtonCancelled(InputAction.CallbackContext context)
    {
        SendMessage("Swing", argument);
        pressed = false;
    }

    public void OnDrawGizmos()
    {
        if (pressed)
        {
            Gizmos.color = Color.HSVToRGB(argument.magnitude, 1, 1);
            Gizmos.DrawLine(downEvent, mousePos);
            Gizmos.DrawLine(transform.position, transform.position + 3 * (Vector3)argument);
        }
    }

    private Vector2 GetMousePos()
    {
        Vector2 screenSpaceMouse = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(screenSpaceMouse);
    }
}
