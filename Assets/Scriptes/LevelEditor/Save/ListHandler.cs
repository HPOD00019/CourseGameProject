using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListHandler
{
    public List<Vector3> Route;
    public Vector3 location;
    public int currentLocation;

    public ListHandler()
    {
        Route = new List<Vector3>();
    }

    public ListHandler(ListHandler l)
    {
        location = l.location;
        Route = new List<Vector3>(l.Route);
        currentLocation = l.currentLocation;
    }
}
