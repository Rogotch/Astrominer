using UnityEngine;

public abstract class CharacterState
{
    public enum STATES {START_TURN, IDLE, MOVE, MINE, END_TURN}
    protected BaseCharacterController controller;
    public CharacterState(BaseCharacterController controller) => this.controller = controller;

    public virtual void Enter(){}
    public virtual void Exit(){}
}

public abstract class CharacterTargetedState : CharacterState
{
    public CharacterTargetedState(BaseCharacterController controller) : base(controller) { }
    public    virtual bool IsCanBeActivatedToDirection(Vector2Int cell_from, Vector2Int direction) => false;
    public    virtual bool            IsCanBeActivated(Vector2Int cell_from, Vector2Int cell_to) => false;
    public    virtual void                    Activate(Vector2Int cell_from, Vector2Int cell_to) { }
    protected virtual void                     Started(Vector2Int cell_from, Vector2Int cell_to) { }
    protected virtual void                       Ended(Vector2Int cell_from, Vector2Int cell_to) { controller.ChangeState(STATES.END_TURN); }
    protected virtual void                   Activated(Vector2Int on_cell) { }
}

public class CharacterIdle : CharacterState
{
    public   CharacterIdle(BaseCharacterController controller) : base(controller) { }
}
public class CharacterTurnStarted: CharacterState
{
    public CharacterTurnStarted(BaseCharacterController controller) : base(controller) { }
    public override void Enter()
    {
        controller.ChangeState(STATES.IDLE);
    }
}
public class CharacterTurnEnded: CharacterState
{
    public CharacterTurnEnded(BaseCharacterController controller) : base(controller) { }
    public override void Enter()
    {
        controller.ChangeState(STATES.START_TURN);
    }
}

public class CharacterMove : CharacterTargetedState
{
    private Movement movement;
    public   CharacterMove(BaseCharacterController controller) : base(controller)
    {
        movement = controller.movementModule;
    }

    public override void Enter()
    {
        if (movement == null) return;

        movement.OnPosition    += Activated;
        movement.MovingStarted += Started;
        movement.MovingEnded   += Ended;
    }
    public override void Exit()
    {
        if (movement == null) return;

        movement.OnPosition    -= Activated;
        movement.MovingStarted -= Started;
        movement.MovingEnded   -= Ended;
    }
    public override bool IsCanBeActivatedToDirection(Vector2Int cell_from, Vector2Int direction)
    {
        if (movement == null) return false;
        return movement.IsCanMove(cell_from + direction);
    }
    public override bool IsCanBeActivated(Vector2Int cell_from, Vector2Int cell_to)
    {
        if (movement == null) return false;
        return movement.IsCanMove(cell_to);
    }

    public override void Activate(Vector2Int cell_from, Vector2Int cell_to)
    {
        movement.MoveTo(cell_from, cell_to);
    }
    protected override void Activated(Vector2Int on_cell)
    {
        controller.gridPosition = on_cell;
    }
}
public class CharacterMine : CharacterTargetedState
{
    private DiggingInstrument instrument;
    public   CharacterMine(BaseCharacterController controller) : base(controller)
    {
        instrument = controller.diggingTool;
    }

    public override void Enter()
    {
        if (instrument == null) return;

        instrument.CellDigged      += Activated;
        instrument.DiggingStarted  += Started;
        instrument.DiggingEnded    += Ended;
    }
    public override void Exit()
    {
        if (instrument == null) return;

        instrument.CellDigged      -= Activated;
        instrument.DiggingStarted  -= Started;
        instrument.DiggingEnded    -= Ended;
    }
    public override bool IsCanBeActivatedToDirection(Vector2Int cell_from, Vector2Int direction)
    {
        if (instrument == null) return false;
        return instrument.IsCanDigToDirection(cell_from, direction);
    }
    public override bool IsCanBeActivated(Vector2Int cell_from, Vector2Int cell_to)
    {
        if (instrument == null) return false;
        return instrument.IsCanDigFrom(cell_from, cell_to);
    }

    public override void Activate(Vector2Int cell_from, Vector2Int cell_to)
    {
        instrument.Dig(cell_from, cell_to);
    }
}