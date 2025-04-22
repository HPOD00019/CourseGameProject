using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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


    public float MoveSpeed;


    void Start()
    {
        EventManager.GetInstance().MovingPlatformEditModeEnter += SwitchTurn;
        EventManager.GetInstance().MovingPlatformEditModeExit += SwitchTurn;


        PlatformRoute = new ObservableCollection<Vector3>();
        PlatformRoute.CollectionChanged += CircleRoute;


        _actualRoute = new List<Vector3>();


        OnTurnState turnState = new OnTurnState(this, ref _actualRoute);
        DefaultState defaultState = new DefaultState(this, ref _actualRoute);

        turnState.MovingSpeed = MoveSpeed;
        defaultState.MovingSpeed = MoveSpeed;


        _states = new Dictionary<Type, IMovingPlatformState>();
        _states.Add(typeof(OnTurnState), turnState);
        _states.Add(typeof(DefaultState), defaultState);
        _state = defaultState;


        EventManager.GetInstance().OnMovingPlatformEditModeEnter(this);
    }

    public void Update()
    {
        _state.MoveToTheNextPoint();
    }


    public void DrawRoute()
    {
        var n = gameObject.AddComponent<RouteVisualizer>();
        n.Route = new List<Vector3>(PlatformRoute);
        
        n.DrawRoute();
    }


    public void SwitchTurn(MovingPlatformScript platform)
    {
        enabled = !enabled;

        if (!enabled)
        {
            DrawRoute();
        }
        else
        {
            Destroy(gameObject.GetComponent<RouteVisualizer>());
            Destroy(gameObject.GetComponent<LineRenderer>());
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
        while (steps > 0)
        {
            _state.MoveToTheNextPoint();
            steps--;
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
