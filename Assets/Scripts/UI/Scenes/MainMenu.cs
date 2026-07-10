using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuickStart()
    {
        
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
