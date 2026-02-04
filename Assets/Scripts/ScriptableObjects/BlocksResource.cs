using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New resource", menuName = "Block's Resources/Base resource")]
public class BlocksResource : ScriptableObject
{
    public string tag = "";
    public SDict_V2Int_TileBase tile_variants;
    
    //private void foo()
    //{
    //    //tile_variants.Dictionary[]
    //}
}
