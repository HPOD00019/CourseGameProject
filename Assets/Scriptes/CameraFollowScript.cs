using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float smoothSpeed = 0.125f; 
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -5);

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
