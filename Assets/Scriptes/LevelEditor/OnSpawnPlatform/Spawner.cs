using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;

    public void SpawnObject(Vector3 location, GameObject gO = null)
    {
        if (gO != null) ObjectToSpawn = gO;
        GameObject newObj = Instantiate(ObjectToSpawn);
        newObj.transform.position = (Vector3)location;

    }    
}
