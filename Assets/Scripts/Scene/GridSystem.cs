using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GridSystem : MonoBehaviour
{
    #region Inspector's variables
    [SerializeField] private Vector2Int            start_position        = Vector2Int.zero;
    [Header("Размеры карты")]
    [SerializeField] private int                   width                 = 200;
    [SerializeField] private int                   height                = 200;
    [Header("Параметры астероида")]
    [SerializeField] private AsteroidParameters    asteroid_background;
    [SerializeField] private AsteroidParameters    asteroid_data;
    [SerializeField] private CellsDataLayer[]      layers;
    [Header("Ссылки")]
    [SerializeField] private TilemapLayers         tilemap_background;
    [SerializeField] private TilemapLayers         tilemap_main;
    [SerializeField] private TilemapLayers         tilemap_top;
    [SerializeField] private GameObject            resources;
    #endregion

    #region Private variables
    private Grid    grid;
    #endregion

    #region Public variables
    //public Dictionary<Vector2Int, Cell> cells_map;
    #endregion

    #region Enums
    //enum CellType {Base, Hard, Resource}
    #endregion

    private void Start()
    {
        GenerateCave();
        ConnectCellsMap();
        ConnectPlayer(CellsSystem.Player);
        grid = GetComponent<Grid>();
        SetUpPlayer();
    }

    private void OnDestroy()
    {
        DisconnectCellsMap();
        DisconnectPlayer(CellsSystem.Player);
    }

    #region Connections
    private void ConnectPlayer(PlayerController player)
    {
        player.MovingStarted += StartPlayerMove;
        player.MovingEnded   += EndPlayerMove;
    }
    private void ConnectCellsMap()
    {
        CellsSystem.CellDamaged     += CellDamaged;
        CellsSystem.CellDestroyed   += CellDestroyed;
        CellsSystem.ResourceDropped += ResourceDropped;
    }
    private void DisconnectPlayer(PlayerController player)
    {
        player.MovingStarted -= StartPlayerMove;
        player.MovingEnded   -= EndPlayerMove;
    }
    private void DisconnectCellsMap()
    {
        CellsSystem.CellDamaged     -= CellDamaged;
        CellsSystem.CellDestroyed   -= CellDestroyed;
        CellsSystem.ResourceDropped -= ResourceDropped;
    }
    #endregion

    public void SetUpPlayer()
    {
        CellsSystem.Player.transform.position = grid.CellToLocal(new Vector3Int(start_position.x, start_position.y)) + grid.cellSize / 2;
        CellsSystem.Player.gridPosition = start_position;
    }

    [ContextMenu("Clear tiles")]
    public void ClearTiles()
    {
        if (tilemap_background != null) tilemap_background.ClearAllTiles();
        if (tilemap_main       != null)       tilemap_main.ClearAllTiles();
        if (tilemap_top        != null)        tilemap_top.ClearAllTiles();
    }

    [ContextMenu("Generate Cave")]
    public void GenerateCave()
    {
        Debug.Log("Generate");
        CellsSystem.BackgroundCells.Clear();
        CellsSystem.CellsMap.Clear();

        ClearTiles();

        Dictionary<Vector2Int, Cell>   asteroid_cells = new Dictionary<Vector2Int, Cell>();
        Dictionary<Vector2Int, Cell> background_cells = new Dictionary<Vector2Int, Cell>();

        GenerateLayer(
            asteroid_data.layerData,
            asteroid_data,
            CheckCellAsteroid,
            ref asteroid_cells);

        GenerateLayer(
            asteroid_background.layerData,
            asteroid_background,
            CheckCellAsteroid,
            ref background_cells);

        //Цикл перебирает все слои и записывает один поверх другого
        foreach (CellsDataLayer layer in layers)
            GenerateLayer(
                layer,
                asteroid_data,
                CheckCellLayer,
                ref asteroid_cells);

        foreach (Vector2Int position in asteroid_cells.Keys)
            CellsSystem.CellsMap[position]        =   asteroid_cells[position];

        foreach (Vector2Int position in background_cells.Keys)
            CellsSystem.BackgroundCells[position] = background_cells[position];

        DrawMap();
    }

    //NoiseDataLayer (и его наследники CellsDataLayer и OresDataLayer) - представляет из себя набор параметров для генерации шума PerlinNoise
    //GenerateLayer генерирует словарь <Vector2Int, Cell> клеток согласно настройкам шума и сразу же заполняет его возможными ресурсами
    private void GenerateLayer(
        CellsDataLayer layer,
        AsteroidParameters parameters,
        Func<Vector2Int, AsteroidParameters, CellsDataLayer, Dictionary<Vector2Int, Cell>, bool> check_function,
        ref Dictionary<Vector2Int, Cell> cells)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (!check_function.Invoke(position, parameters, layer, cells))
                    continue;

                GenereateCell(position, layer.noise, layer, ref cells);
            }
        }
    }

    private void GenereateCell(Vector2Int position, INoiseGenerator noise_checker, CellsDataLayer layer, ref Dictionary<Vector2Int, Cell> cells)
    {
        // Проверяем, принимать ли значение шума за наличие клетки
        if (!noise_checker.NoiseCellCheck(position.x, position.y)) return;
        Cell cell = new Cell(layer.cell_data.GetCell());

        if (layer.resource_params != null)
        {
            cell.cell_resource = layer.resource_params;
        }

        cells[position] = cell;
    }

    private bool CheckCellLayer(Vector2Int position, AsteroidParameters checked_params, CellsDataLayer cell_layer, Dictionary<Vector2Int, Cell> cells)
    {
        return (checked_params.IsInsideAsteroid(position) &&                  // Если клетка в радиусe астероида
               (cells.ContainsKey(position) || cell_layer.additional));       // И если есть проверяемая позиция или сам слой добавочный
    }
    
    private bool CheckCellAsteroid(Vector2Int position, AsteroidParameters checked_params, CellsDataLayer cell_layer, Dictionary<Vector2Int, Cell> cells)
    {
        return checked_params.IsInsideAsteroid(position);                     // Если клетка в радиусe астероида
    }

    private void DrawMap()
    {
        tilemap_background.SetMultipleCells(CellsSystem.BackgroundCells);
        tilemap_main.SetMultipleCells(CellsSystem.CellsMap);
    }

    private void StartPlayerMove(Vector2Int from, Vector2Int to)
    {
        //Vector2Int target_displayed_cell = from + Vector2Int.down;
        //Debug.Log($"Move from {from} to {to}, has cell = {Game.CellsMap.ContainsKey(target_displayed_cell)}");
        //if (Game.CellsMap.ContainsKey(target_displayed_cell))
        //{
        //    Debug.Log("StartPlayerMove");
        //}

        //RemoveCellFromTopLayer(from + Vector2Int.down);
    }
    private void EndPlayerMove(Vector2Int from, Vector2Int to)
    {
        //SetCellOnTopLayer(to + Vector2Int.down);
    }
    
    //private void RemoveCellFromTopLayer(Vector2Int position)
    //{
    //    if (Game.CellsMap.ContainsKey(position))
    //    {
    //        tilemap_top.SetCell(position, null);
    //    }
    //}
    //private void SetCellOnTopLayer(Vector2Int position)
    //{
    //    if (Game.CellsMap.ContainsKey(position))
    //    {
    //        tilemap_top.SetCell(position, Game.CellsMap[position]);
    //    }
    //}

    private void CellDamaged(Vector2Int tile, float damage)
    {
        tilemap_main.SetDamageToCell(tile, CellsSystem.GetCell(tile));
    }

    private void CellDestroyed(Vector2Int tile)
    {
        tilemap_main.BreakTile(tile);
    }
    private void ResourceDropped(Vector2Int on_position, Item resource)
    {
        Vector3 final_position = grid.CellToLocal(new Vector3Int(on_position.x, on_position.y)) + grid.cellSize / 2;
        ItemObject.SpawnResource(resource, on_position, final_position, resources.transform);
    }
}
