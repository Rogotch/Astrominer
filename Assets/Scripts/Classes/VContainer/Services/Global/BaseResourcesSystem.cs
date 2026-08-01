using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BaseResourcesSystem : IResourcesSystem
{
    public event Action<Item> ItemGained;
    public event Action<Item> ResourceChanged;
    private Dictionary<string, Item> items = new Dictionary<string, Item>();

    public void AddResourceToStorage(Item item)
    {
        ItemGained.Invoke(item);
        ChangeResourceValue(item.resourceData, item.count);
        ResourceChanged.Invoke(GetResourceObject(item.resourceData));
    }

    public void AddResourcesBagToStorage(Dictionary<string, Item> bag)
    {
        foreach(string tag in bag.Keys)
        {
            AddResourceToStorage(bag[tag]);
        }
    }

    public void ChangeResourceValue(BlocksResource resource, int count)
    {
        Item changingItem = GetResourceObject(resource);
        changingItem.count += count;
    }

    public Item GetResourceObject(Item item)
    {
        return GetResourceObject(item.resourceData);
    }

    public Item GetResourceObject(BlocksResource resource)
    {
        if (!items.ContainsKey(resource.tag))
             items.Add(resource.tag, new Item(resource, 0));
        return items[resource.tag];
    }

    public bool IsCanSpendResource(Item item)
    {
        Item changingItem = GetResourceObject(item.resourceData);
        return changingItem.count >= item.count;
    }

    public void SpendResource(Item item)
    {
        ChangeResourceValue(item.resourceData, -item.count);
        ResourceChanged.Invoke(GetResourceObject(item.resourceData));
    }

}
