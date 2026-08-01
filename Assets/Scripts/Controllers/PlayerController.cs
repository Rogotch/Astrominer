using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

public class PlayerController : BaseCharacterController
{
    #region Injections
    [Inject] private IPlayerInputService   input;
    [Inject] private Func<IResourcePicker> pickerFactory;
    #endregion

    #region Private variables
    private IResourcePicker resourcePicker;
    #endregion
    public Vector2Int delayed_command;
    public Vector2Int currentMoveVector;

    public override void StartConfiguration()
    {
        base.StartConfiguration();
        EquipDigTool(IDigInstrument.ToolType.DRILL);
        ChangeState(CharacterState.STATES.IDLE);
        resourcePicker           = pickerFactory.Invoke();
        input.OnMovePressed     += MoveInputPressed;
        input.OnMoveReleased    += MoveInputReleased;
        // input.OnMove            += MoveInput;
        moveService.MovingEnded += CheckSteppedCell;
    }
    public override void DisposeConfiguration()
    {
        // input.OnMove            -= MoveInput;
        input.OnMovePressed     -= MoveInputPressed;
        input.OnMoveReleased    -= MoveInputReleased;
        moveService.MovingEnded -= CheckSteppedCell;
    }

    public void EquipDigTool(IDigInstrument.ToolType toolType)
    {
        Equipment.EquipTool(DigToolFactory.Create(toolType, animationService));
    }

    // private void ConnectInput(PlayerInput new_player_input)
    // {
    //     player_input = new_player_input;
    // }
    
    public void MoveInputPressed(Vector2Int moveVector)
    {
        if (moveVector.magnitude > 1) moveVector.y = 0;
        currentMoveVector = moveVector;
        if (currentState is not PlayerIdle) delayed_command = moveVector;
        (currentState as PlayerIdle)?.ReciveInputDirection(moveVector);
    }
    public void MoveInputReleased()
    {
        currentMoveVector = Vector2Int.zero;
    }
    public void MoveInput(Vector2Int move_vector)
    {
        if (move_vector.magnitude > 1) move_vector.y = 0;
        if (currentState is not PlayerIdle) delayed_command = move_vector;
        (currentState as PlayerIdle)?.ReciveInputDirection(move_vector);
    }

    public override void ChangeState(CharacterState.STATES state)
    {
        Func<PlayerController> getPlayer = () => this;
        CharacterState new_state = state switch
        {
            CharacterState.STATES.START_TURN => new CharacterTurnStarted(getPlayer),
            CharacterState.STATES.IDLE       => new PlayerIdle          (getPlayer),
            CharacterState.STATES.MOVE       => new CharacterMove       (getPlayer),
            CharacterState.STATES.MINE       => new CharacterMine       (getPlayer),
            CharacterState.STATES.END_TURN   => new CharacterTurnEnded  (getPlayer),
            _ => null,
        };
        ChangeStateTo(new_state);
    }
    public override void CheckSteppedCell(Vector2Int cell_from, Vector2Int cell_to)
    {
        if (CellsSystem.IsHasResource(cell_to))
        {
            PickupResource(CellsSystem.ResourcesCells[cell_to], cell_to);
        }
    }

    public void PickupResource(Item item, Vector2Int fromCell)
    {
        resourcePicker.ResourcePickup(item);
        CellsSystem.PickupResource(fromCell);
    }
}
