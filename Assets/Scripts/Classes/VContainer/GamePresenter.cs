using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GamePresenter : IStartable
{
    private readonly IInputService input;

    public GamePresenter(IInputService input)
    {
        this.input = input;
    }
    public void Start()
    {
        Debug.Log("Game started with DI");
    }
}
