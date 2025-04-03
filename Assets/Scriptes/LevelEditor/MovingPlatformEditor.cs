using System.Collections;
using System.Collections.Generic;
using LevelEditor.MovingPlatformRoute;
using UnityEngine;

public class MovingPlatformEditor : MonoBehaviour
{
    private PlatformVisualizer _visualizer;
    public MovingPlatformScript PlatformToEdit;

    #region Add Route
    public void AddRoutePoint()
    {
        AddRoutePoint((Vector3)_visualizer.GetMouseWorldPosition());
    }
    public void AddRoutePoint(Vector3 newPoint)
    {
        PlatformToEdit.PlatformRoute.Add(newPoint);
    }
    #endregion
    public void OnEditModeTurn()
    {
        EventManager.GetInstance().OnMovingPlatformEditModeEnter();
        gameObject.SetActive(true);
    }
    public void OnEditModeTurnOff()
    {
        EventManager.GetInstance().OnMovingPlatformEditModeExit();

        gameObject.SetActive(false);
    }
    private void Start()
    {
        _visualizer = FindAnyObjectByType<PlatformVisualizer>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            OnEditModeTurnOff();
        }
        if(Input.GetMouseButtonDown(0))
        {
            AddRoutePoint();
        }
    }
}
