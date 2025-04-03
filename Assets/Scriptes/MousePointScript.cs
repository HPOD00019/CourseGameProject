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
        { // ���������, ��� Camera.main �� null
            Vector3 mousePosition = Input.mousePosition; // �������� ���������� ���� �� ������
            mousePosition.z = Camera.main.nearClipPlane; // ������������� Z-���������� (��������, �� ������� ��������� ��������� ������)

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // ����������� � ������� ����������
            Debug.Log("World Position: " + worldPosition); // ������� ������� �������
        }
        else
        {
            Debug.Log("Camera.main is null. Please ensure there is a camera with the 'MainCamera' tag in the scene.");
        }

    }
}
