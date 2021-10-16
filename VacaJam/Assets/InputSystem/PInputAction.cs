// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PInputAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""532f9c3d-95bc-47a7-b39b-8d93b2f3ab3c"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""32e84be0-ef35-40d2-a96d-29a477a7390c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0343bd85-9d81-4c8f-9501-0ca6862e766a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""JumpHold"",
                    ""type"": ""Button"",
                    ""id"": ""025802c6-1424-467f-9b15-634c91e3aac7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d809d25d-2b63-454f-8ba0-c3b920c98850"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse/Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MoveDir"",
                    ""id"": ""6bd6c5f0-0763-46e6-8ae2-d52ba00f5c97"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""94595bc3-165d-4007-b9f3-776812349012"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse/Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fad0f410-c30d-4b66-bbb8-d902e1cd7014"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse/Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4a31addb-2fff-4b6f-8305-6b84994d722c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse/Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d3353468-1100-4645-b4ed-24a8d4ebf941"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse/Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bd0fe9ac-02fa-4904-bfb2-e74a3e6bf312"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse/Keyboard"",
                    ""action"": ""JumpHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse/Keyboard"",
            ""bindingGroup"": ""Mouse/Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_JumpHold = m_Player.FindAction("JumpHold", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_JumpHold;
    public struct PlayerActions
    {
        private @PInputAction m_Wrapper;
        public PlayerActions(@PInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @JumpHold => m_Wrapper.m_Player_JumpHold;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @JumpHold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpHold;
                @JumpHold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpHold;
                @JumpHold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpHold;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @JumpHold.started += instance.OnJumpHold;
                @JumpHold.performed += instance.OnJumpHold;
                @JumpHold.canceled += instance.OnJumpHold;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse/Keyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnJumpHold(InputAction.CallbackContext context);
    }
}
