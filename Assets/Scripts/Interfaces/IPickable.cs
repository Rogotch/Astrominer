using UnityEngine;

public interface IPickable
{
    public abstract bool CanBePicked(Vector2Int from_position);
    public abstract void PickUp(Vector2Int from_position);
}
