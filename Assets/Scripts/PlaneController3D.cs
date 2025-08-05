using UnityEngine;

public class PlaneController3D : MonoBehaviour
{
    [Header("Throttle Settings")]
    public float throttle = 10.0f;
    public float maxThrottle = 50.0f;
    public float minThrottle = 0f;
    public float throttleChangeRate = 20.0f;

    [Header("Verticle Settings")]
    public float verticalInputSpeed = 15.0f;
    public float verticalAcceleration = 5f; //  How fast input affects vertical velocity
    public float verticalVelocity = 0f; // How fast plane Catches upto Target

    [Header("Yaw Settings")]
    public float yawSpeed = 60.0f; // degree per second

    [Header("Tilting")]
    public float pitchAmount = 30.0f; // W/S OR Up/Down visual tilt
    public float rollAmount = 45.0f; // Left/Right OR A/D banking tilt
    public float tiltSmoothing = 5f;


    private Rigidbody planeRb;

    private void Start()
    {
        planeRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleThrottle();
        HandleVerticalMovement();
        HandleYaw();
        MovePlane();
    }

    void HandleThrottle()
    {
        //Increase Throttle
        if (Input.GetKey(KeyCode.E))
        {
            throttle += throttleChangeRate * Time.deltaTime;
        }

        //Decrease Throttle
        if (Input.GetKey(KeyCode.Q))
        {
            throttle -= throttleChangeRate * Time.deltaTime;
        }

        //Clamp Throttle
        throttle = Mathf.Clamp(throttle,minThrottle,maxThrottle);
    }

    void HandleVerticalMovement()
    {
        float input = Input.GetAxis("Vertical"); // W/S OR Arrow keys
        float targetYVelocity = input * verticalInputSpeed;

        //Smooth Acceleration
        verticalVelocity = Mathf.Lerp(verticalVelocity, targetYVelocity, verticalAcceleration * Time.deltaTime);

    }

    void HandleYaw()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        //Rotate plane around Y axis (yaw)
        transform.Rotate(0f, horizontalInput * yawSpeed * Time.deltaTime, 0f);
    }

    void MovePlane()
    {
        // Move Forward based on Throttle
        Vector3 forwardMove = transform.forward * throttle * Time.deltaTime;
        Vector3 verticalMove =  Vector3.up * verticalVelocity * Time.deltaTime;

        Vector3 newPosition = planeRb.position + forwardMove + verticalMove;
        planeRb.MovePosition(newPosition);

        // Tilt & Roll Amount of Plane visually based on current vertical Speed
        float pitch = -verticalVelocity / verticalInputSpeed * pitchAmount;
        float roll = -Input.GetAxis("Horizontal") * rollAmount;

        Quaternion targetRot = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y, roll);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, tiltSmoothing * Time.deltaTime);
    }
}
