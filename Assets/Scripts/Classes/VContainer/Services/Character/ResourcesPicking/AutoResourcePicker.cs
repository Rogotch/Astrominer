using UnityEngine;
using VContainer;

public class AutoResourcePicker : IResourcePicker
{
    private readonly IResourcesSystem resourcesSystem;
    public AutoResourcePicker(IResourcesSystem resourcesSystem)
    {
        this.resourcesSystem = resourcesSystem;
    }
    public void OnShipUnloadingZone() {}

    public void ResourcePickup(Item item)
    {
        resourcesSystem.AddResourceToStorage(item);
    }
}
