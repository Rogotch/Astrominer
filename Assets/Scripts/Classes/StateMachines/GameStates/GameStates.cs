using System;
using UnityEngine;

public interface IGameState
{
    public enum STATES {LOADING, LOBBY, GAMEPLAY, PAUSE}
    event Action Entered;
    event Action Exited;
    void Enter();
    void Update();
    void Exit();
}
public abstract class GameState  : IGameState
{
    public GameState(){}

    public event Action Entered;
    public event Action Exited;

    public virtual void Enter()
    {
        Entered.Invoke();
    }
    public virtual void Exit()
    {
        Exited.Invoke();
    }

    public virtual void Update() {}
}
public class LoadingGameState  : GameState, IGameState
{
    public LoadingGameState(){}

    public override void Enter()  {}
    public override void Exit()   {}
    public override void Update() {}
}
public class LobbyGameState    : GameState, IGameState
{
    public LobbyGameState(){}

    public override void Enter()  {}
    public override void Exit()   {}
    public override void Update() {}
}
public class GameplayGameState : GameState, IGameState
{
    public GameplayGameState(){}

    public override void Enter()  {}
    public override void Exit()   {}
    public override void Update() {}
}
public class PauseGameState    : GameState, IGameState
{
    public PauseGameState(){}

    public override void Enter()
    {
        Time.timeScale = 0;
        base.Enter();
    }
    public override void Exit()
    {
        Time.timeScale = 1;
        base.Exit();
    } 
    public override void Update() {}
}