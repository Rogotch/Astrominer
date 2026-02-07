using UnityEngine;

public class CircleAsteroid : AsteroidParameters
{
    #region Переменные инспектора
    [Header("Размеры астероида")]
    [SerializeField] public int            radius;
    [SerializeField] public Vector2Int     center;
    [Header("Параметры астероида")]
    [SerializeField] public CellsDataLayer layerData;
    #endregion

    public override bool IsInsideAsteroid(Vector2Int position)
    {
        float dist_to_center = Vector2Int.Distance(position, center);
        float noise_value = layerData.noise.GenerateNoise(position);
        float final_radius = radius + noise_value;
        return dist_to_center <= final_radius;
    }
    public override float GetNoiseValue(Vector2Int position)
    {
        return layerData.noise.GenerateNoise(position);
    }

    public override bool NoiseCellCheck(Vector2Int position)
    {
        return layerData.noise.NoiseCellCheck(position);
    }
}
