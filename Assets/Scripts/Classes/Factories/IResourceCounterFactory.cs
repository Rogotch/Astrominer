using UnityEngine;
using VContainer;
using VContainer.Unity;

public interface IResourceCounterFactory
{
    public abstract ResourcesCounter Create(BlocksResource resource, Transform parent);
}

public class ResourceCounterFactory : IResourceCounterFactory
{
    protected readonly IObjectResolver  resolver;
    protected readonly ResourcesCounter prefab;

    protected ResourceCounterFactory(IObjectResolver resolver)
    {
        this.resolver = resolver;
        prefab = resolver.Resolve<ResourcesCounter>();
    }

    public ResourcesCounter Create(BlocksResource resource, Transform parent)
    {
        ResourcesCounter counter = resolver.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        counter.SetDataByResource(resource);
        return counter;
    }
}