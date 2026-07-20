using System;
using DG.Tweening;
using UnityEngine;

public abstract class BaseDigInstrument : BaseInstrumentModule, IDigInstrument
{
    protected BaseDigInstrument(Grid grid, IAnimationService animationService) : base(grid, animationService) {}

    public virtual event Action<Vector2Int, Vector2Int> DiggingStarted;
    public virtual event Action<Vector2Int, Vector2Int> DiggingEnded;
    public virtual event Action<Vector2Int>             CellDigged;

    public virtual void Dig                 (Vector2Int from, Vector2Int to)   {}
    public virtual void DigCell             (Vector2Int from, Vector2Int cell) {}
    public virtual void DiggingEnd          (Vector2Int from, Vector2Int to)   {}
    public virtual void DiggingStart        (Vector2Int from, Vector2Int to)   {}
    public virtual bool IsCanDig            (Vector2Int target)                     {return false;}
    public virtual bool IsCanDigFrom        (Vector2Int from, Vector2Int to)        {return false;}
    public virtual bool IsCanDigToDirection (Vector2Int from, Vector2Int direction) {return false;}
}
