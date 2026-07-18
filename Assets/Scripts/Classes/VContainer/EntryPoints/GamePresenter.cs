using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GamePresenter : IStartable
{
    public void Start()
    {
        ScenesChanger.RunScene(ScenesChanger.ExistingScenes.MAIN_MENU, UnityEngine.SceneManagement.LoadSceneMode.Additive);

    }
}
