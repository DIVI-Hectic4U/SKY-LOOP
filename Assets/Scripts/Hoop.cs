using UnityEngine;

public class Hoop : MonoBehaviour
{
    public int scoreValue = 10;

    //Trigger detection (if hoop collider is set to Is Trigger)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[TRIGGER ENTER] {other.name} entered {name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player passed through the hoop!");
            //Add scoring Logic here later
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[TRIGGER EXIT] {other.name} exited {name}");
    }

        //Collision detection (if hoop Collider is NOT a trigger)
        private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[COLLISION ENTER] {collision.gameObject.name} collide with {name}");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the Hoop!");
            //Handle collision logic here later
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"[COLLISION EXIT] {collision.gameObject.name} left {name}");
    }
}

