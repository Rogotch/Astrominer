using UnityEngine;

public abstract class AsteroidParameters : ScriptableObject, INoiseGenerator
{
    #region Public
    // [Header("��������� ���������")]
    [SerializeField] public CellsDataLayer layerData;
    #endregion

    public abstract bool IsInsideAsteroid(Vector2Int position);
    public abstract float GenerateNoise(float x, float y);
    public abstract bool NoiseCellCheck(float x, float y);
}
