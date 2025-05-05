using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatformsEditor : MonoBehaviour
{
    private RotationPlatformScript platform;
    public void Start()
    {
        EventManager.GetInstance().EditRotation += SetRotationPlatform;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(platform != null) platform.Rotate();
        }
    }
    public void SetRotationPlatform(RotationPlatformScript platform)
    {
        this.platform = platform;
    }
}
