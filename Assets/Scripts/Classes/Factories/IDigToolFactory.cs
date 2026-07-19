using System;
using UnityEngine;

public interface IDigToolFactory
{
    IDigInstrument Create(IDigInstrument.ToolType type);
}

public class DigToolFactory : IDigToolFactory
{
    private readonly Grid grid;
    public DigToolFactory(Grid grid)
    {
        this.grid = grid;
    }

    public IDigInstrument Create(IDigInstrument.ToolType type)
    {
        return type switch
        {
            IDigInstrument.ToolType.DRILL => new Drill(grid),
            _ => throw new ArgumentException($"Unknown tool type: {type}")
        };
    }
}