using UnityEngine;
using VContainer;

public interface IPickableObjectsFactory
{
    public ItemObject Create(Vector2Int position, Item item);
}

public abstract class BasePickableObjectsFactory
{
    protected readonly IObjectResolver resolver;
    protected readonly ICellsService   cellsService;
    protected readonly Transform       parent;

    protected BasePickableObjectsFactory(ICellsService cellsService, IObjectResolver resolver, LevelStructure structure)
    {
        this.cellsService = cellsService;
        this.resolver = resolver;
        parent = structure.items;
    }
}