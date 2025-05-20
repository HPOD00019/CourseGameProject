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

public class GameMovingPlatform : MonoBehaviour
{
    private Dictionary<Type, IMovingPlatformState> _states;
    private IMovingPlatformState _state;

    [SerializeField] public ObservableCollection<Vector3> PlatformRoute;
    private List<Vector3> _actualRoute;


    private RouteVisualizer _visualizer;
    public float MoveSpeed;

    private Rigidbody _rigidBody;

    private List<Rigidbody> _affectedBodies;
    private Vector3 _lastPosition;

    void Awake()
    {
        _affectedBodies = new List<Rigidbody>();
        _rigidBody = GetComponent<Rigidbody>();

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
        
    }


    public void Start()
    {
        _lastPosition = _rigidBody.position;
    }
    public void Update()
    {
        _state.MoveToTheNextPoint();
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

    public void SetCurrentLocation(int location)
    {
        _state.SetCurrentLocation(location);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.rigidbody;

        if (otherRb != null && !_affectedBodies.Contains(otherRb) && collision.gameObject.tag == "Player")
        {
            var ball = collision.gameObject.GetComponent<BallController>();
            if (!(bool)ball?.OnMovingPlatform)
            {
                ball.OnMovingPlatform = true;
                _affectedBodies.Add(otherRb);
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody otherRb = collision.rigidbody;

        if (otherRb != null && _affectedBodies.Contains(otherRb))
        {
            var ball = collision.gameObject.GetComponent<BallController>();
            if (ball != null)
            {
                ball.OnMovingPlatform = false;
                _affectedBodies.Remove(otherRb);
            }
        }
           
    }

    private void FixedUpdate()
    {
        Vector3 move = _rigidBody.position - _lastPosition;

        foreach(var body in _affectedBodies)
        {
            if(body != null) body.MovePosition(body.position + move);
        }

        _lastPosition = _rigidBody.position;

    }
}
