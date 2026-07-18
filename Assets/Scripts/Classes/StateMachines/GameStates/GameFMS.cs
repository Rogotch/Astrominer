using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

public class GameFMS
{
    private IGameState currentState;
    private GameStateFactory gameStateFactory;
    public void SwitchTo(IGameState.STATES state)
    {
        currentState?.Exit();
        currentState = GetStateByEnum(state);
        currentState.Enter();
    }

    public void Update() => currentState?.Update();
    private IGameState GetStateByEnum(IGameState.STATES stateEnum)
    {
        return stateEnum switch
        {
            IGameState.STATES.LOADING  => gameStateFactory.Get <LoadingGameState>(),
            IGameState.STATES.LOBBY    => gameStateFactory.Get   <LobbyGameState>(),
            IGameState.STATES.GAMEPLAY => gameStateFactory.Get<GameplayGameState>(),
            IGameState.STATES.PAUSE    => gameStateFactory.Get   <PauseGameState>(),
            _ => null,
        };
    }
}
