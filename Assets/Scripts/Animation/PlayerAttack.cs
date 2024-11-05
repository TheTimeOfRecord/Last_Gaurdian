using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack
{
    private PlayerAnimationManager animationManager;

    public PlayerAttack(PlayerAnimationManager animationManager)
    {
        this.animationManager = animationManager;
    }

    public void HandleAttack()
    {
        animationManager.TriggerAttack();
    }
}
