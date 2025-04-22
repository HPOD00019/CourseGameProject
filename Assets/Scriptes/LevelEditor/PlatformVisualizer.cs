using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LevelEditor.MovingPlatformRoute;

public class PlatformVisualizer : MonoBehaviour
{
    public GameObject ObjectToTrack;
    public Spawner ObjectSpawner;
    public GameObject Plane;


    public Vector3? GetMouseWorldPosition()
    {
        Vector3? ans = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] objects = Physics.RaycastAll(ray);
        foreach (RaycastHit obj in objects)
        {
            if(obj.collider.gameObject == Plane)
            {
                ans = new Vector3(
                    (float)Math.Round(obj.point.x), 
                    (float)Math.Round(obj.point.y), 
                    (float)Math.Round(obj.point.z)
                    );
            }
        }
        return ans;
    }


    public void SpawnObject()
    {
        Vector3? location = GetMouseWorldPosition();


        if(location != null)
        {
            ObjectSpawner.SpawnObject((Vector3)location);
        }
        
    }


    public void DragObj()
    {
        Vector3? location = GetMouseWorldPosition();
        if (location!=null) ObjectToTrack.transform.position = (Vector3)location;
    }


    public void SwitchTurn(MovingPlatformScript s)
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


        EventManager.GetInstance().MovingPlatformEditModeEnter += SwitchTurn;
        EventManager.GetInstance().MovingPlatformEditModeExit += SwitchTurn;
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
