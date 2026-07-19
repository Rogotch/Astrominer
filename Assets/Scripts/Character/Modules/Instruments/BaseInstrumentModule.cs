using UnityEngine;
using VContainer;

public abstract class BaseInstrumentModule : BaseModule
{
    protected readonly Grid grid;

    protected BaseInstrumentModule(Grid grid)
    {
        this.grid = grid;
    }
}
