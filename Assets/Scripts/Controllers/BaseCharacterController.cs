using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

public class BaseCharacterController : MonoBehaviour, IStartable, IDisposable
{
    //#region Private variables
    ////private Rigidbody   rigid_body;
    ////private Vector2Int  old_position;
    //#endregion


    #region Public variables
    public Vector2Int        gridPosition;
    public CharacterState    currentState;
    // public DiggingInstrument diggingTool;
    #endregion

    #region Injections
    [Inject] private IEquipmentService        _equipment;
    [Inject] private IDigToolFactory          _digToolFactory;
    [Inject] private IAnimationServiceFactory _animFactory;
    [Inject] private IMovementServiceFactory  _moveFactory;
    [Inject] private Grid                     _worldGrid;
    [Inject] private ICellsService            _cellsSystem;

    public IEquipmentService         Equipment      => _equipment;
    public IDigToolFactory           DigToolFactory => _digToolFactory;
    public IAnimationServiceFactory  AnimFactory    => _animFactory;
    public IMovementServiceFactory   MoveFactory    => _moveFactory;
    public Grid                      WorldGrid      => _worldGrid;
    public ICellsService             CellsSystem    => _cellsSystem;
    #endregion

    #region Protected variables
    protected IAnimationService animationService;
    protected IMovementService  moveService;
    #endregion

    #region Signals
    public event Action<Vector2Int, Vector2Int> MovingStarted;
    public event Action<Vector2Int, Vector2Int> MovingEnded;
    public event Action<Vector2Int>             OnPosition;
    #endregion
    public Func<IMovementService> getMovementService;
    
    public virtual void Start()
    {
        animationService   = AnimFactory.Create(WorldGrid, transform);
        moveService        = MoveFactory.Create(WorldGrid, transform);
        getMovementService =  () => moveService;
    }
    public virtual void Dispose() { }
    
    // public virtual void ConnectDiggingTool()
    // {
    //     diggingTool = GetComponent<DiggingInstrument>();
    // }
    public virtual void DisonnectDiggingTool()
    {

    }

    public void ChangeStateTo(CharacterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public virtual void ChangeState(CharacterState.STATES state){}
    public virtual void CheckSteppedCell(Vector2Int cell_from, Vector2Int cell_to){}

    public virtual void SetCharacterOnCell(Vector2Int position)
    {
        transform.position = WorldGrid.CellToLocal(new Vector3Int(position.x, position.y)) + WorldGrid.cellSize / 2;
        gridPosition = position;
    }

}
