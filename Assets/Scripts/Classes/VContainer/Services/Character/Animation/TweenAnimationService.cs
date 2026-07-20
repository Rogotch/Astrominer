using System;
using DG.Tweening;
using UnityEngine;

public class TweenAnimationService : BaseAnimationService, IAnimationService
{
    public TweenAnimationService(Grid grid, Transform characterTransform, TweenAnimationConfig config) : base(grid, characterTransform)
    {
        this.config = config;
    }

    protected Sequence tweenSequence;
    private readonly TweenAnimationConfig config;

    public override void Digging(
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
            .DOMove(startPosition + offset, config.timeForStepIn)
            .SetEase(config.easeIn)
            .OnStart(() => onStarted?.Invoke())
            .OnComplete(() => onHit?.Invoke()));
        tweenSequence.Append(characterTransform
            .DOMove(startPosition,          config.timeForStepOut)
            .SetEase(config.easeOut)
            .OnComplete(() => onCompleted?.Invoke()));
        tweenSequence.Play();
    }
}
