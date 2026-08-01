using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class MainMenu : MonoBehaviour
{
    [Inject] private ScenesChanger scenesChanger;
    public void StartNewGame()
    {
        scenesChanger.RunScene(ScenesChanger.ExistingScenes.GAME_RUN, LoadSceneMode.Additive);
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
