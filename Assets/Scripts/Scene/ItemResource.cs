using System;
using UnityEngine;
using VContainer;

[Serializable]
public class ItemObject : MonoBehaviour, IPickable
{
    #region Inspector's Variables
    public Item itemData;
    public SpriteRenderer image;
    public Vector2Int gridPosition;
    #endregion

    #region Private Variables
    [Inject] private ICellsService cellsSystem;
    #endregion


    public void Start()
    {
        cellsSystem.ResourcePicked += ItemPicked;
    }

    public void OnDestroy()
    {
        cellsSystem.ResourcePicked -= ItemPicked;
    }

    public void PickUp(Vector2Int from_position)
    {
        Destroy(this.gameObject);
    }
    public bool CanBePicked(Vector2Int from_position)
    {
        return gridPosition == from_position;
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
    public Item(BlocksResource resourceData, int count = 1)
    {
        this.resourceData = resourceData;
        this.sprite       = resourceData.icon;
        this.count        = count;
    }
}
