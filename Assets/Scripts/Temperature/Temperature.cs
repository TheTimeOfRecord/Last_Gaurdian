using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float temperature;
    
    void Update()
    {
        text.text = $"{temperature.ToString()}도";
    }
    
    public void AddTemperature(float value)
    {
        temperature += value;
    }

    public void SubtractTemperature(float value)
    {
        temperature -= value;
    }
}
