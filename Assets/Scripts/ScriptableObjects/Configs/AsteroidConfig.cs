using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Game/Configs/Asteroid Configs")]
public class AsteroidConfig : ScriptableObject
{
    public AsteroidParameters   asteroidData;
    public AsteroidParameters   asteroidDataBackground;
    public CellsDataLayer[]     cellsDataLayers;
    public Vector2Int           startPosition;

}