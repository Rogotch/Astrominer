using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerEntryPoint : IStartable
{
    private readonly IPlayerInputService input;
    private readonly PlayerController    player;

    public PlayerEntryPoint(IPlayerInputService input, PlayerController player)
    {
        this.input  = input;
        this.player = player;
    }
    public void Start()
    {
        Debug.Log("Game started with DI");
    }
}
