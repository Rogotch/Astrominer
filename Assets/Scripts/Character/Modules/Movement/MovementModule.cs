using System;
using System.Collections;
using Unity.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class Movement : BaseModule
{
    #region Сигналы
    public event Action<Vector2Int, Vector2Int> MovingStarted;
    public event Action<Vector2Int, Vector2Int> MovingEnded;
    public event Action<Vector2Int> OnPosition;
    #endregion

    #region Переменные инспектора
    [SerializeField]
    private float timeForStep = 1f;
    [SerializeField]
    private Grid  grid;
    [SerializeField]
    private Ease  ease = Ease.InOutQuad;
    #endregion

    #region Private-переменные
    //private Rigidbody2D rigid_body;
    //private Func<Vector2Int, bool> isCanMove = (target) => true;
    #endregion

    #region Protected-переменные
    protected Tween      currentTween;
    protected Vector2Int startGridPosition;
    protected Vector2Int targetGridPosition;
    protected Vector3    targetPosition;
    #endregion

    #region Public-переменные
    [SerializeField]
    [ReadOnly]
    public bool isMoving = false;
    #endregion

    private void Start()
    {

    }

    private void FixedUpdate()
    {
    }

    public void MoveTo(Vector2Int from, Vector2Int target)
    {
        StartMoving(from, target);
    }

    //public void SetMovementCheckFunction(Func<Vector2Int, bool> checkFunction)
    //{
    //    if (checkFunction != null)
    //    {
    //        isCanMove = checkFunction;
    //    }
    //}

    public bool IsCanMove(Vector2Int target)
    {
        return CellsSystem.IsCellEmpty(target);
    }

    protected void StartMoving(Vector2Int from, Vector2Int target)
    {
        targetPosition = grid.CellToLocal(new Vector3Int(target.x, target.y, 0)) + grid.cellSize / 2;
        targetPosition.z = transform.position.z;
        isMoving = true;

        startGridPosition  = from;
        targetGridPosition = target;
        MovingStarted?.Invoke(from, target);
        currentTween = transform
            .DOMove(targetPosition, timeForStep)
            .SetEase(ease)
            .OnComplete(EndMoving);
        currentTween.Play();
        //currentTween.onComplete += EndMoving;
        //character_animator.SetBool("is_moving", isMoving);
    }
    protected void EndMoving()
    {
        isMoving = false;

        currentTween.Kill();
        currentTween = null;

        OnPosition?.Invoke(targetGridPosition);
        MovingEnded?.Invoke(startGridPosition, targetGridPosition);
        //character_animator.SetBool("is_moving", isMoving);
    }

    protected void SetOnPosition(Vector2Int position)
    {
        targetPosition = grid.CellToLocal(new Vector3Int(position.x, position.y, 0)) + grid.cellSize / 2;
        targetPosition.z = transform.position.z;

        isMoving = false;
        transform.position = targetPosition;
        OnPosition?.Invoke(targetGridPosition);
    }

}
