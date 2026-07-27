using System;
using UnityEngine;

public interface ICamera
{
    
}
public abstract class BaseCameraController : MonoBehaviour, ICamera
{
    public Camera ObjectCamera => GetComponent<Camera>();
    protected BaseCharacterController followedCharacter;
    protected virtual void FollowToCharacter() => transform.position = Vector3.Slerp(transform.position, followedCharacter.transform.position, 0.3f);
    protected virtual bool NeedToFollow() 
    {
        return followedCharacter != null &&
        (Math.Abs(transform.position.magnitude - followedCharacter.transform.position.magnitude) > 0.5);
    }
    public virtual void SetFollowedCharacter(BaseCharacterController character) => followedCharacter = character;
    public virtual void Update()
    {
        if (NeedToFollow()) FollowToCharacter();
    }
    public virtual void SetOnCharacterPosition()
    {
        Vector3 finalPosition = followedCharacter.transform.position;
        finalPosition.z = -10;
        transform.position = finalPosition;
    }
}