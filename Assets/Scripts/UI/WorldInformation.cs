using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInformation : MonoBehaviour
{
    public float clock;

    private void Awake()
    {
        clock = Time.time;
    }
}
