using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RotationPlatformScript : MonoBehaviour
{
    private bool _makingRound;
    public float rotationSpeed = 60f; 
    public float rotationAngle = 360f; 

    private void Start()
    {
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!_makingRound)
            {
                StartCoroutine(MakeRound());
                _makingRound = true;
            }
        }
    }
    private IEnumerator MakeRound()
    {
        float totalRotation = 0f;
        while (totalRotation < rotationAngle)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            if (totalRotation + rotationThisFrame > rotationAngle)
            {
                rotationThisFrame = rotationAngle - totalRotation;
            }

            transform.Rotate(Vector3.right, rotationThisFrame); 
            totalRotation += rotationThisFrame; 

            yield return null; 
        }
        _makingRound = false;
        yield break;
    }
}
