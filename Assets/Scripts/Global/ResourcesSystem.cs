using System.Collections.Generic;
using UnityEngine;

public static class ResourcesSystem
{
    private static Dictionary<string, Item> collected_resources = new Dictionary<string, Item>();

    public static void CollectResource(Item resource)
    {
        if (!collected_resources.ContainsKey(resource.resourceData.tag))
        {
            collected_resources[resource.resourceData.tag] = new Item(resource.resourceData, 0);
        }
        collected_resources[resource.resourceData.tag].count += resource.count;
    }
}
