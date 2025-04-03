using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RouteVisScript : MonoBehaviour
{
    public Color DefaultColor;
    public List<LightRouteScript> Points;
    public GameObject ObjectToSpawn;
    public float TimeToLightPoint;
    private GameObject _currentObjectToSpawn;
    private int _currentPointToLight;
    private List<LightRouteScript> _tale;
    public int TaleLength;
    public Color RouteColor;
    private bool isPaused;
    private bool n = false;
    private List<float> _taleColorPoints;
    private void Start()
    {
        _taleColorPoints = new List<float>();
        CalculateTalePointsColorsIntensity();
        DefaultColor = Color.gray;
        RouteColor = Color.red;
        Points = new List<LightRouteScript>();
        for (int i = 0; i < 300; i++)
        {
            _currentObjectToSpawn = Instantiate(ObjectToSpawn, new Vector3(i, 0, 0), Quaternion.identity);
            Points.Add(_currentObjectToSpawn.GetComponent<LightRouteScript>());
        }
        _currentPointToLight = 4;
    }
    private void Update()
    {
        if((Input.GetMouseButtonDown(0))) n = true;
        if (n)
        {
            if (!isPaused)
            {
                LightUpPoint(_currentPointToLight);
                StartCoroutine(PauseCoroutine(TimeToLightPoint));
                _currentPointToLight++;
            }
        }
    }
    private void LightUpPoint(int pointNumber)
    {
        Color NewColor = RouteColor;
        int _currentIndex = pointNumber;
        Points[pointNumber].ChangeLightColor(NewColor);
        //NewColor = Color.Lerp(DefaultColor, RouteColor, 0.5f);
        //Points[pointNumber-1].ChangeLightColor(NewColor);
        //NewColor = Color.Lerp(DefaultColor, RouteColor, 0.25f);
        //Points[pointNumber-2].ChangeLightColor(NewColor);
        //NewColor = Color.Lerp(DefaultColor, RouteColor, 0.125f);
        //Points[pointNumber-3].ChangeLightColor(NewColor);
        //NewColor = Color.Lerp(DefaultColor, RouteColor, 0f);
        //Points[pointNumber-1].ChangeLightColor(NewColor);
        foreach(float i in _taleColorPoints)
        {
            _currentIndex--;
            NewColor = Color.Lerp(DefaultColor, RouteColor, i);
            Points[_currentIndex].ChangeLightColor(NewColor);
            
        }
    }
    IEnumerator PauseCoroutine(float PauseTime)
    {
        isPaused = true;
        yield return new WaitForSeconds(PauseTime);
        isPaused = false;
    }
    private void CalculateTalePointsColorsIntensity()
    {
        int currentPoint = 0;
        while (_taleColorPoints.Count < TaleLength)
        {
            currentPoint++;
            _taleColorPoints.Append(1 / (Pow(2, currentPoint)));
        }
        _taleColorPoints.Append(0);
    }
/// <summary>
/// 
/// </summary>
/// <param name="x">Основание</param>
/// <param name="y">Показатель</param>
/// <returns></returns>
    private float Pow(int x, int y)
    {
        float ans = 1;
        while (y > 0)
        {
            ans *= x;
            y--;
        }
        return ans;
    }
}
