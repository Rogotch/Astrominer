using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        ScenesChanger.RunScene(ScenesChanger.ExistingScenes.DIGGING_UI, LoadSceneMode.Additive);
        ScenesChanger.RunScene(ScenesChanger.ExistingScenes.LEVEL,      LoadSceneMode.Additive);
    }

    public void QuickStart()
    {
        
    }
    public void QuitToDesktop()
    {
        Debug.Log("Quit");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
