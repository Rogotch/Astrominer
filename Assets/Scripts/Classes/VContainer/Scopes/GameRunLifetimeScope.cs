using VContainer;
using VContainer.Unity;

public class GameRunLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IResourcesSystem, BaseResourcesSystem>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameRunEntryPoint>(Lifetime.Singleton);
    }
}
