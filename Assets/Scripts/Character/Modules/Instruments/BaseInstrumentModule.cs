using UnityEngine;
using VContainer;

public abstract class BaseInstrumentModule
{
    protected readonly Grid grid;
    protected readonly IAnimationService animationService;
    protected readonly ICellsService     cellsSystem;

    protected BaseInstrumentModule(Grid grid, IAnimationService animationService)
    {
        this.grid = grid;
        this.animationService = animationService;
    }

    protected BaseInstrumentModule(Grid grid, IAnimationService animationService, ICellsService cellsSystem)
    {
        this.grid = grid;
        this.animationService = animationService;
        this.cellsSystem = cellsSystem;
    }
}
