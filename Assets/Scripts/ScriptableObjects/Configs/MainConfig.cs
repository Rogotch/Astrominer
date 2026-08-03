using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "MainConfig", menuName = "Game/Configs/Main config")]
public class MainConfig : ScriptableObject
{
    public TweenAnimationConfig      tweenAnimation;
    public TweenMoveConfig           tweenMovement;
    public GameplayInterfaceConfig   gameplayInterface;
    public BreakingProcessTiles      breakingTiles;
}