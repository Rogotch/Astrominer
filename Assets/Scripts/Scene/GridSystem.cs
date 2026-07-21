using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GridSystem : MonoBehaviour
{
    #region Injections
    [Inject] private AsteroidConfig       asteroidConfig;
    [Inject] private ICellsService        cellsSystem;
    #endregion

    #region Inspector's variables
    [Header("Asteroid Size")]
    [SerializeField] private int                   width                 = 200;
    [SerializeField] private int                   height                = 200;
    [Header("Tilemaps")]
    [SerializeField] private TilemapLayers         tilemap_background;
    [SerializeField] private TilemapLayers         tilemap_main;
    [SerializeField] private TilemapLayers         tilemap_top;
    [SerializeField] private GameObject            resources;
    #endregion

    #region Private variables
    private Grid    grid;
    #endregion

    private void Start()
    {
        GenerateCave();
        ConnectCellsMap();
        grid = GetComponent<Grid>();
    }

    private void OnDestroy()
    {
        DisconnectCellsMap();
    }

    #region Connections
    private void ConnectCellsMap()
    {
        cellsSystem.CellDamaged     += CellDamaged;
        cellsSystem.CellDestroyed   += CellDestroyed;
        cellsSystem.ResourceDropped += ResourceDropped;
    }
        private void DisconnectCellsMap()
    {
        cellsSystem.CellDamaged     -= CellDamaged;
        cellsSystem.CellDestroyed   -= CellDestroyed;
        cellsSystem.ResourceDropped -= ResourceDropped;
    }
    #endregion

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
        cellsSystem.BackgroundCells.Clear();
        cellsSystem.CellsMap.Clear();

        ClearTiles();

        Dictionary<Vector2Int, Cell>   asteroid_cells = new Dictionary<Vector2Int, Cell>();
        Dictionary<Vector2Int, Cell> background_cells = new Dictionary<Vector2Int, Cell>();

        GenerateLayer(
            asteroidConfig.asteroidData.layerData,
            asteroidConfig.asteroidData,
            CheckCellAsteroid,
            ref asteroid_cells);

        GenerateLayer(
            asteroidConfig.asteroidDataBackground.layerData,
            asteroidConfig.asteroidDataBackground,
            CheckCellAsteroid,
            ref background_cells);

        foreach (CellsDataLayer layer in asteroidConfig.cellsDataLayers)
            GenerateLayer(
                layer,
                asteroidConfig.asteroidData,
                CheckCellLayer,
                ref asteroid_cells);

        foreach (Vector2Int position in asteroid_cells.Keys)
            cellsSystem.CellsMap[position]        =   asteroid_cells[position];

        foreach (Vector2Int position in background_cells.Keys)
            cellsSystem.BackgroundCells[position] = background_cells[position];

        DrawMap();
    }

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
        return (checked_params.IsInsideAsteroid(position) &&                
               (cells.ContainsKey(position) || cell_layer.additional));
    }
    
    private bool CheckCellAsteroid(Vector2Int position, AsteroidParameters checked_params, CellsDataLayer cell_layer, Dictionary<Vector2Int, Cell> cells)
    {
        return checked_params.IsInsideAsteroid(position);
    }

    private void DrawMap()
    {
        tilemap_background.SetMultipleCells(cellsSystem.BackgroundCells);
        tilemap_main.SetMultipleCells(cellsSystem.CellsMap);
    }

    private void CellDamaged(Vector2Int tile, float damage)
    {
        tilemap_main.SetDamageToCell(tile, cellsSystem.GetCell(tile));
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
