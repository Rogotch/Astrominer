using UnityEngine;

[CreateAssetMenu(menuName = "Game/Asteroid Parameters/Circle")]
public class CircleAsteroid : AsteroidParameters, INoiseGenerator
{
    #region Переменные инспектора
    [Header("Размеры астероида")]
    [SerializeField] public int            radius;
    [SerializeField] public Vector2Int     center;
    #endregion

    public override bool IsInsideAsteroid(Vector2Int position)
    {
        float dist_to_center = Vector2Int.Distance(position, center);
        float noise_value = layerData.noise.GenerateNoise(position);
        float final_radius = radius + noise_value;
        return dist_to_center <= final_radius;
    }
    public virtual float GenerateNoise(Vector2Int position)
    {
        return GenerateNoise(position.x, position.y);
    }
    public override float GenerateNoise(float x, float y)
    {
        return layerData.noise.GenerateNoise(x, y);
    }
    public virtual bool NoiseCellCheck(Vector2Int position)
    {
        return NoiseCellCheck(position.x, position.y);
    }
    public override bool NoiseCellCheck(float x, float y)
    {
        return layerData.noise.NoiseCellCheck(x, y);
    }
}
