using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GamePresenter : IStartable
{
    [Inject] private ScenesChanger scenesChanger;
    public void Start()
    {
        scenesChanger.RunScene(ScenesChanger.ExistingScenes.MAIN_MENU, UnityEngine.SceneManagement.LoadSceneMode.Additive);

    }
}
