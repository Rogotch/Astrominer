using UnityEngine;

public class CommonCameraComponent : BaseCameraController, ICamera
{
    public CommonCameraComponent()
    {
        Debug.Log("CommonCameraComponent");
    }

    public void Awake()
    {
        Debug.Log("CommonCameraComponent Awake");
    }
    public void Disabled()
    {
        Debug.Log("CommonCameraComponent Disabled");
    }
}
