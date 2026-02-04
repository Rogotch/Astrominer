using UnityEngine;

[System.Serializable]
public class CellsDataLayer : GenerationDataLayer
{
    public Cell cell_data;
    public OresDataLayer[] ores_data;
    public bool additional = false;
}
[System.Serializable]
public class OresDataLayer : GenerationDataLayer
{
    public BlocksResource resource_params;
}
[System.Serializable]
public class GenerationDataLayer
{
    public BaseNoise noise;
}
