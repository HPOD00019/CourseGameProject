using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHandler : MonoBehaviour
{
    public GameObject Plane;
    private int _currentPlaneLevel;

    private void MovePlane(Vector3 newPosition)
    {
        Plane.transform.position = newPosition;
    }
    private void MovePlane(int newLevel)
    {
        Plane.transform.position = new Vector3(0, newLevel, 0);
    }

    
    public void ChangePlaneLayer(int newLevel)
    {
        _currentPlaneLevel += newLevel;
        MovePlane(_currentPlaneLevel);
    }


    void Start()
    {
        EventManager.GetInstance().PlaneLayerChanged += ChangePlaneLayer;
        _currentPlaneLevel = 0;
        MovePlane(0);
    }
    void Update()
    {

    }
}
