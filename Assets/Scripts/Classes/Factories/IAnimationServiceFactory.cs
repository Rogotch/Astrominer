using UnityEngine;

public interface IAnimationServiceFactory
{
    IAnimationService Create(Grid grid, Transform character);
}

public class AnimationServiceFactory : IAnimationServiceFactory
{
    public IAnimationService Create(Grid grid, Transform character)
    {
        return new TweenAnimationService(grid, character);
    }
}
