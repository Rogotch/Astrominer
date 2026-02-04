using UnityEngine;

[CreateAssetMenu(menuName = "Game/Simple Noise Data Layer")]
public class SimpleNoise : BaseNoise
{

    public override float GenerateNoise(int x, int y)
    {
        float  perlinValue = Mathf.PerlinNoise(x * noise_scale, y * noise_scale) * 2 - 1;
        return perlinValue;
    }
    public float GenerateNoise(Vector2Int position)
    {
        return   GenerateNoise(position.x, position.y);
    }

    public override bool NoiseCellCheck(int x, int y)
    {
        float noise_value = GenerateNoise(x, y);
        return inversed_check ? noise_value < treshold : noise_value > treshold;
    }
    public bool NoiseCellCheck(Vector2Int position)
    {
        return  NoiseCellCheck(position.x, position.y);
    }
}
