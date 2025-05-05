using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RotationPlatformScript : MonoBehaviour
{
    private bool _makingRound;

    public float rotationSpeed = 300f; 
    public float rotationAngle = 360f;
    private Coroutine _roundCoroutine;

    private bool _rotated;
    private Vector3 _rotationVector = Vector3.right;

    private Quaternion quart;
    private void Start()
    {
        _rotationVector = Vector3.right;
        quart = transform.localRotation;

        RotationPlatformScript[] platforms = FindObjectsOfType<RotationPlatformScript>();
        foreach (var plat in platforms)
        {
            plat.SwitchTurn();
        }
        EventManager.GetInstance().OnEditRotationPlatform(this);
    }


    private void Update()
    {
        if (!_makingRound)
        {
            _makingRound = true;
            _roundCoroutine = StartCoroutine(MakeRound());
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

            transform.Rotate(_rotationVector, rotationThisFrame); 
            totalRotation += rotationThisFrame; 

            yield return null; 
        }

        yield return new WaitForSeconds(2f);
        _makingRound = false;

        yield break;
    }


    public void SwitchTurn()
    {
        if (_roundCoroutine != null) StopCoroutine(_roundCoroutine);
        _makingRound = false;
        transform.rotation = quart;
    }


    public void Rotate()
    {
        RotationPlatformScript[] platforms = FindObjectsOfType<RotationPlatformScript>();
        
        foreach (var plat in platforms)
        {
            plat.SwitchTurn();
        }

        if (_rotated)
        {
            transform.Rotate(0f, 90f, 0f);
            _rotationVector = Vector3.back;
            quart = transform.localRotation;
        }
        else
        {
            _rotationVector = Vector3.forward;
            transform.Rotate(0f, -90f, 0f);
            quart = transform.localRotation;
        }

        _makingRound = false;
        _rotated = !_rotated;
    }
}
