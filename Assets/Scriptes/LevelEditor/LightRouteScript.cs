using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRouteScript : MonoBehaviour
{
    private Renderer _renderer;
    private Coroutine _lightCoroutine;
    private Color _currentColor;
    public float ColorChangingTime;
    private float time;
    
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _currentColor = _renderer.material.color;
        ColorChangingTime = 0.0f;
    }
    void Update()
    {
        
    }
    public void ChangeLightColor(Color newColor)
    {
        _lightCoroutine = StartCoroutine(LightCoroutine(newColor));
    }
    private IEnumerator LightCoroutine(Color newColor)
    {
        float timeElapsed = 0f;
        while (timeElapsed < ColorChangingTime)
        {
            _renderer.material.color = Color.Lerp(_currentColor, newColor, timeElapsed / ColorChangingTime);
            timeElapsed += Time.deltaTime;
            yield return null; 
        }
        _renderer.material.color = newColor;
        _lightCoroutine = null;
        yield break;
    }
}
