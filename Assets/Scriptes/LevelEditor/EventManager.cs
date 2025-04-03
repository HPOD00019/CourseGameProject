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

    public delegate void PlatformEditModeEnter();
    public event PlatformEditModeEnter MovingPlatformEditModeEnter;
    public event PlatformEditModeEnter MovingPlatformEditModeExit;

    public void OnMovingPlatformEditModeEnter()
    {
        MovingPlatformEditModeEnter.Invoke();
    }
    public void OnMovingPlatformEditModeExit()
    {
        MovingPlatformEditModeExit.Invoke();
    }

}
