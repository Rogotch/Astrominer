using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerController      playerPrefab; 
    [SerializeField] private LevelStructure        structure; 
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IAnimationServiceFactory, AnimationServiceFactory >(Lifetime.Singleton);
        builder.Register<IMovementServiceFactory,  MovementServiceFactory  >(Lifetime.Singleton);
        builder.Register<IPlayerInputService,      PCPlayerInputService    >(Lifetime.Singleton);
        builder.Register<IEquipmentService,        EquipmentService        >(Lifetime.Singleton);
        builder.Register<IDigToolFactory,          DigToolFactory          >(Lifetime.Singleton);
        builder.RegisterFactory<IResourcePicker>(container => 
            {
                IResourcesSystem dependency = container.Resolve<IResourcesSystem>();
                return () => new AutoResourcePicker(dependency);
            }, Lifetime.Singleton);
        builder.RegisterComponentInNewPrefab<PlayerController>(playerPrefab, Lifetime.Singleton)
                .UnderTransform(structure.characters);

        builder.RegisterEntryPoint<PlayerEntryPoint>();
    }
}
