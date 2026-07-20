using System;
using DG.Tweening;
using UnityEngine;

public interface IMovementService
{
    
    #region Actions
    event Action<Vector2Int, Vector2Int> MovingStarted;
    event Action<Vector2Int, Vector2Int> MovingEnded;
    event Action<Vector2Int> OnPosition;
    #endregion

    public abstract void MoveTo(Vector2Int from, Vector2Int target);

    public abstract bool IsCanMove(Vector2Int target);
    public abstract void StartMoving(Vector2Int from, Vector2Int target);
    public abstract void EndMoving();
    public abstract void SetOnPosition(Vector2Int position);
}

public abstract class BaseMovement : IMovementService
{
    #region Actions
    public virtual event Action<Vector2Int, Vector2Int> MovingStarted;
    public virtual event Action<Vector2Int, Vector2Int> MovingEnded;
    public virtual event Action<Vector2Int> OnPosition;
    #endregion

    #region Injections
    protected readonly Grid grid;
    protected readonly Transform transform;
    #endregion

    #region Protecteds
    protected Vector2Int startGridPosition;
    protected Vector2Int targetGridPosition;
    protected Vector3    targetPosition;
    #endregion
    
    protected BaseMovement(Grid grid, Transform transform)
    {
        this.grid = grid;
        this.transform = transform;
    }

    public abstract bool IsCanMove(Vector2Int target);
    public abstract void MoveTo(Vector2Int from, Vector2Int target);
    public abstract void StartMoving(Vector2Int from, Vector2Int target);
    public abstract void EndMoving();
    public abstract void SetOnPosition(Vector2Int position);

}