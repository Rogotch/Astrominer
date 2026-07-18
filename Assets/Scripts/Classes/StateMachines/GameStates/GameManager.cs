using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameFMS gameFMS;

    void Start()
    {
        gameFMS = new GameFMS();
        gameFMS.SwitchTo(IGameState.STATES.LOADING);
    }
}
