using System;
using UnityEngine;

public interface ICamera
{
    
}
public abstract class BaseCameraController : MonoBehaviour, ICamera
{
    public Camera ObjectCamera => GetComponent<Camera>();
    protected BaseCharacterController followedCharacter;
    protected virtual void FollowToCharacter()
    {
        Vector3 finalPosition = followedCharacter.transform.position;
        finalPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, finalPosition, 0.01f);
    }
    protected virtual bool NeedToFollow() 
    {
        Vector2 charVec = new Vector2(followedCharacter.transform.position.x, followedCharacter.transform.position.y);
        Vector2  camVec = new Vector2(transform.position.x, transform.position.y);
        return followedCharacter != null &&
        (Math.Abs(camVec.magnitude - charVec.magnitude) > 0.1f);
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