using DG.Tweening;
using System;
using UnityEngine;

public abstract class DiggingInstrument : BaseInstrumentModule
{
    #region Signals
    public virtual event Action<Vector2Int, Vector2Int> DiggingStarted;
    public virtual event Action<Vector2Int, Vector2Int> DiggingEnded;
    public virtual event Action<Vector2Int>             CellDigged;
    #endregion

    #region Переменные инспектора
    [SerializeField]
    protected Grid grid;
    [SerializeField]
    [ReadOnly]
    public bool isDigging = false;
    #endregion

    #region Protected-переменные
    protected Sequence diggingTweenSequence;
    #endregion

    public abstract bool IsCanDig(Vector2Int target);

    public abstract bool IsCanDigFrom(Vector2Int from, Vector2Int to);

    public abstract bool IsCanDigToDirection(Vector2Int from, Vector2Int direction);

    public abstract void Dig(Vector2Int from, Vector2Int to);
    public abstract void DiggingStart(Vector2Int from, Vector2Int to);
    public abstract void DiggingEnd(Vector2Int from, Vector2Int to);
    public virtual void DigCell(Vector2Int from, Vector2Int cell) { }
}
