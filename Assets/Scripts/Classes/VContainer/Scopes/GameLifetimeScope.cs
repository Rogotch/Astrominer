using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("Game Scope");
        builder.RegisterEntryPoint<GamePresenter>();

        builder.Register<GameplayLifetimeScope>(Lifetime.Scoped);
    }
}