using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Saver : MonoBehaviour
{
    public string LevelName;
    public string SaveLevel()
    {

        Level ans = new Level();
        MovingPlatformScript[] movingPlatforms = FindObjectsOfType<MovingPlatformScript>();
        RotationPlatformScript[] rotationPlatforms = FindObjectsOfType<RotationPlatformScript>();
        StaticPlatformScript[] staticPlatformScripts = FindObjectsOfType<StaticPlatformScript>();
        TrapPlatformScript[] trapPlatformScripts = FindObjectsOfType<TrapPlatformScript>();
        SpawnPlatform[] spawnPlatforms = FindObjectsOfType<SpawnPlatform>();

        var q = new ListHandler();
        foreach (var platform in movingPlatforms)
        {
            q.currentLocation = platform.GetCurrentLocation();
            q.location = platform.GetCurrentWorldLocation();
            q.Route = new List<Vector3>(platform.PlatformRoute);
            ans.platforms.Add(q);
            q = new ListHandler();
        }
        

        foreach (var rotation in rotationPlatforms)
        {
            ans.rotationPlatforms.Add(rotation.gameObject.transform.position);
        }

        foreach (var statics in staticPlatformScripts)
        {
            ans.staticPlatforms.Add(statics.gameObject.transform.position);
        }

        foreach (var trap in trapPlatformScripts)
        {
            ans.trapPlatforms.Add(trap.gameObject.transform.position);
        }

        foreach (var platform in spawnPlatforms)
        {
            ans.Finish.Add(platform.gameObject.transform.position);
        }

        string n = JsonUtility.ToJson(ans);

        return n;
    }


    public void SaveToMemory(string name = "")
    {
        if (name != "")
        {
            LevelName = name + ".json";
        }
        else
        {
            return;
        }


        string folderName = "Levels";

        string folderPath = Path.Combine(Application.streamingAssetsPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, LevelName);

        File.WriteAllText(filePath , SaveLevel());
    }


    public void Update()
    {

    }


    public void Start()
    {
        
    }
}
