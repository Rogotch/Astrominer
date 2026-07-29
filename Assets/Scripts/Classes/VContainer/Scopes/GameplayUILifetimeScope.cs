using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayUILifetimeScope : LifetimeScope
{
    [SerializeField] private ResourcesCounter counterPrefab;
    protected override void Configure(IContainerBuilder builder)
    {

        builder.Register<ISceneUIService, GameplayUIService>(Lifetime.Singleton);

        builder.RegisterEntryPoint<GameplayUIEntryPoint>(Lifetime.Singleton);
    }
}
