using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private Grid grid; 
    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("Gameplay Scope");

        builder.RegisterEntryPoint<LevelEntryPoint>();
        builder.Register<PlayerLifetimeScope>(Lifetime.Scoped);


        builder.RegisterComponent(grid);
    }
}
