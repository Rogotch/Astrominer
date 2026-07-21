using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private Grid           grid; 
    [SerializeField] private AsteroidConfig asteroidConfig; 
    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("Gameplay Scope");

        builder.RegisterInstance(asteroidConfig);
        builder.RegisterComponent(grid);

        builder.Register<PlayerLifetimeScope>(Lifetime.Scoped);
        builder.Register<IAnimationServiceFactory, AnimationServiceFactory>(Lifetime.Singleton);
        builder.Register<IMovementServiceFactory,  MovementServiceFactory> (Lifetime.Singleton);
        builder.Register<ICellsService,            RectangleCellsService>  (Lifetime.Singleton);
        
        builder.RegisterEntryPoint<LevelEntryPoint>();
    }
}
