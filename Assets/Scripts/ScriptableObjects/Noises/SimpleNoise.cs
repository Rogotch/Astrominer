using UnityEngine;

[CreateAssetMenu(menuName = "Game/Noise/Simple Noise Data Layer")]
public class SimpleNoise : BaseNoise, INoiseGenerator
{
    [SerializeField] public bool addZeroSymmetry = false;
    public override float GenerateNoise(float x, float y)
    {
        float  perlinValue = Mathf.PerlinNoise(x * noise_scale, y * noise_scale);
        if (addZeroSymmetry)
            perlinValue = perlinValue * 2 - 1;

        return perlinValue;
    }
    public override bool NoiseCellCheck(float x, float y)
    {
        float noise_value = GenerateNoise(x, y);
        return inversed_check ? noise_value < treshold : noise_value > treshold;
    }
}
