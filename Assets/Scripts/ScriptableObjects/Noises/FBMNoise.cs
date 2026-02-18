using UnityEngine;

[CreateAssetMenu(menuName = "Game/Noise/FBM Noise Data Layer")]
public class FBMNoise : SimpleNoise
{ 
    [Range(1, 5)]
    public int octaves = 4;

    [Range(0f, 1f)]
    public float persistence = 0.5f;

    [Range(1f, 4f)]
    public float lacunarity = 2f;

    public override float GenerateNoise(float x, float y)
    {
        float total = 0f;
        float frequency = noise_scale;
        float amplitude = 1f;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX = x * frequency;
            float sampleY = y * frequency;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
            total += perlinValue * amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return total;
    }
}
