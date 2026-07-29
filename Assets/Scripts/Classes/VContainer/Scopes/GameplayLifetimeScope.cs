using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private Grid                  grid; 
    [SerializeField] private GridSystem            gridSystem; 
    [SerializeField] private AsteroidConfig        asteroidConfig; 
    [SerializeField] private ItemObject            itemPrefab; 
    [SerializeField] private PlayerController      playerPrefab; 
    [SerializeField] private BaseCameraController  sceneCamera; 
    [SerializeField] private LevelStructure        structure; 
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(asteroidConfig);
        builder.RegisterComponent(structure);
        builder.RegisterComponent(itemPrefab);
        builder.RegisterComponent(grid);

        builder.Register<ICellsService, RectangleCellsService>  (Lifetime.Scoped);
        builder.RegisterComponent(gridSystem);
        Debug.Log($"structure characters {structure.characters != null}");
        builder.RegisterComponentInNewPrefab<BaseCameraController>(sceneCamera, Lifetime.Scoped);


        builder.Register<IAnimationServiceFactory, AnimationServiceFactory >(Lifetime.Singleton);
        builder.Register<IMovementServiceFactory,  MovementServiceFactory  >(Lifetime.Singleton);
        builder.Register<IPlayerInputService,      PCPlayerInputService    >(Lifetime.Singleton);
        builder.Register<IEquipmentService,        EquipmentService        >(Lifetime.Singleton);
        builder.Register<IDigToolFactory,          DigToolFactory          >(Lifetime.Singleton);
        builder.RegisterComponentInNewPrefab<PlayerController>(playerPrefab, Lifetime.Singleton)
                .UnderTransform(structure.characters);


        builder.Register<IPickableObjectsFactory,  PickableOreFactory>(Lifetime.Singleton);
        builder.Register<PlayerLifetimeScope>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<LevelEntryPoint>();
    }
}