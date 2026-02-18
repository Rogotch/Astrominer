using UnityEngine;
using UnityEngine.Tilemaps;

public static class RendererExtention
{
    public static void SetColor(this Renderer renderer, Color color)
    {
        if (renderer == null) return;

             if (renderer is SpriteRenderer   spriteRenderer)            spriteRenderer.color = color;
        else if (renderer is MeshRenderer       meshRenderer)     meshRenderer.material.color = color;
        else if (renderer is TilemapRenderer tilemapRenderer)  tilemapRenderer.material.color = color;
    }
}
