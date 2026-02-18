using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapLayers : MonoBehaviour
{
    #region
    [SerializeField] private Color modulation = Color.white;
    #endregion

    #region Tilemaps
    [SerializeField] private Tilemap tilemap_blocks;
    [SerializeField] private Tilemap tilemap_resources;
    [SerializeField] private Tilemap break_cells;
    #endregion
    public void OnValidate()
    {
        SetModulation(modulation);
    }
    public void ClearAllTiles()
    {
        tilemap_blocks.ClearAllTiles();
        tilemap_resources.ClearAllTiles();
        break_cells.ClearAllTiles();
    }

    public void BreakTile(Vector2Int position)
    {
        tilemap_blocks.SetTile(new Vector3Int(position.x, position.y, 0), null);
        tilemap_resources.SetTile(new Vector3Int(position.x, position.y, 0), null);
        break_cells.SetTile(new Vector3Int(position.x, position.y, 0), null);
    }

    //Важно - перед вызовом этой функции в Game.CellsMap уже дожны произойти все изменения
    public void SetCell(Vector2Int position, Cell cell)
    {
        if (cell == null)
        {
            tilemap_blocks.SetTile(new Vector3Int(position.x, position.y, 0), null);
            return;
        }
        tilemap_blocks.SetTile(new Vector3Int(position.x, position.y, 0), cell.tile);

        if (cell.cell_resource != null)
        {
            TileBase resource_tile = cell.cell_resource.tile_variants.Dictionary[CellsSystem.GetCellNeighboursVector(position)];
            tilemap_resources.SetTile(new Vector3Int(position.x, position.y, 0), resource_tile);

        }
        if (cell.health != cell.max_health)
        {
            SetDamageToCell(position, cell);
        }
        //tilemap_resources.SetTile(new Vector3Int(position.x, position.y, 0), null);
    }

    public void SetMultipleCells(Vector2Int[] positions, Cell[] cells)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            SetCell(positions[i], cells[i]);
        }
    }
    public void SetMultipleCells(Dictionary<Vector2Int, Cell> cells)
    {
        foreach (Vector2Int position in cells.Keys)
        {
            SetCell(position, cells[position]);
        }
    }

    public void SetDamageToCell(Vector2Int position, Cell cell)
    {
        if (cell == null) return;
    }

    private void SetModulation(Color color)
    {
        tilemap_blocks.color = color;
        tilemap_resources.color = color;
        break_cells.color = color;
    }


}
