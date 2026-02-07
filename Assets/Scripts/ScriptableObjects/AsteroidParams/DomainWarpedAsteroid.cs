using UnityEngine;
using UnityEngine.UIElements;

public class DomainWarpedAsteroid : CircleAsteroid
{
    #region Переменные инспектора
    [Header("Domain Warping")]
    [Range(0f, 1f),      SerializeField] protected float warpStrength;
    [Range(0.01f, 0.2f), SerializeField] protected float warpScale;
    [                    SerializeField] public    bool  addSymmetry = false;
    [                    SerializeField] public    float yOffset = 1000f;
    #endregion

    public override bool IsInsideAsteroid(Vector2Int position)
    {
        Vector2 warped = GetWarpedValue(position);

        float dist_to_center = Vector2.Distance(warped, center);

        float noise_value = layerData.noise.GenerateNoise((warped - center));

        float final_radius = radius + noise_value;
        return dist_to_center <= final_radius;
    }
    public override float GetNoiseValue(Vector2Int position)
    {
        Vector2 new_coord = GetWarpedValue(position) - center;
        return layerData.noise.GenerateNoise(new_coord);
    }
    public override bool NoiseCellCheck(Vector2Int position)
    {
        Vector2 new_coord = GetWarpedValue(position) - center;
        return layerData.noise.NoiseCellCheck(new_coord);
    }
    private Vector2 GetWarpedValue(Vector2Int position)
    {
        Vector2 warp = new Vector2(
            Mathf.PerlinNoise(position.x * warpScale,           position.y * warpScale),            //x
            Mathf.PerlinNoise(position.x * warpScale + yOffset, position.y * warpScale + yOffset)); //y

        if (addSymmetry)
            warp = warp * 2 - Vector2.one;

        Vector2 warped = new Vector2(
            position.x + warp.x * warpStrength * radius,
            position.y + warp.y * warpStrength * radius);
        return warped;
    }
}
