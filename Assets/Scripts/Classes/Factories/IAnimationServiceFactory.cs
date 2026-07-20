using UnityEngine;

public interface IAnimationServiceFactory
{
    IAnimationService Create(Grid grid, Transform character);
}

public class AnimationServiceFactory : IAnimationServiceFactory
{
    private readonly TweenAnimationConfig config;
    public AnimationServiceFactory(MainConfig mainConfig)
    {
        config = mainConfig.tweenAnimation;
    }
    public IAnimationService Create(Grid grid, Transform character)
    {
        return new TweenAnimationService(grid, character, config);
    }
}
