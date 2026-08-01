using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScenesChanger
{
    public enum ExistingScenes {GAME_RUN, LOADING_SCREEN, MAIN_MENU, DIGGING_UI, LEVEL}
    private readonly Dictionary<ExistingScenes, int> ScenesIndexes = new Dictionary<ExistingScenes, int> {
        {ExistingScenes.GAME_RUN,       1},
        {ExistingScenes.LOADING_SCREEN, 2},
        {ExistingScenes.MAIN_MENU,      3},
        {ExistingScenes.DIGGING_UI,     4},
        {ExistingScenes.LEVEL,          5}};
        
    public void RunScene(ExistingScenes scene, LoadSceneMode loadingMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(ScenesIndexes[scene], loadingMode);
    }
    public void CloseScene(ExistingScenes scene)
    {
        SceneManager.UnloadSceneAsync(ScenesIndexes[scene]);
    }
}
