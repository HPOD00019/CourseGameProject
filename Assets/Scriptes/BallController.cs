using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f; 
    [SerializeField] private float maxSpeed = 10f;      
    [SerializeField] private float rotationSpeed = 5f;


    private Rigidbody rb;
    private float currentSpeed = 0f;
    private Vector3 moveDirection;

    public bool OnMovingPlatform;

    private float _minimalHeight;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        OnMovingPlatform = false;
    }


    public void Start()
    {

        var n = FindAnyObjectByType<Initializer>();
        if(n != null) _minimalHeight = n.minimalHeight - 10;

    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        if(gameObject.transform.position.y <= _minimalHeight) gameObject.transform.position = new Vector3(0,1,0);
    }

    private void FixedUpdate()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);

            Vector3 force = moveDirection * currentSpeed;
            rb.AddForce(force, ForceMode.Acceleration);

            Vector3 rotationAxis = Vector3.Cross(Vector3.up, moveDirection);
            rb.AddTorque(rotationAxis * rotationSpeed * currentSpeed);
        }
        else
        {
            currentSpeed = Mathf.Max(currentSpeed - acceleration * Time.fixedDeltaTime, 0f);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
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
        if (spawnPlatform != null)
        {
            spawnPlatform.PassLevel();
        }
    }
}