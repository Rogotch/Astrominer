using System;
using UnityEngine;

public interface IInputService
{
    event Action<Vector2Int> OnMove;
    event Action<Vector2>    OnCameraMove;
    event Action             OnPaused;

}
