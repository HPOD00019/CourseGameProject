using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EventManager 
{
    static private EventManager _instance;
    private EventManager() { }
    static public EventManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new EventManager();
        }
        return _instance;
    }


    public delegate void PlatformEditModeEnter(MovingPlatformScript platform);
    public delegate void PlatformEditModeExit(MovingPlatformScript platform);
    

    public event PlatformEditModeEnter MovingPlatformEditModeEnter;
    public event PlatformEditModeExit MovingPlatformEditModeExit;


    public void OnMovingPlatformEditModeEnter(MovingPlatformScript platform)
    {
        MovingPlatformEditModeEnter.Invoke(platform);
    }


    public void OnMovingPlatformEditModeExit(MovingPlatformScript platform)
    {
        MovingPlatformEditModeExit.Invoke(platform);
    }

}
