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

    //#region Protected variables
    //#endregion

    #region Public variables
    public Vector2Int        gridPosition;
    public CharacterState    currentState;
    public Movement          movementModule;
    // public DiggingInstrument diggingTool;
    #endregion

    #region Injections
    [Inject] private IEquipmentService        _equipment;
    [Inject] private IDigToolFactory          _digToolFactory;
    [Inject] private IAnimationServiceFactory _animFactory;
    [Inject] private Grid                     _worldGrid;
    #endregion

    public IEquipmentService         Equipment      => _equipment;
    public IDigToolFactory           DigToolFactory => _digToolFactory;
    public IAnimationServiceFactory  AnimFactory    => _animFactory;
    public Grid                      WorldGrid      => _worldGrid;

    #region Signals
    public event Action<Vector2Int, Vector2Int> MovingStarted;
    public event Action<Vector2Int, Vector2Int> MovingEnded;
    public event Action<Vector2Int>             OnPosition;
    #endregion
    
    public virtual void Start() { }
    public virtual void Dispose() { }

    public virtual void ConnectMovement()
    {
        movementModule = GetComponent<Movement>();
        if (movementModule != null)
        {
            movementModule.MovingEnded += CheckSteppedCell;
        }
    }
    public virtual void DisonnectMovement()
    {
        if (movementModule != null)
        {
            movementModule.MovingEnded -= CheckSteppedCell;
        }
    }
    
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

}
