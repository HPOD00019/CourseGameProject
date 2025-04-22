using System.Collections;
using System.Collections.Generic;
using LevelEditor.MovingPlatformRoute;
using UnityEngine;

public class MovingPlatformEditor : MonoBehaviour
{
    private PlatformVisualizer _visualizer;
    public MovingPlatformScript PlatformToEdit;

    private List<MovingPlatformScript> _movingPlatforms;
    #region Add Route


    public void AddRoutePoint()
    {
        Vector3? v = _visualizer.GetMouseWorldPosition();
        if (v != null) AddRoutePoint((Vector3)v);
    }


    public void AddRoutePoint(Vector3 newPoint)
    {
        PlatformToEdit.PlatformRoute.Add(newPoint);
        foreach (var script in _movingPlatforms)
        {
            script.MakeAStep();
        }
    }


    #endregion
    public void OnEditModeSwitch(MovingPlatformScript platform)
    {
        _movingPlatforms = new List<MovingPlatformScript>(FindObjectsOfType<MovingPlatformScript>());
        PlatformToEdit = platform;
        if (PlatformToEdit.PlatformRoute.Count == 0) AddRoutePoint();
        enabled = !enabled;
    }


    public void Start()
    {
        this.enabled = false;
        _visualizer = FindAnyObjectByType<PlatformVisualizer>();


        EventManager.GetInstance().MovingPlatformEditModeEnter += OnEditModeSwitch;
        EventManager.GetInstance().MovingPlatformEditModeExit += OnEditModeSwitch;
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EventManager.GetInstance().OnMovingPlatformEditModeExit(PlatformToEdit);
        }
        if(Input.GetMouseButtonDown(0))
        {
            AddRoutePoint();
        }
    }
}
