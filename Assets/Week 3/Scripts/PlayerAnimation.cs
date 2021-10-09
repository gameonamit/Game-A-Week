using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerAnimation : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    public float runSpeed = 1.5f;

    private bool isIdle = false;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        MovementAnimation();
    }

    private void MovementAnimation()
    {
        if(Mathf.Abs(playerInput.Horizontal) > 0)
        {
            skeletonAnimation.AnimationName = "run";
            skeletonAnimation.loop = true;
            skeletonAnimation.timeScale = Mathf.Lerp(1f, 1.5f, runSpeed * Time.deltaTime);
            isIdle = false;
        }
        else
        {
            if (!isIdle)
            {
                skeletonAnimation.AnimationName = "idle";
                skeletonAnimation.loop = true;
                skeletonAnimation.timeScale = 1f;
                isIdle = true;
            }
        }

        if (!playerMovement.isGrounded)
        {
            skeletonAnimation.AnimationName = "jump";
            skeletonAnimation.loop = false;
            skeletonAnimation.timeScale = 1f;
            isIdle = false;
        }
    }
}
