using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class CellsSystem
{
    //private static GlobalData _data;
    //private static SaveData   _saveData;
    private static Dictionary<Vector2Int, Cell> cells_map;
    private static Dictionary<Vector2Int, Cell> background_cells;
    private static Dictionary<Vector2Int, Item> resources_cells;

    //public static GlobalData Data
    //    { get { if (_data == null) _data = Resources.Load<GlobalData>("Resources/Globals/BaseData.asset"); return _data; } }
    //public static SaveData   SaveData;
    public static Dictionary<Vector2Int, Cell> CellsMap
        { get { if (cells_map        == null) cells_map        = new Dictionary<Vector2Int, Cell>(); return cells_map; } }
    public static Dictionary<Vector2Int, Cell> BackgroundCells
        { get { if (background_cells == null) background_cells = new Dictionary<Vector2Int, Cell>(); return background_cells; } }
    public static Dictionary<Vector2Int, Item> ResourcesCells
        { get { if (resources_cells == null)  resources_cells  = new Dictionary<Vector2Int, Item>(); return resources_cells; } }

    public static PlayerController Player;

    #region —игналы
    public static event Action<Vector2Int, float>   CellDamaged;
    public static event Action<Vector2Int>          CellDestroyed;
    public static event Action<Vector2Int, Item>    ResourceDropped;
    public static event Action<Vector2Int>          ResourcePicked;
    #endregion

    public static Vector2Int GetCellNeighboursVector(Vector2Int position)
    {
        bool has_up    = cells_map.ContainsKey(position + Vector2Int.up);
        bool has_right = cells_map.ContainsKey(position + Vector2Int.right);
        
        return new Vector2Int(
            (has_right ? 1 : 0), //X
            (has_up    ? 1 : 0)  //Y
            );
    }

    public static Cell GetCell(Vector2Int position)
    {
        if (IsCellEmpty(position)) return null;
        return cells_map[position];
    }

    public static bool IsCellEmpty(Vector2Int cell)
    {
        return !CellsMap.ContainsKey(cell);
    }

    public static void DamageCell(Vector2Int cell, float damage)
    {
        if (IsCellEmpty(cell))
            return;

        cells_map[cell].health -= damage;
        CellDamaged?.Invoke(cell, damage);
        CheckCell(cell);
    }

    public static void CheckCell(Vector2Int cell)
    {
        if (IsCellEmpty(cell))
            return;

        if (cells_map[cell].health <= 0)
            DestroyCell(cell);
    }

    public static void DestroyCell(Vector2Int cell)
    {
        if (IsCellEmpty(cell))
            return;

        //—юда добавить выпадение ресурсов
        if (cells_map[cell].cell_resource != null)
        {
            BlocksResource resource = cells_map[cell].cell_resource;
            Item new_resource = new Item(resource);
            DropResource(cell, new_resource);
        }
        cells_map.Remove(cell);
        CellDestroyed?.Invoke(cell);
    }


    public static void DropResource(Vector2Int on_cell, Item resource)
    {
        ResourcesCells[on_cell] = resource;
        ResourceDropped?.Invoke(on_cell, resource);
    }

    public static void PickupResource(Vector2Int from_cell)
    {
        Debug.Log($"resource picked from cell {from_cell}");
        ResourcesCells.Remove(from_cell);
        ResourcePicked?.Invoke(from_cell);
    }
}
