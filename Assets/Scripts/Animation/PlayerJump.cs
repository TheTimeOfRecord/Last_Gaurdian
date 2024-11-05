using UnityEngine;

public class PlayerJump
{
    private PlayerAnimationManager animationManager;

    public PlayerJump(PlayerAnimationManager animationManager)
    {
        this.animationManager = animationManager;
    }

    public void HandleJump(bool isJumpPressed, bool canJump, bool canMove)
    {
        if (isJumpPressed && canJump && canMove)
        {
            animationManager.TriggerJump();
        }
    }
}
