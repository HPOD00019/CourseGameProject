using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllScript : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody obj;

    void Start()
    {
        obj = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {

    }
    void FixedUpdate()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            obj.AddForce(Vector3.forward * speed);
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            obj.AddForce(-Vector3.forward * speed);
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            obj.AddForce(Vector3.right * speed);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            obj.AddForce(-Vector3.right*speed);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject tuchedObj = collision.gameObject;
        TrapPlatformScript trapScript = tuchedObj.GetComponent<TrapPlatformScript>();
        SpawnPlatform spawnPlatform = tuchedObj.GetComponent<SpawnPlatform>();

        if (trapScript != null)
        {
            trapScript.Fall();
        }
        if(spawnPlatform != null)
        {
            spawnPlatform.PassLevel();
        }
    }
}
