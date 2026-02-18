using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class PlayerController : BaseCharacterController
{

    #region Private variables
    private PlayerInput player_input;
    private Vector2Int delayed_command;
    private Dictionary<string, Item> picked_resources = new Dictionary<string, Item>();

    #endregion

    protected override void Awake()
    {
        base.Awake();
        ConnectInput(GetComponent<PlayerInput>());
        CellsSystem.Player = this;
        ChangeState(CharacterState.STATES.IDLE);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void ConnectInput(PlayerInput new_player_input)
    {
        player_input = new_player_input;
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 raw_input = context.ReadValue<Vector2>();
        Vector2Int direction = new Vector2Int((int)raw_input.x, (int)raw_input.y);
        (currentState as PlayerIdle)?.ReciveInputDirection(direction);
    }

    public override void ChangeState(CharacterState.STATES state)
    {
        CharacterState new_state = state switch
        {
            CharacterState.STATES.START_TURN => new CharacterTurnStarted(this),
            CharacterState.STATES.IDLE       => new PlayerIdle(this),
            CharacterState.STATES.MOVE       => new CharacterMove(this),
            CharacterState.STATES.MINE       => new CharacterMine(this),
            CharacterState.STATES.END_TURN   => new CharacterTurnEnded(this),
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
