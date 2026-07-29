using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICellsService
{
    #region Actions
    public abstract event Action<Vector2Int, float>   CellDamaged;
    public abstract event Action<Vector2Int>          CellDestroyed;
    public abstract event Action<Vector2Int, Item>    ResourceDropped;
    public abstract event Action<Vector2Int>          ResourcePicked;
    #endregion

    public abstract Dictionary<Vector2Int, Cell> CellsMap         {get;}
    public abstract Dictionary<Vector2Int, Cell> BackgroundCells  {get;}
    public abstract Dictionary<Vector2Int, Item> ResourcesCells   {get;}

    public abstract Vector2Int GetCellNeighboursVector (Vector2Int position);
    public abstract Cell       GetCell                 (Vector2Int position);
    public abstract bool       IsCellEmpty             (Vector2Int cell);
    public abstract bool       IsHasResource           (Vector2Int cell);
    public abstract void       DamageCell              (Vector2Int cell, float damage);
    public abstract void       CheckCell               (Vector2Int cell);
    public abstract void       DestroyCell             (Vector2Int cell);
    public abstract void       DropResource            (Vector2Int on_cell, Item resource);
    public abstract void       PickupResource          (Vector2Int from_cell);
    public abstract void       LogAllResources         ();
    public abstract Vector3    GetCellWorldPosition    (Vector2Int from_cell);
    public abstract Vector2Int GetCellMapPosition      (Vector3    cell);
}

public abstract class BaseCellsService : ICellsService
{
    protected Dictionary<Vector2Int, Cell> cellsMap        = new Dictionary<Vector2Int, Cell>();       
    protected Dictionary<Vector2Int, Cell> backgroundCells = new Dictionary<Vector2Int, Cell>();
    protected Dictionary<Vector2Int, Item> resourcesCells  = new Dictionary<Vector2Int, Item>(); 

    public abstract  Dictionary<Vector2Int, Cell> CellsMap          { get;}
    public abstract  Dictionary<Vector2Int, Cell> BackgroundCells   { get;}
    public abstract  Dictionary<Vector2Int, Item> ResourcesCells    { get;}

    protected readonly Grid             grid;

    #region Actions
    public abstract event Action<Vector2Int, float>   CellDamaged;
    public abstract event Action<Vector2Int>          CellDestroyed;
    public abstract event Action<Vector2Int, Item>    ResourceDropped;
    public abstract event Action<Vector2Int>          ResourcePicked;
    #endregion

    protected BaseCellsService(Grid grid) {this.grid = grid; }

    public abstract Vector2Int GetCellNeighboursVector (Vector2Int position);
    public abstract Cell       GetCell                 (Vector2Int position);
    public abstract bool       IsCellEmpty             (Vector2Int cell);
    public abstract bool       IsHasResource           (Vector2Int cell);
    public abstract void       DamageCell              (Vector2Int cell, float damage);
    public abstract void       CheckCell               (Vector2Int cell);
    public abstract void       DestroyCell             (Vector2Int cell);
    public abstract void       DropResource            (Vector2Int on_cell, Item resource);
    public abstract void       PickupResource          (Vector2Int from_cell);
    public abstract Vector3    GetCellWorldPosition    (Vector2Int from_cell);
    public abstract Vector2Int GetCellMapPosition      (Vector3    cell);
    public abstract void       LogAllResources         ();
}