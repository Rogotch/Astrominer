using DG.Tweening;
using System;
using UnityEngine;
using VContainer;

public class Drill : BaseDigInstrument, IDigInstrument
{
    #region Signals
    public override event Action<Vector2Int, Vector2Int> DiggingStarted;
    public override event Action<Vector2Int, Vector2Int> DiggingEnded;
    public override event Action<Vector2Int>             CellDigged;
    #endregion


    public float drillDamage = 1f;

    public Drill(Grid grid) : base(grid) { }
    
    public override void Dig(Vector2Int from, Vector2Int to)
    {
        Debug.Log($"grid {grid}");
        DiggingStart(from, to);
    }

    public override void DiggingEnd(Vector2Int from, Vector2Int to)
    {
        DiggingEnded?.Invoke(from, to);
    }

    public override void DigCell(Vector2Int from, Vector2Int cell)
    {
        CellsSystem.DamageCell(cell, drillDamage);
        CellDigged?.Invoke(cell);
    }

    public override bool IsCanDig(Vector2Int target)
    {
        return !CellsSystem.IsCellEmpty(target);
    }

    public override bool IsCanDigFrom(Vector2Int from, Vector2Int to)
    {
        if ((from - to).magnitude != 1) return false;
        return IsCanDig(to);
    }

    public override bool IsCanDigToDirection(Vector2Int from, Vector2Int direction)
    {
        return IsCanDigFrom(from, from + direction);
    }
}
