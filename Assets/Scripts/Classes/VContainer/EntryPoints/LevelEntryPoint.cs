using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelEntryPoint : IStartable, IDisposable
{
    private readonly GridSystem    gridSystem;
    private readonly ScenesChanger scenesChanger;
    public LevelEntryPoint(GridSystem gridSystem, ScenesChanger scenesChanger)
    {
        this.gridSystem     = gridSystem;
        this.scenesChanger  = scenesChanger;
    }
    public void Start()
    {
        gridSystem.GenerateCave();
        gridSystem.ConnectCellsMap();
    }
    public void Dispose()
    {
        gridSystem.DisconnectCellsMap();
    }

}
