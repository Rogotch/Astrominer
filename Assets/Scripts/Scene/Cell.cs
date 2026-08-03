using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

[System.Serializable]
public class Cell
{
    public float          max_health = 5f;
    [System.NonSerialized]
    public float          health = 5f;
    public TileBase       tile;
    public BlocksResource cell_resource = null;

    public Cell() { }
    public Cell(Cell gived_cell)
    {
        max_health    = gived_cell.max_health;
        health        = gived_cell.max_health;
        tile          = gived_cell.tile;
        cell_resource = gived_cell.cell_resource;
    }

    public float GetHealthLevel()
    {
        return health / max_health;
    }
}
//[System.Serializable]
//public class BlocksResource
//{
//    public TileBase tile;
//    public BlocksResource() { }
//    public BlocksResource(BlocksResource gived_ore)
//    {
//        tile = gived_ore.tile;
//    }
//}
