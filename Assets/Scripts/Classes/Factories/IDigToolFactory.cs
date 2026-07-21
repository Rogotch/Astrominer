using System;
using UnityEngine;

public interface IDigToolFactory
{
    IDigInstrument Create(IDigInstrument.ToolType type, IAnimationService animService);
}

public class DigToolFactory : IDigToolFactory
{
    private readonly Grid grid;
    private readonly ICellsService cellsSystem;
    public DigToolFactory(Grid grid, ICellsService cellsSystem)
    {
        this.grid = grid;
        this.cellsSystem = cellsSystem;
    }

    public IDigInstrument Create(IDigInstrument.ToolType type, IAnimationService animService)
    {
        return type switch
        {
            IDigInstrument.ToolType.DRILL => new Drill(grid, animService, cellsSystem),
            _ => throw new ArgumentException($"Unknown tool type: {type}")
        };
    }
}