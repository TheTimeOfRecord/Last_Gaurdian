using UnityEngine;
using UnityEngine.UI;

public enum ECondition
{
    CurValue,
    MaxValue
}

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(ECondition condition, float value)
    {
        if (condition == ECondition.CurValue)
        {
            curValue = Mathf.Min(curValue + value, maxValue);
        }
        else if (condition == ECondition.MaxValue)
        {
            maxValue += value;
        }
    }

    public void Subtract(float value)
    {
        curValue -= Mathf.Max(curValue - value, 0);
    }
}
