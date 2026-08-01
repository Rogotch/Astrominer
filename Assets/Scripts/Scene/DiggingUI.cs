using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class DiggingUI : MonoBehaviour
{
    [Inject] private IResourceCounterFactory counterFactory;
    [Inject] private MainConfig              mainConfig;
    [Inject] private IResourcesSystem        resourcesSystem;
    [SerializeField] private Transform countersObject;
    private GameplayInterfaceConfig InterfaceConfig => mainConfig.gameplayInterface;
    private Dictionary<String, ResourcesCounter> counters = new Dictionary<string, ResourcesCounter>();

    public void Start()
    {
        InitCounters();
        resourcesSystem.ResourceChanged += UpdateResourceCounter;
    }
    public void OnDestroy()
    {
        resourcesSystem.ResourceChanged -= UpdateResourceCounter;
    }
    // {
    // }
    public void InitCounters()
    {
        foreach(BlocksResource resource in InterfaceConfig.DisplayedResources)
        {
            counters.Add(resource.tag, counterFactory.Create(resource, countersObject));
        }
    }
    public void UpdateResourceCounter(Item resource)
    {
        if (!counters.ContainsKey(resource.resourceData.tag)) return;
        counters[resource.resourceData.tag].SetCounter(resource.count);
    }
}
