using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : SingleTonBase<EnvironmentManager>
{
    Temperature _temperature;
    public Temperature Temperature
    {
        get { return _temperature; }
        set { _temperature = value; }
    }
    
    TimeController _timeController;

    public TimeController TimeController
    {
        get { return _timeController; }
        set { _timeController = value; }
    }
}
