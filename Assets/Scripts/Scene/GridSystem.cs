using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GridSystem : MonoBehaviour
{
    #region Inspector's variables
    [Header("Размеры карты")]
    [SerializeField] private int               width                 = 200;
    [SerializeField] private int               height                = 200;
    [Header("Параметры астероида")]
    [SerializeField] private Vector2Int        center                = new Vector2Int(100, 100);
    [SerializeField] private int               radius                = 60;
    [SerializeField] private CellsDataLayer    asteroid_layer;
    [SerializeField] private CellsDataLayer[]  layers;
    [Range(0f, 1f), SerializeField]
                     private float             surfaceNoiseStrength  = 0.3f;
    [Header("Ссылки")]
    [SerializeField] private TilemapLayers     tilemap_main;
    [SerializeField] private TilemapLayers     tilemap_top;
    #endregion

    #region Private variables
    //private Grid    grid;
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
        ConnectPlayer(Game.Player);
    }

    private void OnDestroy()
    {
        DisconnectCellsMap();
        DisconnectPlayer(Game.Player);
    }

    #region Connections
    private void ConnectPlayer(PlayerController player)
    {
        player.MovingStarted += StartPlayerMove;
        player.MovingEnded   += EndPlayerMove;
    }
    private void ConnectCellsMap()
    {
        Game.CellDamaged     += CellDamaged;
        Game.CellDestroyed   += CellDestroyed;
    }
    private void DisconnectPlayer(PlayerController player)
    {
        player.MovingStarted -= StartPlayerMove;
        player.MovingEnded   -= EndPlayerMove;
    }
    private void DisconnectCellsMap()
    {
        Game.CellDamaged     -= CellDamaged;
        Game.CellDestroyed   -= CellDestroyed;
    }
    #endregion

    [ContextMenu("Clear tiles")]
    public void ClearTiles()
    {
        if (tilemap_main   != null) tilemap_main.ClearAllTiles();
        if (tilemap_top    != null) tilemap_top.ClearAllTiles();
    }

    private bool IsInsideAsteroid(Vector2Int position)
    {
        float dist_to_center = Vector2Int.Distance(position, center);
        float noise_value    = asteroid_layer.noise.GenerateNoise(position);
        float final_radius   = radius + noise_value * surfaceNoiseStrength;

        return dist_to_center <= final_radius;
    }

    [ContextMenu("Generate Cave")]
    public void GenerateCave()
    {
        Game.CellsMap.Clear();
        ClearTiles();

        Dictionary<Vector2Int, Cell> asteroid_cells = new Dictionary<Vector2Int, Cell>();

        if (!asteroid_layer.additional)
             asteroid_layer.additional = true;

        GenerateLayer(asteroid_layer, ref asteroid_cells);

        //Цикл перебирает все слои и записывает один поверх другого
        foreach (CellsDataLayer layer in layers)
            GenerateLayer(layer, ref asteroid_cells);

        foreach (Vector2Int position in asteroid_cells.Keys)
            Game.CellsMap[position] = asteroid_cells[position];

        DrawMap();
    }

    //NoiseDataLayer (и его наследники CellsDataLayer и OresDataLayer) - представляет из себя набор параметров для генерации шума PerlinNoise
    //GenerateLayer генерирует словарь <Vector2Int, Cell> клеток согласно настройкам шума и сразу же заполняет его возможными ресурсами
    private void GenerateLayer(CellsDataLayer layer, ref Dictionary<Vector2Int, Cell> asteroid_cells)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (!IsInsideAsteroid(position) ||                                // Если клетка вне радиуса астероида
                    !(asteroid_cells.ContainsKey(position) || layer.additional))  // Или если нет проверяемой позиции, а сам слой не добавочный
                    continue;                                                     // То переходим к следуюшей позиции

                //Внутри цикла проверяется, принимать ли значение шума за наличие клетки
                if (!layer.noise.NoiseCellCheck(x, y)) continue;
                Cell cell = new Cell(layer.cell_data);
                BlocksResource  final_resource = null;

                //Дальше соответствующая проверка на наличие ячейки проходят шумы с параметрами из OresDataLayer и если да, в ячейку записывается последняя подходящая руда. Если подходят несколько руд, то результирующей будет самая нижняя в списке
                foreach (OresDataLayer ore_layer in layer.ores_data)
                {
                    if (!ore_layer.noise.NoiseCellCheck(x, y)) continue;
                    final_resource = ore_layer.resource_params;
                }

                if (final_resource != null)
                {
                    cell.cell_resource = final_resource;
                }

                asteroid_cells[new Vector2Int(x, y)] = cell;
            }
        }
    }

    private void DrawMap()
    {
        tilemap_main.SetMultipleCells(Game.CellsMap);
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
        tilemap_main.SetDamageToCell(tile, Game.GetCell(tile));
    }

    private void CellDestroyed(Vector2Int tile)
    {
        tilemap_main.BreakTile(tile);
    }
}
