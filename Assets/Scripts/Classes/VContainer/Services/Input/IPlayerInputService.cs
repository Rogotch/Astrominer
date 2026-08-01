using System;
using UnityEngine;

public interface IPlayerInputService
{
    event Action<Vector2Int> OnMove;
    event Action<Vector2Int> OnMovePressed;
    event Action             OnMoveReleased;
    event Action<Vector2>    OnCameraMove;
    event Action             OnPaused;

}
