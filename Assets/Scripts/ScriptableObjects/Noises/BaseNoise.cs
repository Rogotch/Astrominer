using UnityEngine;

public abstract class BaseNoise : ScriptableObject, INoiseGenerator
{
    [Range(0.01f, 0.2f)]
    public float noise_scale = 0.05f;
    [Range(0f, 1f)]
    public float treshold = 0.5f;

    public bool inversed_check;

    public abstract float GenerateNoise(float x, float y);
    public virtual  float GenerateNoise(int x, int y)            {return GenerateNoise((float)x, (float)y);}
    public virtual  float GenerateNoise(Vector2Int position)     {return GenerateNoise(position.x, position.y);}
    public virtual  float GenerateNoise(Vector2    position)     {return GenerateNoise(position.x, position.y);}
    public abstract bool  NoiseCellCheck(float x, float y);
    public virtual  bool  NoiseCellCheck(int   x, int   y)       {return NoiseCellCheck((float)x, (float)y);}
    public virtual  bool  NoiseCellCheck(Vector2Int position)    {return NoiseCellCheck(position.x, position.y);}
    public virtual  bool  NoiseCellCheck(Vector2    position)    {return NoiseCellCheck(position.x, position.y);}
}
