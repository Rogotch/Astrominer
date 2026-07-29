using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelEntryPoint : IStartable, IDisposable
{
    private readonly GridSystem gridSystem;
    private readonly PlayerController player;
    // private readonly GridSystem cellsSystem;
    public LevelEntryPoint(GridSystem gridSystem, PlayerController player)
    {
        this.gridSystem  = gridSystem;
        this.player = player;
    }
    public void Start()
    {
        ScenesChanger.CloseScene(ScenesChanger.ExistingScenes.MAIN_MENU);
        gridSystem.GenerateCave();
        gridSystem.ConnectCellsMap();
    }
    public void Dispose()
    {
        gridSystem.DisconnectCellsMap();
    }

}
