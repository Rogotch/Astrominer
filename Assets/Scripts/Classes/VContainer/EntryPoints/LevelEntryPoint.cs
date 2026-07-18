using UnityEngine;
using VContainer.Unity;

public class LevelEntryPoint : IStartable
{
    public void Start()
    {
        Debug.Log("Close menu");
        ScenesChanger.CloseScene(ScenesChanger.ExistingScenes.MAIN_MENU);
    }
}
