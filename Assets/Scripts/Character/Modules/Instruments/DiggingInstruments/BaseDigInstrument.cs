using System;
using DG.Tweening;
using UnityEngine;

public abstract class BaseDigInstrument : BaseInstrumentModule, IDigInstrument
{
    protected Sequence diggingTweenSequence;

    #region Setting variables
    protected float drillDamage = 1.0f;
    protected float timeForStep = 0.3f;
    protected Ease  easeIn      = Ease.InQuad;
    protected Ease  easeOut     = Ease.OutQuad;
    #endregion

    protected BaseDigInstrument(Grid grid) : base(grid) {}

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
