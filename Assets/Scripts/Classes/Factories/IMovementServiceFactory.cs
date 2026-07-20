using UnityEngine;

public interface IMovementServiceFactory
{
    IMovementService Create(Grid grid, Transform character);
}

public class MovementServiceFactory : IMovementServiceFactory
{
    private readonly TweenMoveConfig config;
    public MovementServiceFactory(MainConfig mainConfig)
    {
        config = mainConfig.tweenMovement;
    }
    public IMovementService Create(Grid grid, Transform character)
    {
        return new TweenMovement(grid, character, config);
    }
}
