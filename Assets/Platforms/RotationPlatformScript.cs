using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RotationPlatformScript : MonoBehaviour
{
    private bool _makingRound;
    public float rotationSpeed = 60f; // Скорость поворота в градусах в секунду
    public float rotationAngle = 360f; // Угол поворота в градусах

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
            // Рассчитываем поворот за текущий кадр
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            // Ограничиваем поворот до нужного угла
            if (totalRotation + rotationThisFrame > rotationAngle)
            {
                rotationThisFrame = rotationAngle - totalRotation;
            }

            transform.Rotate(Vector3.right, rotationThisFrame); // Поворачиваем вокруг оси Y
            totalRotation += rotationThisFrame; // Обновляем общий угол поворота

            yield return null; // Ждем следующего кадра
        }
        _makingRound = false;
        yield break;
    }
}
