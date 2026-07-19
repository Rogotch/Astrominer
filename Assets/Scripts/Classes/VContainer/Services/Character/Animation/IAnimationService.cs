using System;
using DG.Tweening;
using UnityEngine;

public interface IAnimationService
{
    void Digging(
        Vector2Int from,
        Vector2Int to,
        Action onStarted    = null,
        Action onHit        = null,
        Action onCompleted  = null);
}

public class TweenAnimationService : IAnimationService
{
    private readonly Transform characterTransform;
    private readonly Grid grid;
    public TweenAnimationService(Grid grid, Transform characterTransform)
    {
        this.characterTransform = characterTransform;
        this.grid = grid;
    }
    
    protected Sequence tweenSequence;

    #region Setting variables
    protected float timeForStep = 0.3f;
    protected Ease  easeIn      = Ease.InQuad;
    protected Ease  easeOut     = Ease.OutQuad;
    #endregion

    public void Digging(
        Vector2Int from,
        Vector2Int to,
        Action onStarted    = null,
        Action onHit        = null,
        Action onCompleted  = null)
    {
        Vector2Int direction      = to - from;
        Vector3    offset         = new Vector3(direction.x * (grid.cellSize.x / 5), direction.y * (grid.cellSize.y / 5), 0);
        Vector3    startPosition  = grid.CellToLocal(new Vector3Int(from.x, from.y, 0)) + grid.cellSize / 2;

        startPosition.z = characterTransform.position.z;

        tweenSequence = DOTween.Sequence();
        tweenSequence.Append(characterTransform
            .DOMove(startPosition + offset, timeForStep)
            .SetEase(easeIn)
            .OnStart(() => onStarted?.Invoke())
            .OnComplete(() => onHit?.Invoke()));
        tweenSequence.Append(characterTransform
            .DOMove(startPosition,          timeForStep)
            .SetEase(easeOut)
            .OnComplete(() => onCompleted?.Invoke()));
        tweenSequence.Play();
        //    .SetEase(ease)
        //    .OnComplete(EndMoving);
    }
}