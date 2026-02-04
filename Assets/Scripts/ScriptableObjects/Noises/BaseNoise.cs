using UnityEngine;

public abstract class BaseNoise : ScriptableObject
{
    [Range(0.01f, 0.2f)]
    public float noise_scale = 0.05f;
    [Range(0f, 1f)]
    public float treshold = 0.5f;

    public bool inversed_check;

    public abstract float GenerateNoise(int x, int y);

    public abstract bool NoiseCellCheck(int x, int y);
}
