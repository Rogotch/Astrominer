using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IResourcesSystem
{
    abstract event Action<Item> ItemGained;
    abstract event Action<Item> ResourceChanged;
    abstract void AddResourceToStorage     (Item item);
    abstract void AddResourcesBagToStorage (Dictionary<string, Item> bag);
    abstract Item GetResourceObject        (Item item);
    abstract void SpendResource            (Item item);
    abstract bool IsCanSpendResource       (Item item);
    abstract void ChangeResourceValue      (BlocksResource resource, int count);
}
