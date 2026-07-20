using UnityEngine;
using VContainer;

public abstract class BaseInstrumentModule : BaseModule
{
    protected readonly Grid grid;
    protected readonly IAnimationService animationService;

    protected BaseInstrumentModule(Grid grid, IAnimationService animationService)
    {
        this.grid = grid;
        this.animationService = animationService;
    }
}
