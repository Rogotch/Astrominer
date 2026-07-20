using System;
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

public abstract class BaseAnimationService : IAnimationService
{
    protected readonly Transform characterTransform;
    protected readonly Grid grid;
    protected BaseAnimationService(Grid grid, Transform characterTransform)
    {
        this.characterTransform = characterTransform;
        this.grid = grid;
    }
    
    public abstract void Digging(
        Vector2Int from,
        Vector2Int to,
        Action onStarted    = null,
        Action onHit        = null,
        Action onCompleted  = null);
}