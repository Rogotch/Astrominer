using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New resource", menuName = "Block's Resources/Base resource"), Serializable]
public class BlocksResource : ScriptableObject
{
    [SerializeField] public string               tag = "";
    [SerializeField] public Sprite               icon;
    [SerializeField] public SDict_V2Int_TileBase tile_variants;
    [SerializeField] public GameObject           scene_prefab;


}
