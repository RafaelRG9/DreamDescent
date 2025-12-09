using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "DoomDescent/InputReader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions
{
    private Controls _controls;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction JumpEvent;
    public event UnityAction DashEvent;
    public event UnityAction AttackEvent;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();

            _controls.Gameplay.SetCallbacks(this);
        }

        _controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controls.Gameplay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DashEvent?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            AttackEvent?.Invoke();
        }
    }
}