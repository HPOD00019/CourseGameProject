using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointScript : MonoBehaviour
{
    Vector3 diference;
    void Start()
    {
        
    }
    //void Update()
    //{

    //    Camera cam = Camera.main;
    //    Vector3 position = Input.mousePosition;
    //    Debug.Log(cam == null);
    //    diference = cam.ScreenToWorldPoint(position);
    //    Debug.Log(diference);
    //}
    void Update()
    { 
        if (Camera.main != null)
        { // Проверяем, что Camera.main не null
            Vector3 mousePosition = Input.mousePosition; // Получаем координаты мыши на экране
            mousePosition.z = Camera.main.nearClipPlane; // Устанавливаем Z-координату (например, на ближней плоскости отсечения камеры)

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // Преобразуем в мировые координаты
            Debug.Log("World Position: " + worldPosition); // Выводим мировую позицию
        }
        else
        {
            Debug.Log("Camera.main is null. Please ensure there is a camera with the 'MainCamera' tag in the scene.");
        }

    }
}
