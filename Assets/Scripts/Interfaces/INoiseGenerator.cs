using UnityEngine;

public interface INoiseGenerator
{
    public abstract float GenerateNoise(float x, float y);
    public abstract bool NoiseCellCheck(float x, float y);
}
