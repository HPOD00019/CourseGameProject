using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{

    [SerializeField] public List<ListHandler> platforms;
    public List<Vector3> staticPlatforms;
    public List<Vector3> rotationPlatforms;
    public List<Vector3> diagonalPlatforms;
    public List<Vector3> trapPlatforms;
    public List<Vector3> Finish;
    public Level()
    {
        platforms = new List<ListHandler>();
        staticPlatforms = new List<Vector3>();
        rotationPlatforms = new List<Vector3>();
        diagonalPlatforms = new List<Vector3>();
        trapPlatforms = new List<Vector3>();
        Finish = new List<Vector3>();
    }
}
