using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition thirst;
    public Condition stamina;

    private void Start()
    {
        PlayerManager.Instance.Player.playerCondition.uiCondition = this;
    }
}
