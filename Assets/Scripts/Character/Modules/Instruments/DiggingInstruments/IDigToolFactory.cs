using System;
using UnityEngine;

public interface IDigToolFactory
{
    IDigInstrument Create(IDigInstrument.ToolType type);
}

public class DigToolFactory : IDigToolFactory
{
    public IDigInstrument Create(IDigInstrument.ToolType type)
    {
        return type switch
        {
            IDigInstrument.ToolType.DRILL => new Drill(),
            _ => throw new ArgumentException($"Unknown tool type: {type}")
        };
    }
}