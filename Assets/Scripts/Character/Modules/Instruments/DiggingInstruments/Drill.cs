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

    public Drill(Grid grid, IAnimationService animationService, ICellsService cellsSystem) : base(grid, animationService, cellsSystem) {}
    
    public override void Dig(Vector2Int from, Vector2Int to)
    {
        animationService.Digging(
            from, 
            to, 
            () => DiggingStart(from, to),
            () => DigCell(from, to),
            () => DiggingEnd(from, to));
    }

    public override void DiggingStart(Vector2Int from, Vector2Int to)
    {
        DiggingStarted?.Invoke(from, to);
    }
    public override void DigCell(Vector2Int from, Vector2Int cell)
    {
        cellsSystem.DamageCell(cell, drillDamage);
        CellDigged?.Invoke(cell);
    }
    public override void DiggingEnd(Vector2Int from, Vector2Int to)
    {
        DiggingEnded?.Invoke(from, to);
    }


    public override bool IsCanDig(Vector2Int target)
    {
        return !cellsSystem.IsCellEmpty(target);
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
