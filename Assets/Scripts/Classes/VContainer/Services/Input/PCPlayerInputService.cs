using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayerInputService : IPlayerInputService, IDisposable
{
    public event Action<Vector2Int> OnMove;
    public event Action<Vector2Int> OnMovePressed;
    public event Action             OnMoveReleased;
    public event Action<Vector2>    OnCameraMove;
    public event Action             OnPaused;
    private PlayerInputActions controls;
    
    public PCPlayerInputService()
    {

        controls = new PlayerInputActions();
        controls.Player.Movement.performed += MoveInputPressed;
        controls.Player.Movement.canceled  += MoveInputCanceled;
        controls.Enable();
    }
    public void Dispose()
    {
        controls.Player.Movement.performed -= MoveInputPressed;
        controls.Player.Movement.canceled  -= MoveInputCanceled;
        controls.Dispose();
    }
    public void MoveInputPressed(InputAction.CallbackContext context)
    {
        Vector2Int direction = GetMoveInput(context);
        // OnMove?.Invoke(direction);
        OnMovePressed?.Invoke(direction);
    }
    public void MoveInputCanceled(InputAction.CallbackContext context)
    {
        OnMoveReleased?.Invoke();
    }

    private Vector2Int GetMoveInput(InputAction.CallbackContext context)
    {
        Vector2 raw_input = context.ReadValue<Vector2>();
        Vector2Int direction = new Vector2Int((int)raw_input.x, (int)raw_input.y);
        if (direction.magnitude > 1) direction.y = 0;
        return direction;
    }
}
