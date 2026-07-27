using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "TweenAnimationConfig", menuName = "Game/Configs/Tween Animation Config")]
public class TweenAnimationConfig : ScriptableObject
{
    public float timeForStepIn   = 0.3f;
    public float timeForStepOut  = 0.3f;
    public Ease  easeIn          = Ease.InQuad;
    public Ease  easeOut         = Ease.OutQuad;
}
