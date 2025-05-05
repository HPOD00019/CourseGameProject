using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using LevelEditor.MovingPlatformRoute;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MovingPlatformEditor : MonoBehaviour
{
    public GameObject Plane;
    [SerializeField] private GameObject PhantomPlatform;
    private List<GameObject> PhantomPlatforms = new List<GameObject>();

    private PlatformVisualizer _visualizer;
    public MovingPlatformScript PlatformToEdit;

    private List<MovingPlatformScript> _movingPlatforms;
    #region Add Route


    public void AddRoutePoint()
    {
        Vector3? v = _visualizer.GetMouseWorldPosition();
        if (v != null && 
            Validator.IsPlatformAllowed(PlatformToEdit, (Vector3)v) &&
            IsPlatformAllowed((Vector3)v))
        {
            AddRoutePoint((Vector3)v);
        } 
            
    }


    public void AddRoutePoint(Vector3 newPoint)
    {
        RemovePhantoms();

        PlatformToEdit.PlatformRoute.Add(newPoint);
        PlatformToEdit.DrawRoute();
        foreach (var script in _movingPlatforms)
        {
            script.MakeAStep();
        }

        SpawnPhantomPlatforms();
    }



    #endregion


    #region Skip Step/ Step Back

    public void SkipAStep(int steps = 1)
    {
        foreach(var script in _movingPlatforms)
        {
            if(script != PlatformToEdit)
            {
                script.MakeAStep(steps);
            }
        }
    }


    public void StepBack(MovingPlatformScript mv)
    {
        mv.MakeAStep(-1);
    }


    public void StepBack()
    {
        foreach (var script in _movingPlatforms)
        {
            if(script != PlatformToEdit) script.MakeAStep(-1);
        }
    }

    #endregion

    #region Spawn phantom platforms

    public void SpawnPhantomPlatforms(int n = 0)
    {
        if (PlatformToEdit == null) return;
        if(PlatformToEdit.PlatformRoute.Count > 0)
        {
            var x = PlatformToEdit.PlatformRoute[PlatformToEdit.PlatformRoute.Count - 1].x;
            var z = PlatformToEdit.PlatformRoute[PlatformToEdit.PlatformRoute.Count - 1].z;
            var y = Plane.transform.position.y;

            List<Vector3> platforms = new List<Vector3>();

            platforms.Add(new Vector3(x + 1, y, z));
            platforms.Add(new Vector3(x - 1, y, z));
            platforms.Add(new Vector3(x, y, z - 1));
            platforms.Add(new Vector3(x, y, z + 1));

            foreach (var plat in platforms)
            {
                if (Validator.IsPlatformAllowed(PlatformToEdit, plat))
                {
                    var obj = Instantiate(PhantomPlatform, plat, Quaternion.identity);
                    obj.layer = 2;
                    PhantomPlatforms.Add(obj);
                }
            }
        }

    }


    private bool IsPlatformAllowed(Vector3 location)
    {
        var x = PlatformToEdit.gameObject.transform.position.x;
        var z = PlatformToEdit.gameObject.transform.position.z;
        var y = Plane.transform.position.y;

        List<Vector3> platforms = new List<Vector3>();

        platforms.Add(new Vector3(x + 1, y, z));
        platforms.Add(new Vector3(x - 1, y, z));
        platforms.Add(new Vector3(x, y, z - 1));
        platforms.Add(new Vector3(x, y, z + 1));

        return platforms.Contains(location) || PlatformToEdit.PlatformRoute.Count == 0;
    }
    public void RemovePhantoms(int n = 0)
    {
        foreach(var platform in PhantomPlatforms)
        {
            Destroy(platform);
        }
    }
    #endregion
    public void OnEditModeSwitch()
    {
        RemovePhantoms();

        if (PlatformToEdit != null) ChangeRouteLayer(PlatformToEdit, 0);
        PlatformToEdit = null;


        _movingPlatforms = new List<MovingPlatformScript>(FindObjectsOfType<MovingPlatformScript>());
        var focused = EventManager.GetInstance().focusedObject;


        if (focused != null) PlatformToEdit = focused.GetComponent<MovingPlatformScript>();

        if (PlatformToEdit != null)
        {

            if (PlatformToEdit.PlatformRoute.Count == 0) AddRoutePoint();


            enabled = true;
            ChangeRouteLayer(PlatformToEdit, 1);

        }
        else
        {
            enabled = false;
        }
    }


    public void Start()
    {
        enabled = false;
        _visualizer = FindAnyObjectByType<PlatformVisualizer>();


        EventManager.GetInstance().MovingPlatformEditModeEnter += OnEditModeSwitch;
        EventManager.GetInstance().MovingPlatformEditModeExit += OnEditModeSwitch;

        EventManager.GetInstance().PlaneLayerChanged += RemovePhantoms;
        EventManager.GetInstance().PlaneLayerChanged += SpawnPhantomPlatforms;

    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EventManager.GetInstance().OnMovingPlatformEditModeExit();
        }
        if(Input.GetMouseButtonDown(0))
        {
            AddRoutePoint();
        }
    }

    private void ChangeRouteLayer(MovingPlatformScript platform, int layer)
    {
        LineRenderer ln = platform.GetComponent<LineRenderer>();

        if (ln != null)
        {
            ln.sortingOrder = layer;
        }
    }
}
