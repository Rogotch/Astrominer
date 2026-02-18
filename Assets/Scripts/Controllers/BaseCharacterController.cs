using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BaseCharacterController : MonoBehaviour
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
    public DiggingInstrument diggingTool;
    #endregion

    #region Signals
    public event Action<Vector2Int, Vector2Int> MovingStarted;
    public event Action<Vector2Int, Vector2Int> MovingEnded;
    public event Action<Vector2Int> OnPosition;
    #endregion

    protected virtual void Awake()
    {
        //rigid_body = GetComponent<Rigidbody>();
        ConnectMovement();
    }

    protected virtual void OnDestroy()
    {
        DisonnectMovement();
    }

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
    
    public virtual void ConnectDiggingTool()
    {
        diggingTool = GetComponent<DiggingInstrument>();
    }
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
