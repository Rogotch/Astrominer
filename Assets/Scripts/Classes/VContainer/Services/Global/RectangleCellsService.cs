using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RectangleCellsService : BaseCellsService, ICellsService
{
    public RectangleCellsService(Grid grid) : base(grid) { }

    public override Dictionary<Vector2Int, Cell> CellsMap
        { get { if (cellsMap == null)        cellsMap        = new Dictionary<Vector2Int, Cell>(); return cellsMap; } }
    public override Dictionary<Vector2Int, Cell> BackgroundCells
        { get { if (backgroundCells == null) backgroundCells = new Dictionary<Vector2Int, Cell>(); return backgroundCells; } }
    public override Dictionary<Vector2Int, Item> ResourcesCells
        { get { if (resourcesCells == null)  resourcesCells  = new Dictionary<Vector2Int, Item>(); return resourcesCells; } }

    #region Actions
    public override event Action<Vector2Int, float>   CellDamaged;
    public override event Action<Vector2Int>          CellDestroyed;
    public override event Action<Vector2Int, Item>    ResourceDropped;
    public override event Action<Vector2Int>          ResourcePicked;
    #endregion

    public override Vector2Int GetCellNeighboursVector(Vector2Int position)
    {
        bool has_up    = cellsMap.ContainsKey(position + Vector2Int.up);
        bool has_right = cellsMap.ContainsKey(position + Vector2Int.right);
        
        return new Vector2Int(
            (has_right ? 1 : 0), //X
            (has_up    ? 1 : 0)  //Y
            );
    }

    public override Cell GetCell(Vector2Int position)
    {
        if (IsCellEmpty(position)) return null;
        return cellsMap[position];
    }

    public override bool IsCellEmpty(Vector2Int cell)
    {
        return !cellsMap.ContainsKey(cell);
    }

    public override void DamageCell(Vector2Int cell, float damage)
    {
        if (IsCellEmpty(cell))
            return;

        cellsMap[cell].health -= damage;
        CellDamaged?.Invoke(cell, damage);
        CheckCell(cell);
    }

    public override void CheckCell(Vector2Int cell)
    {
        if (IsCellEmpty(cell))
            return;

        if (cellsMap[cell].health <= 0)
            DestroyCell(cell);
    }

    public override void DestroyCell(Vector2Int cell)
    {
        // if (IsCellEmpty(cell))
        //     return;
        
        if (cellsMap[cell].cell_resource != null)
        {
            BlocksResource resource = cellsMap[cell].cell_resource;
            Item new_resource = new Item(resource);
            DropResource(cell, new_resource);
        }
        string result = string.Join(", ", resourcesCells.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
        cellsMap.Remove(cell);
        CellDestroyed?.Invoke(cell);
    }


    public override void DropResource(Vector2Int on_cell, Item resource)
    {
        resourcesCells.Add(on_cell, resource);
        ResourceDropped?.Invoke(on_cell, resource);
        Debug.Log("Resource dropped");
    }

    public override void PickupResource(Vector2Int from_cell)
    {
        Debug.Log($"resource picked from cell {from_cell}");
        resourcesCells.Remove(from_cell);
        ResourcePicked?.Invoke(from_cell);
    }

    public override Vector3 GetCellWorldPosition(Vector2Int from_cell)
    {
        Vector3 final_position = grid.CellToLocal(new Vector3Int(from_cell.x, from_cell.y)) + grid.cellSize / 2;
        // Vector3 rawPosition = grid.CellToWorld(new Vector3Int(from_cell.x, 0, from_cell.y));
        // final_position.z = grid.transform.position.z;
        return final_position;
    }

    public override Vector2Int GetCellMapPosition(Vector3 cell)
    {
        Vector3Int position = grid.LocalToCell(cell);
        return new Vector2Int(position.x, position.z);
    }
}
