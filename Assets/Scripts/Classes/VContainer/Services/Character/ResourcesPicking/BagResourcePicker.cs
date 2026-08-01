using System.Collections.Generic;
using UnityEngine;

public class BagResourcePicker : IResourcePicker
{
    private readonly IResourcesSystem resourcesSystem;
    public BagResourcePicker(IResourcesSystem resourcesSystem)
    {
        this.resourcesSystem = resourcesSystem;
    }
    private Dictionary<string, Item> pickedResources = new Dictionary<string, Item>();

    public void OnShipUnloadingZone()
    {
        resourcesSystem.AddResourcesBagToStorage(pickedResources);
    }

    public void ResourcePickup(Item item)
    {
        if (!pickedResources.ContainsKey(item.Tag))
             pickedResources.Add(item.Tag, new Item(item.resourceData, 0));
        pickedResources[item.Tag].count += item.count;
    }
}
