using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "MainConfig", menuName = "Game/Configs/Main config")]
public class MainConfig : ScriptableObject
{
    public TweenAnimationConfig tweenAnimation;
    public TweenMoveConfig      tweenMovement;
}
[CreateAssetMenu(fileName = "TweenAnimationConfig", menuName = "Game/Configs/Tween Animation Config")]
public class TweenAnimationConfig : ScriptableObject
{
    public float timeForStepIn   = 0.3f;
    public float timeForStepOut  = 0.3f;
    public Ease  easeIn          = Ease.InQuad;
    public Ease  easeOut         = Ease.OutQuad;
}

[CreateAssetMenu(fileName = "TweenMoveConfig", menuName = "Game/Configs/Tween Move Config")]
public class TweenMoveConfig : ScriptableObject
{
    public float timeForStep = 0.4f;
    public Ease  ease = Ease.InOutQuad;
}
