using System;
using UnityEngine;

public abstract class CharacterState
{
    public enum STATES {START_TURN, IDLE, MOVE, MINE, END_TURN}
    protected readonly Func<BaseCharacterController> getController;
    public CharacterState(Func<BaseCharacterController> getController) => this.getController = getController;

    public virtual void Enter(){}
    public virtual void Exit(){}
}

public abstract class CharacterTargetedState : CharacterState
{
    public CharacterTargetedState(Func<BaseCharacterController> getController) : base(getController) {}
    public    virtual bool IsCanBeActivatedToDirection(Vector2Int cell_from, Vector2Int direction) => false;
    public    virtual bool            IsCanBeActivated(Vector2Int cell_from, Vector2Int cell_to) => false;
    public    virtual void                    Activate(Vector2Int cell_from, Vector2Int cell_to) { }
    protected virtual void                     Started(Vector2Int cell_from, Vector2Int cell_to) { }
    protected virtual void                       Ended(Vector2Int cell_from, Vector2Int cell_to) { getController().ChangeState(STATES.END_TURN); }
    protected virtual void                   Activated(Vector2Int on_cell) { }
}

public class CharacterIdle : CharacterState
{
    public   CharacterIdle(Func<BaseCharacterController> getController) : base(getController) {}
}
public class CharacterTurnStarted: CharacterState
{
    public CharacterTurnStarted(Func<BaseCharacterController> getController) : base(getController) {}
    public override void Enter()
    {
        getController().ChangeState(STATES.IDLE);
    }
}
public class CharacterTurnEnded: CharacterState
{
    public CharacterTurnEnded(Func<BaseCharacterController> getController) : base(getController) {}
    public override void Enter()
    {
        getController().ChangeState(STATES.START_TURN);
    }
}

public class CharacterMove : CharacterTargetedState
{
    protected readonly Func<Movement> getMovement;
    public   CharacterMove(Func<BaseCharacterController> getController) : base(getController)
    {
        getMovement = () => getController().movementModule;
    }

    public override void Enter()
    {
        Movement movement = getMovement();
        if (movement == null) return;
        movement.OnPosition    += Activated;
        movement.MovingStarted += Started;
        movement.MovingEnded   += Ended;
    }
    public override void Exit()
    {
        Movement movement = getMovement();
        if (movement == null) return;
        movement.OnPosition    -= Activated;
        movement.MovingStarted -= Started;
        movement.MovingEnded   -= Ended;
    }
    public override bool IsCanBeActivatedToDirection(Vector2Int cell_from, Vector2Int direction)
    {
        if (getMovement() == null) return false;
        return getMovement().IsCanMove(cell_from + direction);
    }
    public override bool IsCanBeActivated(Vector2Int cell_from, Vector2Int cell_to)
    {
        if (getMovement() == null) return false;
        return getMovement().IsCanMove(cell_to);
    }

    public override void Activate(Vector2Int cell_from, Vector2Int cell_to)
    {
        getMovement().MoveTo(cell_from, cell_to);
    }
    protected override void Activated(Vector2Int on_cell)
    {
        getController().gridPosition = on_cell;
    }
}
public class CharacterMine : CharacterTargetedState
{
    protected readonly Func<IDigInstrument> getInstrument;
    public   CharacterMine(Func<BaseCharacterController> getController) : base(getController)
    {
        getInstrument = () => getController().Equipment.currentDigTool;
    }

    public override void Enter()
    {
        IDigInstrument instrument = getInstrument();
        if (instrument == null) return;

        instrument.CellDigged      += Activated;
        instrument.DiggingStarted  += Started;
        instrument.DiggingEnded    += Ended;
    }
    public override void Exit()
    {
        IDigInstrument instrument = getInstrument();
        if (instrument == null) return;

        instrument.CellDigged      -= Activated;
        instrument.DiggingStarted  -= Started;
        instrument.DiggingEnded    -= Ended;
    }
    public override bool IsCanBeActivatedToDirection(Vector2Int cell_from, Vector2Int direction)
    {
        if (getInstrument() == null) return false;
        return getInstrument().IsCanDigToDirection(cell_from, direction);
    }
    public override bool IsCanBeActivated(Vector2Int cell_from, Vector2Int cell_to)
    {
        if (getInstrument() == null) return false;
        return getInstrument().IsCanDigFrom(cell_from, cell_to);
    }

    public override void Activate(Vector2Int cell_from, Vector2Int cell_to)
    {
        getInstrument().Dig(cell_from, cell_to);
    }
}