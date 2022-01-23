using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SwingController : MonoBehaviour
{
    private PlayerInput input;
    private InputAction mouseState;
    private Vector2 downEvent;
    private Vector2 mousePos;
    private Vector2 upDeltaEvent;
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
        }
    }

    private void OnMouseButtonStarted(InputAction.CallbackContext context)
    {
        pressed = true;
        downEvent = GetMousePos();
    }

    private void OnMouseButtonCancelled(InputAction.CallbackContext context)
    {
        pressed = false;
        upDeltaEvent = downEvent - GetMousePos();
        upDeltaEvent.Normalize();
        SendMessage("Swing", upDeltaEvent);
    }

    public void OnDrawGizmos()
    {
        if (pressed)
        {
            Gizmos.DrawLine(downEvent, mousePos);
        }
    }

    private Vector2 GetMousePos()
    {
        Vector2 screenSpaceMouse = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(screenSpaceMouse);
    }
}
