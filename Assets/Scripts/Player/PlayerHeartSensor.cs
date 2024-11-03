using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartSensor : MonoBehaviour
{
    private int waterCount = 0;
    public event Action<bool> onHeartWaterEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterCount++;
            SetOnWater();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterCount--;
            SetOnWater();
        }
    }

    private void SetOnWater()
    {
        if (waterCount > 0)
        {
            onHeartWaterEvent?.Invoke(true);
        }
        else
        {
            onHeartWaterEvent?.Invoke(false);
        }
    }
}
