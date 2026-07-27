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
        Debug.Log("Gameplay Scope");

        builder.RegisterInstance(asteroidConfig);
        builder.RegisterComponent(structure);
        builder.RegisterComponent(itemPrefab);
        builder.RegisterComponent(grid);

        builder.Register<ICellsService, RectangleCellsService>  (Lifetime.Scoped);
        builder.RegisterComponent(gridSystem);
        Debug.Log($"structure characters {structure.characters != null}");
        builder.RegisterComponentInNewPrefab<BaseCameraController>(sceneCamera, Lifetime.Scoped);
        builder.RegisterComponentInNewPrefab<PlayerController>(playerPrefab, Lifetime.Scoped)
                .UnderTransform(structure.characters);


        builder.Register<IPlayerInputService, PCPlayerInputService> (Lifetime.Scoped);
        builder.Register<IEquipmentService,   EquipmentService>     (Lifetime.Scoped);

        builder.Register<IDigToolFactory, DigToolFactory>(Lifetime.Scoped);
        builder.Register<IPickableObjectsFactory,  PickableOreFactory>(Lifetime.Scoped);
        builder.Register<IAnimationServiceFactory, AnimationServiceFactory>(Lifetime.Scoped);
        builder.Register<IMovementServiceFactory,  MovementServiceFactory> (Lifetime.Scoped);
        builder.Register<PlayerLifetimeScope>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<LevelEntryPoint>();
    }
}