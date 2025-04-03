using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.MovingPlatformRoute
{
    public class IRoutePoint : MonoBehaviour
    {
        private Color _currentColor;
        private Coroutine _colorChangeCoroutine;
        private Renderer _renderer;
        [SerializeField] private float _colorChangingTime;
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _currentColor = _renderer.material.color;
        }
        private void Update()
        {
        }
        public void LightPoint(Color newColor)
        {
            _colorChangeCoroutine = StartCoroutine(ChangeColor(newColor));
        }
        private IEnumerator ChangeColor(Color newColor)
        {
            float timeElapsed = 0f;
            while (timeElapsed < _colorChangingTime)
            {
                _renderer.material.color = Color.Lerp(_currentColor, newColor, timeElapsed / _colorChangingTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            _renderer.material.color = newColor;
            _colorChangeCoroutine = null;
            yield break;
        }
    }
}

