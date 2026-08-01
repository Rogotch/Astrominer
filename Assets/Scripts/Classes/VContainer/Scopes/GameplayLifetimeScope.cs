using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private Grid                  grid; 
    [SerializeField] private GridSystem            gridSystem; 
    [SerializeField] private AsteroidConfig        asteroidConfig; 
    [SerializeField] private ItemObject            itemPrefab; 
    [SerializeField] private BaseCameraController  sceneCamera; 
    [SerializeField] private LevelStructure        structure; 
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(asteroidConfig);
        builder.RegisterComponent(structure);
        builder.RegisterComponent(itemPrefab);
        builder.RegisterComponent(grid);

        builder.Register<IResourcesSystem,         BaseResourcesSystem     >(Lifetime.Singleton);
        builder.Register<ICellsService,            RectangleCellsService   >(Lifetime.Singleton);
        builder.RegisterComponent(gridSystem);
        builder.RegisterComponentInNewPrefab<BaseCameraController>(sceneCamera, Lifetime.Singleton);

        builder.Register<IPickableObjectsFactory,  PickableOreFactory>(Lifetime.Singleton);
        builder.Register<PlayerLifetimeScope>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<LevelEntryPoint>();
    }
}