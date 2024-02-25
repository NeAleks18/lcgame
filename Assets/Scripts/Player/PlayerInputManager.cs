using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerInputManager", menuName = "Game/PlayerInputReader")]
public class PlayerInputManager : ScriptableObject, PlayerControlInput.IGameplayActions
{
    // Assign delegate{} to events to initialise them with an empty delegate
    // so we can skip the null check when we use them

    //GamePlay Events
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction jumpCanceledEvent = delegate { };
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<Vector2> cameraMoveEvent = delegate { };
    public event UnityAction cancelEvent = delegate { };
    
    private PlayerControlInput m_Controls;
    private bool jumpState;
 
    private void OnEnable()
    {
        if (m_Controls == null)
        {
            m_Controls = new PlayerControlInput();
            m_Controls.Gameplay.SetCallbacks(this);
        }
        
        EnableGameplayInput();
    }
    
    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveEvent.Invoke(ctx.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();
        if (ctx.phase == InputActionPhase.Canceled)
            jumpCanceledEvent.Invoke();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        cameraMoveEvent.Invoke(ctx.ReadValue<Vector2>());
    }

    public void OnCancel(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
            cancelEvent.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Not implemented yet");
    }
    
    public void EnableGameplayInput()
    {
        m_Controls.Gameplay.Enable();
    }
    
    public void DisableAllInput()
    {
        m_Controls.Gameplay.Disable();
    }
    
    public bool LeftMouseDown() => Mouse.current.leftButton.isPressed;
    
    //Temporary method of accessing Escape Button without writing more methods/code
    //Let's see how much time live this method
    public bool EscapeButtonDown() => Keyboard.current.escapeKey.isPressed;
    
    public bool OnJumpButtonState()
    {
        if (m_Controls.Gameplay.Jump.WasReleasedThisFrame())
            jumpState = false;
        if (m_Controls.Gameplay.Jump.WasPressedThisFrame())
            jumpState = true;
        
        return jumpState;

    }
}
