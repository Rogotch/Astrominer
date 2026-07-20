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
        builder.RegisterEntryPoint<GamePresenter>();
        builder.RegisterInstance(mainConfig);

        builder.Register<GameplayLifetimeScope>(Lifetime.Scoped);
    }
}