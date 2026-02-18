using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Game/Cell Data")]
public class CellData : ScriptableObject
{
    [SerializeField] public float          max_health = 5;
    [SerializeField] public TileBase       tile;
    [SerializeField] public BlocksResource cell_resource = null;

    public Cell GetCell()
    {
        Cell cell = new Cell();
        cell.max_health = max_health;
        cell.health = max_health;
        cell.tile = tile;
        cell.cell_resource = cell_resource;
        return cell;
    }

}
