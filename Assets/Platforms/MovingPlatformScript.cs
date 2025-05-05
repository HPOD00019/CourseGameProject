using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using LevelEditor.MovingPlatformRoute;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Dictionary<Type, IMovingPlatformState> _states;
    private IMovingPlatformState _state;

    public ObservableCollection<Vector3> PlatformRoute;
    private List<Vector3> _actualRoute;


    private RouteVisualizer _visualizer;
    public float MoveSpeed;


    void Start()
    {
        EventManager.GetInstance().MovingPlatformEditModeEnter += SwitchTurn;
        EventManager.GetInstance().MovingPlatformEditModeExit += SwitchTurn;


        PlatformRoute = new ObservableCollection<Vector3>();
        PlatformRoute.CollectionChanged += CircleRoute;
        PlatformRoute.CollectionChanged += ShowRouteIsValid;

        _actualRoute = new List<Vector3>();


        OnTurnState turnState = new OnTurnState(this, ref _actualRoute);
        DefaultState defaultState = new DefaultState(this, ref _actualRoute);

        turnState.MovingSpeed = MoveSpeed;
        defaultState.MovingSpeed = MoveSpeed;


        _states = new Dictionary<Type, IMovingPlatformState>();
        _states.Add(typeof(OnTurnState), turnState);
        _states.Add(typeof(DefaultState), defaultState);
        _state = defaultState;

        _visualizer = gameObject.AddComponent<RouteVisualizer>();

        EventManager.GetInstance().focusedObject = gameObject;
        EventManager.GetInstance().OnMovingPlatformEditModeEnter();
    }

    public void Update()
    {
        _state.MoveToTheNextPoint();
    }


    public void DrawRoute()
    {
        _visualizer.Route = new List<Vector3>(PlatformRoute);
        _visualizer.DrawRoute();
    }


    public void SwitchTurn()
    {
        enabled = !enabled;

        if (!enabled)
        {
            DrawRoute();
        }
        foreach(var state in _states.Keys)
        {
            if(state != _state.GetType())
            {
                _state = _states[state];
                _state.StopPlatform();
            }
        }
    }

    public void MakeAStep(int steps = 1)
    {
        if (steps > 0)
        {
            while (steps > 0)
            {
                _state.MoveToTheNextPoint();
                steps--;
            }
        }
        else
        {
            while (steps < 0)
            {
                _state.MoveToTheNextPoint();
                steps++;
            }
        }
    }

    public int GetCurrentLocation()
    {
        return _state.GetCurrentLocation();
    }


    public List<Vector3> GetNextLocations(int count = 1)
    {
        return _state.GetNextLocations(count);
    }


    private void ShowRouteIsValid(object sender, EventArgs e)
    {
        if (Validator.IsRouteValid(this))
        {
            _visualizer.LineBeginColor = Color.green;
            _visualizer.LineEndColor = Color.green;
        }
        else
        {
            _visualizer.LineBeginColor = Color.red;
            _visualizer.LineEndColor = Color.red;
        }
    }
    private void CircleRoute(object sender, EventArgs e)
    {
        List<Vector3> ans = new List<Vector3>(PlatformRoute);
        if ((ans.Count > 0) && (ans[ans.Count - 1] != ans[0]))
        {
            List<Vector3> routes = new List<Vector3>(ans);
            routes.Reverse();
            routes.RemoveAt(0);// Remove the first item, equal to the last one
            ans.AddRange(routes);
        }
        _actualRoute.Clear();
        _actualRoute.AddRange(ans);
    }
}
