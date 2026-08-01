using UnityEngine;

public interface IResourcePicker
{
    public abstract void ResourcePickup(Item item);
    public abstract void OnShipUnloadingZone();
}
