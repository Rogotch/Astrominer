using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BreakingProcessTiles", menuName = "Game/Configs/Breaking Process Tiles")]
public class BreakingProcessTiles : ScriptableObject
{
    public TileBase[] breakingTiles;


    public TileBase GetTileByCell(Cell cell)
    {
        float breakingProgress = 1 - cell.GetHealthLevel();
        int index = (int)Mathf.Ceil(breakingProgress * breakingTiles.Length) - 1;
        return breakingTiles[index];
    }
}
