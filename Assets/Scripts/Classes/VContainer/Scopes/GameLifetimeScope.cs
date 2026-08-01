using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private MainConfig mainConfig;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ScenesChanger>(Lifetime.Scoped);
        builder.RegisterInstance(mainConfig);

        builder.Register<GameplayLifetimeScope>(Lifetime.Scoped);
        builder.RegisterEntryPoint<GamePresenter>();
    }
}