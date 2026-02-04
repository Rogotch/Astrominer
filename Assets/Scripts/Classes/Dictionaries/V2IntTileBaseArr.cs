using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TilesArrVariantPair : IDictionaryPair<Vector2Int, TileBase[]>
{
    [SerializeField] public Vector2Int _key;
    [SerializeField] public TileBase[] _value;
    public Vector2Int key => _key;
    public TileBase[] value { get => _value; set => this._value = value; }
}

[Serializable]
public class SDict_V2Int_TileBaseArr : SeriazableDictionary<Vector2Int, TileBase[], TilesArrVariantPair>
{

}
