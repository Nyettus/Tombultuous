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
                },
                {
                    ""name"": ""Weapon Switch"",
                    ""type"": ""Value"",
                    ""id"": ""491fff6b-ba05-4ca2-8046-70d40b31919b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""QuickSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""d29a049d-ecf1-48fa-b6f5-e86e5c2c4c78"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""6b3cc2ac-8b5a-494c-b250-e155bdb44a79"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c433a126-371b-4457-86c9-d23522f3a991"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12f4ba14-6e91-4738-b6cd-bc1b53be1940"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad0329c5-d0a1-4b72-96cb-aa637799cd0d"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d6545ac-166d-47ff-ba05-d10f62a4ef30"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5119eb08-3653-43d6-bb72-f88b30ee4540"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42db1119-2721-476a-b2c9-a43a0260a78a"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""005766d7-dd0b-4362-94e4-0e2c420680c6"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33c235a0-4526-48ac-bd1b-adb50ec35243"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e444c46-53ca-4b1e-82e8-8f15ac91bb41"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=10)"",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c5c8278-08a9-4199-a22a-e9e99cd50bc5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickSwitch"",
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
        m_KeyboardMap_WeaponSwitch = m_KeyboardMap.FindAction("Weapon Switch", throwIfNotFound: true);
        m_KeyboardMap_QuickSwitch = m_KeyboardMap.FindAction("QuickSwitch", throwIfNotFound: true);
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
    private readonly InputAction m_KeyboardMap_WeaponSwitch;
    private readonly InputAction m_KeyboardMap_QuickSwitch;
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
        public InputAction @WeaponSwitch => m_Wrapper.m_KeyboardMap_WeaponSwitch;
        public InputAction @QuickSwitch => m_Wrapper.m_KeyboardMap_QuickSwitch;
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
                @WeaponSwitch.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnWeaponSwitch;
                @WeaponSwitch.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnWeaponSwitch;
                @WeaponSwitch.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnWeaponSwitch;
                @QuickSwitch.started -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnQuickSwitch;
                @QuickSwitch.performed -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnQuickSwitch;
                @QuickSwitch.canceled -= m_Wrapper.m_KeyboardMapActionsCallbackInterface.OnQuickSwitch;
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
                @WeaponSwitch.started += instance.OnWeaponSwitch;
                @WeaponSwitch.performed += instance.OnWeaponSwitch;
                @WeaponSwitch.canceled += instance.OnWeaponSwitch;
                @QuickSwitch.started += instance.OnQuickSwitch;
                @QuickSwitch.performed += instance.OnQuickSwitch;
                @QuickSwitch.canceled += instance.OnQuickSwitch;
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
        void OnWeaponSwitch(InputAction.CallbackContext context);
        void OnQuickSwitch(InputAction.CallbackContext context);
    }
}
