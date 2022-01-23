// GENERATED AUTOMATICALLY FROM 'Assets/Misc/Golf Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GolfControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GolfControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Golf Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""84deae52-f263-4c4b-8054-3cc7154b6d77"",
            ""actions"": [
                {
                    ""name"": ""MouseButton"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9d84e492-604a-4be7-b913-e9f34e326cbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e2217fb6-8f28-4d1f-9256-dd131542148c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""MouseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse&Keyboard"",
            ""bindingGroup"": ""Mouse&Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MouseButton = m_Player.FindAction("MouseButton", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MouseButton;
    public struct PlayerActions
    {
        private @GolfControls m_Wrapper;
        public PlayerActions(@GolfControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseButton => m_Wrapper.m_Player_MouseButton;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MouseButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseButton;
                @MouseButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseButton;
                @MouseButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseButton;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseButton.started += instance.OnMouseButton;
                @MouseButton.performed += instance.OnMouseButton;
                @MouseButton.canceled += instance.OnMouseButton;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse&Keyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMouseButton(InputAction.CallbackContext context);
    }
}
