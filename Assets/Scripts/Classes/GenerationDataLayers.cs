using UnityEngine;

// [CreateAssetMenu(fileName = "CellsDataLayersArray", menuName = "Game/Configs/Cells Data Layers Array")]
// public class CellsDataLayersArray : ScriptableObject
// {
//     public CellsDataLayer[] cellsDataLayers;
// }
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
