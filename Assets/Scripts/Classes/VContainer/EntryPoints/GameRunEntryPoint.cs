using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class GameRunEntryPoint : IStartable
{
    private readonly ScenesChanger scenesChanger;
    public GameRunEntryPoint(ScenesChanger scenesChanger)
    {
        this.scenesChanger = scenesChanger;
    }
    public void Start()
    {
        scenesChanger.RunScene  (ScenesChanger.ExistingScenes.DIGGING_UI, LoadSceneMode.Additive);
        scenesChanger.RunScene  (ScenesChanger.ExistingScenes.LEVEL,      LoadSceneMode.Additive);
        scenesChanger.CloseScene(ScenesChanger.ExistingScenes.MAIN_MENU);
    }
}
