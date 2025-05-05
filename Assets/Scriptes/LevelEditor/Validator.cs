using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Validator 
{
    public static bool IsPlatformAllowed(MovingPlatformScript mv, Vector3 targetLocation)
    {
        bool ans = true;
        List<MovingPlatformScript> platforms = new List<MovingPlatformScript>(GameObject.FindObjectsOfType<MovingPlatformScript>());
        
        foreach (var platform in platforms)
        {
            if(platform != null && platform != mv)
            {
                if (platform.PlatformRoute.Contains(targetLocation))
                {
                    if(platform.GetNextLocations()[0] == targetLocation)
                    {
                        return false;
                    }
                }
            }
        }

        return ans;
    }

    public static bool IsRouteValid(MovingPlatformScript mv)
    {
        bool ans = true;

        List<MovingPlatformScript> platforms = new List<MovingPlatformScript>(GameObject.FindObjectsOfType<MovingPlatformScript>());
        List<Vector3> route = new List<Vector3>(mv.PlatformRoute);


        foreach (var platform in platforms)
        {
            if (platform != null && platform != mv)
            {
                if (platform.PlatformRoute.Intersect(route).Any()) 
                {
                    var mvNextPoints = mv.GetNextLocations(4 * route.Count * platform.PlatformRoute.Count);
                    var platformNextPoints = platform.GetNextLocations(4 * route.Count * platform.PlatformRoute.Count);

                    for(int i = 0; i < mvNextPoints.Count; i++)
                    {
                        if (mvNextPoints[i] ==  platformNextPoints[i])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return ans;
    }
}
