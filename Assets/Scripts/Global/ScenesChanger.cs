using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class ScenesChanger
{
    public enum ExistingScenes {LOADING_SCREEN, MAIN_MENU, DIGGING_UI, LEVEL}
    private static readonly Dictionary<ExistingScenes, int> ScenesIndexes = new Dictionary<ExistingScenes, int> {
        {ExistingScenes.LOADING_SCREEN, 1},
        {ExistingScenes.MAIN_MENU,      2},
        {ExistingScenes.DIGGING_UI,     3},
        {ExistingScenes.LEVEL,          4}};
        
    public static void RunScene(ExistingScenes scene, LoadSceneMode loadingMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(ScenesIndexes[scene], loadingMode);
    }
    public static void CloseScene(ExistingScenes scene)
    {
        SceneManager.UnloadSceneAsync(ScenesIndexes[scene]);
    }
}
