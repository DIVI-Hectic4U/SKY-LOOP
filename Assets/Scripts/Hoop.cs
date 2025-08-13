using UnityEngine;

public class Hoop : MonoBehaviour
{
    
    [HideInInspector] public HoopSpawner spawner;

    //Trigger detection (if hoop collider is set to Is Trigger)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[TRIGGER ENTER] {other.name} entered {name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player passed through the hoop!");
            Debug.Log("Destroying: " + transform.root.name);
            //Add scoring Logic here later
            
            // Tell spawner to make another 
            if(spawner != null)
            {
                spawner.SpawnNextHoop();
            }

            //Remove this Hoop
            Destroy(transform.root.gameObject);
        }
    }



    

    

    
}

