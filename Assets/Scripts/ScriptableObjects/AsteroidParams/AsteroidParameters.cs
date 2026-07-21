using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidParameters", menuName = "Game/AsteroidParameters")]
public abstract class AsteroidParameters : ScriptableObject, INoiseGenerator
{
    #region Public
    [SerializeField] public CellsDataLayer layerData;
    #endregion

    public abstract bool IsInsideAsteroid(Vector2Int position);
    public abstract float GenerateNoise(float x, float y);
    public abstract bool NoiseCellCheck(float x, float y);
}
