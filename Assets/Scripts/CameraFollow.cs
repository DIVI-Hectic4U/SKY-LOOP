using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public KeyCode boostKey = KeyCode.Space;

    [Header("FOV Settings")]
    public float normalFOV = 60f;
    public float boostFOV = 70f;
    public float boostFOVBurst = 80f;
    public float burstDuration = 0.2f;
    private float burstTimer = 0f;
    private bool wasBoostingLastFrame = false;

    [Header("Shake Settigns")]
    public float shakeIntensity = 0.05f;
    public float shakeSpeed = 20f;

    private Camera cam;


    public Transform target;
    public Vector3 localOffset = new Vector3(0, 3, -10);
    public float smoothSpeed = 5f;


    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = normalFOV;
    }
    void LateUpdate()
    {
        if (!target) return;
        
        bool boosting = Input.GetKey(boostKey);

        //Detect boost press for FOV burst
        if(boosting && !wasBoostingLastFrame)
        {
            burstTimer = burstDuration;
        }
        wasBoostingLastFrame = boosting;

        // Calculate desired position in the plane's local space
        Vector3 desiredPosition = target.TransformPoint(localOffset);

        //Add shake if Boosting
        if (boosting)
        {
            float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            float shakeY = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            desiredPosition += target.right * shakeX + target.up * shakeY;
        }


        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // Look ahead of the plane instead of directly at it
        Vector3 lookTarget = target.position + target.forward * 50f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), Time.deltaTime * smoothSpeed);

        //Handle FOV changes
        float targetFOV = normalFOV;
        if (boosting)
        {
            if(burstTimer > 0)
            {
                targetFOV = boostFOVBurst;
                burstTimer -= Time.deltaTime;
            }
            else
            {
                targetFOV = boostFOV;
            }
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * smoothSpeed);
    }
}
