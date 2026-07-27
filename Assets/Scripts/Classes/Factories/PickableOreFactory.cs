using Unity.Mathematics;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PickableOreFactory : BasePickableObjectsFactory, IPickableObjectsFactory
{
    private readonly ItemObject prefab;
    public PickableOreFactory(ICellsService cellsService, IObjectResolver resolver, LevelStructure structure, ItemObject prefab) : base(cellsService, resolver, structure)
    {
        this.prefab = prefab;
    }

    public ItemObject Create(Vector2Int position, Item item)
    {
        ItemObject ore = resolver.Instantiate(prefab, cellsService.GetCellWorldPosition(position), Quaternion.identity, parent);
        ore.SetData(item);
        ore.gridPosition = position;
        return ore;
    }
}
