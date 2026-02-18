using System;
using UnityEngine;

[Serializable]
public class ItemObject : MonoBehaviour
{
    #region Inspector's Variables
    public Item itemData;
    public SpriteRenderer image;
    public Vector2Int gridPosition;
    #endregion

    #region Private Variables
    #endregion

    public static GameObject SpawnResource(Item resource, Vector2Int grid_position, Vector3 on_position, Transform parent)
    {
        GameObject instance = Instantiate(resource.resourceData.scene_prefab, parent);
        instance.transform.position = on_position;
        ItemObject item = instance.GetComponent<ItemObject>();
        if (item != null)
        {
            item.SetData(resource);
            item.gridPosition = grid_position;
        }
        return instance;
    }

    public void Awake()
    {
        CellsSystem.ResourcePicked += ItemPicked;
    }

    public void OnDestroy()
    {
        CellsSystem.ResourcePicked -= ItemPicked;
    }

    public void SetData(Item new_data)
    {
        itemData = new_data;
        image.sprite = itemData.resourceData.icon;
    }

    private void ItemPicked(Vector2Int from_position)
    {
        if (gridPosition != from_position) return;
        Destroy(this.gameObject);
    }
}

public class Item
{
    #region Inspector's Variables
    public Sprite         sprite;
    public BlocksResource resourceData;
    public int            count;
    #endregion

    #region Inspector's Variables

    #endregion

    public Item() { }
    public Item(BlocksResource resource_data, int _count = 1)
    {
        this.resourceData = resource_data;
        this.sprite       = resource_data.icon;
        this.count        = _count;
    }
}
