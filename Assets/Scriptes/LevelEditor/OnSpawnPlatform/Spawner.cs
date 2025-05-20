using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public GameObject FinishPlatform;

    private List<GameObject> platforms;

    public void SpawnObject(Vector3 location, GameObject gO = null)
    {
        if (gO != null) ObjectToSpawn = gO;
        if (!CheckForPlatform(location)) return;

        GameObject newObj = Instantiate(ObjectToSpawn);
        newObj.transform.position = (Vector3)location;

        platforms.Add(newObj);
    }

    public void Start()
    {
        GameObject go = Instantiate(FinishPlatform);
        go.transform.position = new Vector3(0,0,0);
        var n = go.GetComponent<SpawnPlatform>();
        if (n != null) Destroy(n);

        platforms = new List<GameObject>();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) RemoveLastPlatform();
    }
    private bool CheckForPlatform(Vector3 location)
    {
        foreach(var p in platforms)
        {
            if(p.transform.position == location) return false;
        }

        return true;
    }


    private void RemoveLastPlatform()
    {
        if (platforms.Count == 0) return;
        var n = platforms[platforms.Count - 1];
        platforms.Remove(n);
        Destroy(n);
    }
}
