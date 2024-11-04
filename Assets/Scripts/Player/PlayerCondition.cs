using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    
    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition thirst { get { return uiCondition.thirst; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public float noStaminaHealthDecay;
    
    
    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        thirst.Subtract(thirst.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue > 0.0f)
        {
            health.Add(health.passiveValue* Time.deltaTime);
        }
        else if(hunger.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (thirst.curValue > 0.0f)
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }
        else if (thirst.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
            stamina.Subtract(noStaminaHealthDecay * Time.deltaTime);
        }

        if(health.curValue < 0.0f)
        {
            Die();
        }
    }
    
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    private void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }
        
        stamina.Subtract(amount);
        return true;
    }
}