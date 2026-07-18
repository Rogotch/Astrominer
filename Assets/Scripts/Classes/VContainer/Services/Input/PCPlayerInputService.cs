using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayerInputService : IPlayerInputService
{
    public event Action<Vector2Int> OnMove;
    public event Action<Vector2> OnCameraMove;
    public event Action OnPaused;
    private PlayerInputActions controls;
    
    public PCPlayerInputService()
    {
        controls = new PlayerInputActions();
        controls.Player.Movement.performed += ReadMoveInput;
    }
    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        if (!context.performed)return;
        
        Vector2 raw_input = context.ReadValue<Vector2>();
        Vector2Int direction = new Vector2Int((int)raw_input.x, (int)raw_input.y);

        if (direction.magnitude > 1) direction.y = 0;
        OnMove.Invoke(direction);
    }
}
