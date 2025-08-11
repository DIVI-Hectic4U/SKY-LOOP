using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float baseSpeed = 50f; // Constant forward Speed
    public float boostMultiplier = 2f; // speed multiplier when boosting
    public float acceleration = 2f; // smooth speed change

    [Header("Rotation Settings")]
    public float pitchSpeed = 60f; // Up/Down
    public float yawSpeed = 60f; // left/right
    public float rollSpeed = 80f; // tilt

    private float currentSpeed;
    private float targetSpeed;

    private void Start()
    {
        currentSpeed = baseSpeed;
        targetSpeed = baseSpeed;
    }

    private void Update()
    {
        HandleBoost();
        HandleRotation();
        MoveForward();
    }

    void HandleBoost()
    {
        //if Space is held, target speed is boosted
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetSpeed = baseSpeed * boostMultiplier;
        }
        else
        {
            targetSpeed = baseSpeed;

        }

        //Smooth transition between Speeds
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
    }

    void HandleRotation()
    {
        float pitch = -Input.GetAxis("Vertical"); // W/S or Up/Down
        float yaw = Input.GetAxis("Horizontal"); // A/D or left/right

        float roll = 0f;
        if (Input.GetKey(KeyCode.Q)) roll = 1f;
        if (Input.GetKey(KeyCode.E)) roll = -1f;

        //Apply full rotation freedom
        transform.Rotate(pitch * pitchSpeed * Time.deltaTime, yaw * yawSpeed * Time.deltaTime, roll * rollSpeed * Time.deltaTime, Space.Self);
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);
    }
}
