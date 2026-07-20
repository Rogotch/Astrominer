using System;
using UnityEngine;
using DG.Tweening;

public class TweenMovement : BaseMovement, IMovementService
{
    public override event Action<Vector2Int, Vector2Int> MovingStarted;
    public override event Action<Vector2Int, Vector2Int> MovingEnded;
    public override event Action<Vector2Int> OnPosition;
    
    private readonly TweenMoveConfig config;
    protected Tween      currentTween;
    public TweenMovement(Grid grid, Transform transform, TweenMoveConfig config) : base(grid, transform)
    {
        this.config = config;
    }

    public override bool IsCanMove(Vector2Int target)
    {
        return CellsSystem.IsCellEmpty(target);
    }

    public override void MoveTo(Vector2Int from, Vector2Int target)
    {
        StartMoving(from, target);
    }

    public override void StartMoving(Vector2Int from, Vector2Int target)
    {
        targetPosition = grid.CellToLocal(new Vector3Int(target.x, target.y, 0)) + grid.cellSize / 2;
        targetPosition.z = transform.position.z;

        startGridPosition  = from;
        targetGridPosition = target;
        MovingStarted?.Invoke(from, target);
        currentTween = transform
            .DOMove(targetPosition, config.timeForStep)
            .SetEase(config.ease)
            .OnComplete(EndMoving);
        currentTween.Play();
    }

    public override void EndMoving()
    {
        currentTween.Kill();
        currentTween = null;

        OnPosition?.Invoke(targetGridPosition);
        MovingEnded?.Invoke(startGridPosition, targetGridPosition);
    }

    public override void SetOnPosition(Vector2Int position)
    {
        targetPosition = grid.CellToLocal(new Vector3Int(position.x, position.y, 0)) + grid.cellSize / 2;
        targetPosition.z = transform.position.z;

        transform.position = targetPosition;
        OnPosition?.Invoke(targetGridPosition);
    }
}
