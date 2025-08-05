using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Stores the Plane to Follow


    public Vector3 followOffset = new Vector3(0f, 5f, -15f); // Default Camera offset
    public float followSpeed = 5f; // How smoothly The Camera will Follow 
    public float rotationSpeed = 2f; // How Smoothly The Camera rotates to match


    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        //Desired Position
        Vector3 desiredPosition = target.position + target.TransformDirection(followOffset);

        //Smoothly move to that position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        //rotate camera to look at the plane 
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
