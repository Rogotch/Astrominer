using UnityEngine;

public abstract class AsteroidParameters : ScriptableObject
{
    public abstract bool IsInsideAsteroid(Vector2Int position);
    public abstract float GetNoiseValue(Vector2Int position);
    public abstract bool NoiseCellCheck(Vector2Int position);
}
