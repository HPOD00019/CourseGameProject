using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHandler : MonoBehaviour
{
    public GameObject Plane;
    private int _currentPlaneLevel;
    public void ChangePlaneLevel(int value)
    {
        _currentPlaneLevel += value;
    }
    private void MovePlane(Vector3 newPosition)
    {
        Plane.transform.position = newPosition;
    }
    private void MovePlane(int newLevel)
    {
        Plane.transform.position = new Vector3(0, newLevel, 0);
    }
    void Start()
    {
        _currentPlaneLevel = 0;
        MovePlane(0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _currentPlaneLevel++;
            MovePlane(_currentPlaneLevel);
        }
    }
}
