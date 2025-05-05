using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LevelEditor.MovingPlatformRoute;

public class PlatformVisualizer : MonoBehaviour
{
    
    public Spawner ObjectSpawner;
    public GameObject Plane;


    public GameObject ObjectToTrack;

    public Vector3? GetMouseWorldPosition()
    {
        Vector3? ans = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] objects = Physics.RaycastAll(ray);

        foreach (var obj in objects)
        {
            if(obj.collider.gameObject == Plane)
            {
                ans = new Vector3(
                    (float)Math.Round(objects[0].point.x), 
                    (float)Math.Round(objects[0].point.y), 
                    (float)Math.Round(objects[0].point.z)
                    );
            }
        }


        return ans;
    }


    public void SpawnObject()
    {
        if (ObjectToTrack == null) return;
        Vector3? location = GetMouseWorldPosition();


        if(location != null)
        {
            ObjectSpawner.SpawnObject((Vector3)location);
        }
        
    }


    public void DragObj()
    {
        if (ObjectToTrack == null) return;
        Vector3? location = GetMouseWorldPosition();

        if (location!=null)
        {
            ObjectToTrack.SetActive(true);
            ObjectToTrack.transform.position = (Vector3)location;
        }  

    }


    public void SetNewObjectToTrack(GameObject obj)
    {
        Destroy(ObjectToTrack);
        ObjectToTrack = Instantiate(obj);
    }


    public void SwitchTurn()
    {
        enabled = !enabled;

        if (ObjectToTrack != null)
        {
            MeshRenderer m = ObjectToTrack.GetComponent<MeshRenderer>();
            Collider c = ObjectToTrack.GetComponentInChildren<Collider>();


            if (c != null) c.enabled = enabled;
            if (m != null) m.enabled = enabled;

            ObjectToTrack.SetActive(enabled);

        }
    }


    private void Start()
    {
        ObjectSpawner = FindAnyObjectByType<Spawner>();
        EventManager.GetInstance().SpawnPlatformAlowed += SwitchTurn;
    }


    private void Update()
    {
        DragObj();
        if(Input.GetMouseButtonDown(0))
        {
            SpawnObject();
        }
    }
}
