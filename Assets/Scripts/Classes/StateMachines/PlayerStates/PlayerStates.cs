using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerIdle : CharacterIdle
{
    public PlayerIdle(PlayerController controller) : base(controller) {}

    public void ReciveInputDirection(Vector2Int direction)
    {
        CharacterMove move_check = new CharacterMove(controller);
        CharacterMine mine_check = new CharacterMine(controller);

        if      (move_check.IsCanBeActivatedToDirection(controller.gridPosition, direction)) ActivateToDirection(STATES.MOVE, direction);
        else if (mine_check.IsCanBeActivatedToDirection(controller.gridPosition, direction)) ActivateToDirection(STATES.MINE, direction);
    }

    private void ActivateToDirection(STATES action, Vector2Int direction)
    {
        controller.ChangeState(action);
        (controller.currentState as CharacterTargetedState)?.Activate(controller.gridPosition, controller.gridPosition + direction);
    }
}
