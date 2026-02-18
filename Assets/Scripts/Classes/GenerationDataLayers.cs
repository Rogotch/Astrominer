using UnityEngine;

[System.Serializable]
public class CellsDataLayer : GenerationDataLayer
{
    public CellData cell_data;
    public BlocksResource resource_params;
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
