using UnityEngine;

public class SpinPropeller : MonoBehaviour
{
    private float speed = 50f;
    private float multiplier = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * speed );

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(Vector3.right * speed * multiplier );
        }
    }
}
