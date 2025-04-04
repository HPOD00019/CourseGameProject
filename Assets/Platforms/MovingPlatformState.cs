using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMovingPlatformState
{
    public float MovingSpeed { get; set; }
    protected readonly MonoBehaviour gameObject;
    protected Coroutine MoveToNextPoint;
    protected int _currentLocation;
    protected List<Vector3> _points;
    public abstract void MoveToTheNextPoint();
    public IMovingPlatformState(MonoBehaviour GameObject, ref List<Vector3> points, int currentLocation = 0)
    {
        gameObject = GameObject;
        _points = points;
        _currentLocation = currentLocation;
    }
    public virtual void StopPlatform()
    {
        if(_points.Count > 1)gameObject.transform.position = _points[_currentLocation];
    }
}
public class OnTurnState : IMovingPlatformState
{
    public OnTurnState(MonoBehaviour GameObject, ref List<Vector3> points, int currentLocation = 0) : base(GameObject, ref points, currentLocation = 0)
    {

    }
    public override void MoveToTheNextPoint()
    {
        if (_points.Count > 1)
        {
            _currentLocation = _currentLocation == _points.Count - 1 ? 0 : _currentLocation + 1;
            gameObject.transform.position = _points[_currentLocation];
        }

    }
}
public class DefaultState : IMovingPlatformState
{
    public override void MoveToTheNextPoint()
    {
        if (MoveToNextPoint == null && _points.Count > 1)
        {
            _currentLocation = _currentLocation == _points.Count - 1? 0 : _currentLocation + 1;
            MoveToNextPoint = gameObject.StartCoroutine(MoveTowardsPoint(_points[_currentLocation]));
        }
    }
    public DefaultState(MonoBehaviour GameObject, ref List<Vector3> points, int currentLocation = 0) : base(GameObject, ref points, currentLocation = 0)
    {
    }
    private IEnumerator MoveTowardsPoint(Vector3 targetLocation)
    {
        while (Vector3.Distance(gameObject.transform.position, targetLocation) > 0.00001f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetLocation, MovingSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.transform.position = targetLocation;
        MoveToNextPoint = null;
    }
}