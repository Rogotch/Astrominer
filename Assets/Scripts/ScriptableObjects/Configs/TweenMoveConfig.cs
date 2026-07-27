using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "TweenMoveConfig", menuName = "Game/Configs/Tween Move Config")]
public class TweenMoveConfig : ScriptableObject
{
    public float timeForStep = 0.4f;
    public Ease  ease = Ease.InOutQuad;
}
