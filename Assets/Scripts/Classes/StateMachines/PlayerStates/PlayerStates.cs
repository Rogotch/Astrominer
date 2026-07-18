using System;
using UnityEngine;

public class PlayerIdle : CharacterIdle
{
    public PlayerIdle(Func<PlayerController> getController) : base(getController) {}

    public void ReciveInputDirection(Vector2Int direction)
    {
        (getController() as PlayerController).delayed_command = Vector2Int.zero;

        CharacterMove move_check = new CharacterMove(getController);
        CharacterMine mine_check = new CharacterMine(getController);

        if      (move_check.IsCanBeActivatedToDirection(getController().gridPosition, direction)) ActivateToDirection(STATES.MOVE, direction);
        else if (mine_check.IsCanBeActivatedToDirection(getController().gridPosition, direction)) ActivateToDirection(STATES.MINE, direction);
    }

    private void ActivateToDirection(STATES action, Vector2Int direction)
    {
         getController().ChangeState(action);
        (getController().currentState as CharacterTargetedState)?.Activate(getController().gridPosition, getController().gridPosition + direction);
    }


    public override void Enter()
    {
        PlayerController controller = (getController() as PlayerController);
        base.Enter();
        if (controller.delayed_command.magnitude > 0) ReciveInputDirection(controller.delayed_command);
    }
}
