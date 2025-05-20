using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Initializer : MonoBehaviour
{
    public string LevelName;

    public GameObject TrapPlatformPrefab;
    public GameObject MovingPlatformPrefab;
    public GameObject RotationPlatformPrefab;
    public GameObject StaticPlatformPrefab;
    public GameObject SpawnPlatformPrefab;

    public GameObject Ball;
    public CameraFollowScript Camera;


    public float minimalHeight;
    public void LoadLevel()
    {
        Level level = new Level();
        string jsonLevel = string.Empty;

        if (string.IsNullOrEmpty(LevelName)) return;


        string folderName = "Levels";
        string levelName = LevelName + ".json";

        string folderPath = Path.Combine(Application.streamingAssetsPath, folderName);

        if (Directory.Exists(folderPath))
        {
            string filePath = Path.Combine(folderPath, levelName);
            jsonLevel = File.ReadAllText(filePath);

            level = JsonUtility.FromJson<Level>(jsonLevel);
        }
        else
        {
            return;
        }
        Load(level);

    }


    private void Load(Level level)
    {
        minimalHeight = 0f;

        foreach(var platform in level.trapPlatforms)
        {
            var p = Instantiate(TrapPlatformPrefab);
            p.transform.position = platform;
            if(p.transform.position.y < minimalHeight) minimalHeight = p.transform.position.y;
        }   

        foreach(var platform in level.rotationPlatforms)
        {
            var p = Instantiate(RotationPlatformPrefab);
            p.transform.position = platform;
            if (p.transform.position.y < minimalHeight) minimalHeight = p.transform.position.y;
        }

        foreach(var platform in level.staticPlatforms)
        {
            var p = Instantiate(StaticPlatformPrefab);
            p.transform.position = platform;
            if (p.transform.position.y < minimalHeight) minimalHeight = p.transform.position.y;
        }

        foreach(var platform in level.platforms)
        {
            var p = Instantiate(MovingPlatformPrefab);
            p.GetComponent<Rigidbody>().position = platform.location;
            var mp = p.GetComponent<GameMovingPlatform>();
            if (mp != null)
            {
                mp.SetCurrentLocation(platform.currentLocation);
                foreach(var route in platform.Route)
                {
                    mp.PlatformRoute.Add(route);
                    if (mp.transform.position.y < minimalHeight) minimalHeight = p.transform.position.y;
                }
            }
        }

        foreach(var platform in level.Finish)
        {
            var p = Instantiate(SpawnPlatformPrefab);
            p.transform.position = platform;
            if (p.transform.position.y < minimalHeight) minimalHeight = p.transform.position.y;
        }


    }
    public void Awake()
    {
        LevelName = InformationManager.LevelToLoadName;
    }
    public void Start()
    {
        GameObject go = Instantiate(SpawnPlatformPrefab);
        go.transform.position = new Vector3(0, 0, 0);
        var n = go.GetComponent<SpawnPlatform>();
        if (n != null) Destroy(n);
        
        LoadLevel();

        SpawnBall();
    }


    private void SpawnBall()
    {
        var ball = Instantiate(Ball);
        ball.gameObject.transform.position = new Vector3(0f, 1f, 0f);
        Camera.SetTarget(ball.gameObject.transform);
    }
}
