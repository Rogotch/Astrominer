using System.Collections.Generic;
using UnityEngine;

public interface ISceneUIService
{
    public abstract void ChangeResourceCounter(BlocksResource resource);
}

public abstract class BaseGameplayUIService : ISceneUIService
{
    // public Dictionary<string, ResourcesCounter> Counters;
    public abstract void ChangeResourceCounter(BlocksResource resource);
}