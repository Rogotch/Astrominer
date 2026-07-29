using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerEntryPoint : IStartable, IDisposable
{
    private readonly IPlayerInputService  input;
    private readonly PlayerController     player;
    private readonly AsteroidConfig       config;
    private readonly BaseCameraController sceneCamera;

    public PlayerEntryPoint(IPlayerInputService input, PlayerController player, AsteroidConfig config, BaseCameraController sceneCamera)
    {
        this.input       = input;
        this.player      = player;
        this.config      = config;
        this.sceneCamera = sceneCamera;
    }
    public void Start()
    {
        Debug.Log("Game started with DI");
        player.SetCharacterOnCell(config.startPosition);
        sceneCamera.SetFollowedCharacter(player);
        sceneCamera.SetOnCharacterPosition();
        // sceneCamera
        player.StartConfiguration();
    }
    public void Dispose()
    {        
        player.DisposeConfiguration();
    }

}