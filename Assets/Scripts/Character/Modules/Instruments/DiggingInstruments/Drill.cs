using DG.Tweening;
using System;
using UnityEngine;

public class Drill : BaseDigInstrument, IDigInstrument
{
    #region Signals
    public override event Action<Vector2Int, Vector2Int> DiggingStarted;
    public override event Action<Vector2Int, Vector2Int> DiggingEnded;
    public override event Action<Vector2Int>             CellDigged;
    #endregion

    public override void Dig(Vector2Int from, Vector2Int to)
    {
        DiggingStart(from, to);
    }

    public override void DiggingStart(Vector2Int from, Vector2Int to)
    {
        Vector2Int direction      = to - from;
        Vector3    offset         = new Vector3(direction.x * (grid.cellSize.x / 5), direction.y * (grid.cellSize.y / 5), 0);
        Vector3    startPosition  = grid.CellToLocal(new Vector3Int(from.x, from.y, 0)) + grid.cellSize / 2;

        startPosition.z = transform.position.z;

        DiggingStarted?.Invoke(from, to);
        diggingTweenSequence = DOTween.Sequence();
        diggingTweenSequence.Append(transform
            .DOMove(startPosition + offset, timeForStep)
            .SetEase(easeIn)
            .OnComplete(() => DigCell(from, to)));
        diggingTweenSequence.Append(transform
            .DOMove(startPosition,          timeForStep)
            .SetEase(easeOut)
            .OnComplete(() => DiggingEnd(from, to)));
        diggingTweenSequence.Play();
        //    .SetEase(ease)
        //    .OnComplete(EndMoving);
    }

    public override void DiggingEnd(Vector2Int from, Vector2Int to)
    {
        diggingTweenSequence.Kill();
        diggingTweenSequence = null;

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
