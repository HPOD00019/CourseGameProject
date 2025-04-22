using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelEditor.MovingPlatformRoute
{
    public class RouteVisualizer : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public List<Vector3> Route;

        public Color LineBeginColor = Color.red;
        public Color LineEndColor = Color.red;

        public float width = 0.1f;

        public void Awake()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        public void DrawRoute()
        {

            lineRenderer.positionCount = Route.Count;
            lineRenderer.widthMultiplier = width;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

            lineRenderer.startColor = LineBeginColor;
            lineRenderer.endColor = LineEndColor;


            lineRenderer.SetPositions(Route.ToArray());
        }
    }
}

