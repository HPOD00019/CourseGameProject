using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMovingPlatformState
{
    public float MovingSpeed { get; set; }
    protected readonly MonoBehaviour gameObject;
    protected bool MoveToNextPoint = false;
    protected Coroutine MoveToNextPointCoroutine;
    protected int _currentLocation;
    protected List<Vector3> _points;
    protected Rigidbody _rigidBody;

    public abstract void MoveToTheNextPoint();
    public abstract void MoveToThePreviousPoint();


    public Vector3 GetCurrentWorldLocation()
    {
        return _points[_currentLocation];
    }


    public IMovingPlatformState(MonoBehaviour GameObject, ref List<Vector3> points, int currentLocation = 0, Rigidbody rigidBody = null)
    {
        gameObject = GameObject;
        _points = points;
        _currentLocation = currentLocation;
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }


    public virtual void StopPlatform()
    {
        if(_points.Count > 1)gameObject.transform.position = _points[_currentLocation];
    }


    public int GetCurrentLocation()
    {
        return _currentLocation;
    }    
    

    public List<Vector3> GetNextLocations(int count = 1)
    {
        List<Vector3> ans = new List<Vector3>();
        int index = _currentLocation;

        while(count > 0)
        {
            index = index >= _points.Count - 1 ? 0 : index + 1;
            ans.Add(_points[index]);
            count--;
        }

        return ans;
    }


    public void SetCurrentLocation(int location)
    {
        _currentLocation = location;
    }
}


public class OnTurnState : IMovingPlatformState
{

    public OnTurnState(MonoBehaviour GameObject, ref List<Vector3> points, int currentLocation = 0) : base(GameObject, ref points, currentLocation = 0)
    {

    }


    public override void MoveToThePreviousPoint()
    {
        if (_points.Count > 1)
        {
            _currentLocation = _currentLocation == 0 ? _points.Count - 1 : _currentLocation - 1;
            gameObject.transform.position = _points[_currentLocation];
        }
    }


    public override void MoveToTheNextPoint()
    {

        if (_points.Count > 1)
        {
            _currentLocation = _currentLocation == _points.Count - 1 ? 1 : _currentLocation + 1;
            gameObject.transform.position = _points[_currentLocation];
        }

    }
}
public class DefaultState : IMovingPlatformState
{

    public override void MoveToThePreviousPoint()
    {
        if (!MoveToNextPoint && _points.Count > 1)
        {
            _currentLocation = _currentLocation == 0 ? _points.Count - 1 : _currentLocation - 1;
            MoveToNextPointCoroutine = gameObject.StartCoroutine(MoveTowardsPoint(_points[_currentLocation]));
        }
    }


    public override void MoveToTheNextPoint()
    {
        if (!MoveToNextPoint  && _points.Count > 1)
        {
            _currentLocation = _currentLocation == _points.Count - 1? 1 : _currentLocation + 1;
            MoveToNextPointCoroutine = gameObject.StartCoroutine(MoveTowardsPoint(_points[_currentLocation]));
        }
    }


    public DefaultState(MonoBehaviour GameObject, 
                        ref List<Vector3> points, 
                        int currentLocation = 0) : base(GameObject, 
                                                        ref points, 
                                                        currentLocation = 0)
    {
    }


    private IEnumerator MoveTowardsPoint(Vector3 targetLocation)
    {
        MoveToNextPoint = true;
        bool targetReached = Vector3.Distance(gameObject.transform.position, targetLocation) < 0.025f;
        while (true)
        {
            targetReached = Vector3.Distance(gameObject.transform.position, targetLocation) < 0.025f;
            if (targetReached)
            {
                gameObject.transform.position = targetLocation;
                if(_rigidBody != null) _rigidBody.position = targetLocation;
                break;
            }


            if (_rigidBody != null)
            {
                Vector3 move = (targetLocation - _rigidBody.position).normalized;

                _rigidBody.MovePosition(_rigidBody.position + move * MovingSpeed * Time.deltaTime);
                
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetLocation, MovingSpeed * Time.deltaTime);            
            }



            yield return null;
        }

        gameObject.transform.position = targetLocation;
        MoveToNextPoint = false;
    }

}