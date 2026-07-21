using UnityEngine;

public interface IMovementServiceFactory
{
    IMovementService Create(Grid grid, Transform character);
}

public class MovementServiceFactory : IMovementServiceFactory
{
    private readonly TweenMoveConfig config;
    private readonly ICellsService   cellsService;
    public MovementServiceFactory(MainConfig mainConfig, ICellsService cellsService)
    {
        config = mainConfig.tweenMovement;
        this.cellsService = cellsService;
    }
    public IMovementService Create(Grid grid, Transform character)
    {
        return new TweenMovement(grid, character, config, cellsService);
    }
}
