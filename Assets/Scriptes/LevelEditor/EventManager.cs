
using System.Diagnostics.Contracts;
using UnityEngine;

public class EventManager 
{
    public GameObject focusedObject;
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

    public void Clear()
    {
        _instance = null;
    }
    public delegate void PlatformEditModeEnter();
    public delegate void PlatformEditModeExit();
    public delegate void LayerLevelChanged(int change);
    public delegate void SpawnPlatform();
    public delegate void EditRotationPlatform(RotationPlatformScript platform);

    public event PlatformEditModeEnter MovingPlatformEditModeEnter;
    public event PlatformEditModeExit MovingPlatformEditModeExit;


    public event LayerLevelChanged PlaneLayerChanged;

    public event SpawnPlatform SpawnPlatformAlowed;

    public event EditRotationPlatform EditRotation;

    public void OnMovingPlatformEditModeEnter()
    {
        MovingPlatformEditModeEnter.Invoke();
        OnSpawnPlatformAlowedSwitch();
    }


    public void OnMovingPlatformEditModeExit()
    {
        focusedObject = null; 
        MovingPlatformEditModeExit.Invoke();
        OnSpawnPlatformAlowedSwitch();
        
    }


    public void OnPlaneLayerChanged(int change)
    {
        PlaneLayerChanged.Invoke(change);
    }


    public void OnSpawnPlatformAlowedSwitch()
    {
        SpawnPlatformAlowed.Invoke();
    }


    public void OnEditRotationPlatform(RotationPlatformScript platform)
    {
        EditRotation.Invoke(platform);
    }
}
