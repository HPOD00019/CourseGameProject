using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerEditor : MonoBehaviour
{

    public KeyCode LayerUp;
    public KeyCode LayerDown;

    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    public GameObject platform5;

    public GameObject platform1Phantom;
    public GameObject platform2Phantom;
    public GameObject platform3Phantom;
    public GameObject platform4Phantom;
    public GameObject Platform5Phantom;

    private void Update()
    {
        if (Input.GetKeyDown(LayerUp))
        {
            EventManager.GetInstance().OnPlaneLayerChanged(1);
        }

        if (Input.GetKeyDown(LayerDown))
        {
            EventManager.GetInstance().OnPlaneLayerChanged(-1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GetComponent<Spawner>().ObjectToSpawn = platform1;
            GetComponent<PlatformVisualizer>().SetNewObjectToTrack(platform1Phantom);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GetComponent<Spawner>().ObjectToSpawn = platform2;
            GetComponent<PlatformVisualizer>().SetNewObjectToTrack(platform2Phantom);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GetComponent<Spawner>().ObjectToSpawn = platform3;
            GetComponent<PlatformVisualizer>().SetNewObjectToTrack(platform3Phantom);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            GetComponent<Spawner>().ObjectToSpawn = platform4;
            GetComponent<PlatformVisualizer>().SetNewObjectToTrack(platform4Phantom);
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            GetComponent<Spawner>().ObjectToSpawn = platform5;
            GetComponent<PlatformVisualizer>().SetNewObjectToTrack(Platform5Phantom);
        }
    }
}
