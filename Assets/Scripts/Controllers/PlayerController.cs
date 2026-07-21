using System;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

public class PlayerController : BaseCharacterController, IStartable, IDisposable
{
    #region Injections
    [Inject] private IPlayerInputService input;
    #endregion

    #region Private variables
    private Dictionary<string, Item> picked_resources = new Dictionary<string, Item>();
    #endregion
    public Vector2Int delayed_command;


    public override void Start()
    {
        base.Start();
        EquipDigTool(IDigInstrument.ToolType.DRILL);
        input.OnMove            += MoveInput;
        moveService.MovingEnded += CheckSteppedCell;
        ChangeState(CharacterState.STATES.IDLE);
    }
    public override void Dispose()
    {
        base.Dispose();
        input.OnMove -= MoveInput;
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
        if (CellsSystem.ResourcesCells.ContainsKey(cell_to))
        {
            PickupResource(CellsSystem.ResourcesCells[cell_to]);
        }
    }

    public void PickupResource(Item item)
    {
        if (picked_resources.ContainsKey(item.resourceData.tag))
        {
            picked_resources[item.resourceData.tag].count += item.count;
        }
        else
        {
            picked_resources[tag] = item;
        }
        CellsSystem.PickupResource(gridPosition);
    }

}
