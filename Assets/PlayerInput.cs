//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""KeyboardMap"",
            ""id"": ""3690e6f0-0519-439c-befa-a7bd46f6d582"",
            ""actions"": [
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""dc6e816e-cc48-403a-9013-00865e2bf7b6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2895e643-26a6-40b7-bfb7-f0e9b871c3ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""9c8220ab-467f-4a57-acbe-dec77132e7bb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e58c1ea7-805f-4c16-bf16-b3aeaddd2b92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6ebd4906-fcb2-4110-9c7f-19378449ffa5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d37bec06-ccf4-4dce-b04f-ffe28dbeaffc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Value"",
                    ""id"": ""2426881a-9806-48d0-beb6-cfdba45e01bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""f6ba534f-56f1-4370-8183-515b683b33c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""73515985-de75-4549-ae4f-f939a08bf0f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0bd56d08-ca1f-43fb-bf57-0166344692ff"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2c92aaf3-1b1d-446b-902f-c98bd72473e3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f2f8fadb-7ffa-47bb-ad72-e5ec10887ac9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d7a4fadd-62bf-41db-80b7-f850ca1caacc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f612a940-9ed6-4982-90e2-8058d29533b2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e496f94d-325b-402e-9731-f6ccad7556ea"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecfd0e13-d680-4ee6-b0b8-cc0cddd44dee"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83052e9a-6d88-4708-9941-f2538fee1ca2"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3541e322-6820-43bf-816b-98c39351c937"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c62b1bc-d093-4855-9177-5b8c4e0fbbe8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12c319d9-1c53-41e8-bd20-c72035d95572"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b6f55d2-2f0b-418d-b738-dd46d8772616"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28f8e22f-cbe5-4cd7-97a5-325fafba26ca"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // KeyboardMap
        m_KeyboardMap = asset.FindActionMap("KeyboardMap", throwIfNotFound: true);
        m_KeyboardMap_Walk = m_KeyboardMap.FindAction("Walk", throwIfNotFound: true);
        m_KeyboardMap_Jump = m_KeyboardMap.FindAction("Jump", throwIfNotFound: true);
        m_KeyboardMap_Look = m_KeyboardMap.FindAction("Look", throwIfNotFound: true);
        m_KeyboardMap_Dash = m_KeyboardMap.FindAction("Dash", throwIfNotFound: true);
        m_KeyboardMap_Pause = m_KeyboardMap.FindAction("Pause", throwIfNotFound: true);
        m_KeyboardMap_Interact = m_KeyboardMap.FindAction("Interact", throwIfNotFound: true);
        m_KeyboardMap_Attack = m_KeyboardMap.FindAction("Attack", throwIfNotFound: true);
        m_KeyboardMap_Special = m_KeyboardMap.FindAction("Special", throwIfNotFound: true);
        m_KeyboardMap_Reload = m_KeyboardMap.FindAction("Reload", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // KeyboardMap
    private readonly InputActionMap m_KeyboardMap;
    private IKeyboardMapActions m_KeyboardMapActionsCallbackInterface;
    private readonly InputAction m_KeyboardMap_Walk;
    private readonly InputAction m_KeyboardMap_Jump;
    private readonly InputAction m_KeyboardMap_Look;
    private readonly InputAction m_KeyboardMap_Dash;
    private readonly InputAction m_KeyboardMap_Pause;
    private readonly InputAction m_KeyboardMap_Interact;
    private readonly InputAction m_KeyboardMap_Attack;
    private readonly InputAction m_KeyboardMap_Special;
    private readonly InputAction m_KeyboardMap_Reload;
    public struct KeyboardMapActions
    {
        private @PlayerInput m_Wrapper;
        public KeyboardMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Walk => m_Wrapper.m_KeyboardMap_Walk;
        public InputAction @Jump => m_Wrapper.m_KeyboardMap_Jump;
        public InputAction @Look => m_Wrapper.m_KeyboardMap_Look;
        public InputAction @Dash => m_Wrapper.m_KeyboardMap_Dash;
        public InputAction @Pause => m_Wrapper.m_KeyboardMap_Pause;
        public InputAction @Interact => m_Wrapper.m_KeyboardMap_Interact;
        public InputAction @Attack => m_Wrapper.m_KeyboardMap_Attack;
        public InputAction @Special => m_Wrapper.m_KeyboardMap_Special;
        public InputAction @Reload => m_Wrapper.m_KeyboardMap_Reload;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMapActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardMapActions instance)
        {
            if (m_Wrapper.m_KeyboardMapActionsCallbackInterface != null)
            {
                @Walk.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnWalk;
                @Jump.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnLook;
                @Dash.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnDash;
                @Pause.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnPause;
                @Interact.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnAttack;
                @Special.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnSpecial;
                @Reload.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_KeyboardMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public KeyboardMapActions @KeyboardMap => new KeyboardMapActions(this);
    public interface IKeyboardMapActions
    {
        void OnWalk(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
}
