using System.Collections.Generic;
using UnityEngine;

public class GameStateFactory
{
    public T Get<T>() where T : IGameState, new() {return new T();}
}
