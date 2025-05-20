using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatformScript : MonoBehaviour
{
    public Vector3 location;
    public float ResurrectionTime;
    public float Delay;
    private Quaternion rotation;
    public IEnumerator FallCoroutine()
    {
        yield return new WaitForSeconds(Delay);
        var rb = gameObject.AddComponent<Rigidbody>();

        yield return new WaitForSeconds(ResurrectionTime);

        transform.position = location;
        transform.rotation = rotation;

        if (rb != null) Destroy(rb);

        yield break;
    }


    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    void Start()
    {
        location = transform.position;
        ResurrectionTime = 2;
        rotation = transform.rotation;

        if(gameObject.GetComponent<Rigidbody>() != null ) Destroy(gameObject.GetComponent<Rigidbody>());
    }
    void Update()
    {
        
    }
    public void Fall()
    {
        StartCoroutine(FallCoroutine());
    }
}
