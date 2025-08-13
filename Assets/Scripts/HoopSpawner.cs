using UnityEngine;

public class HoopSpawner : MonoBehaviour
{
    public GameObject hoopPrefab; // store the hoops Prefab
    public Transform player; // store the transform component of Player

    public int hoopsAhead = 5; // How many Hoops ahead of the player
    public float distanceBetweenHoops = 50f;
    public float verticalVariation = 10f;
    public float horizontalVariation = 10f;

    private Vector3 lastHoopPosition;

    private void Start()
    {
        // Start a little ahead of the player
        lastHoopPosition = player.position + player.forward * 100f;

        // Spawn initial hoops
        for(int i = 0; i < hoopsAhead; i++)
        {
            SpawnNextHoop();
        }
    }

    public void SpawnNextHoop()
    {
        // Small random variation so hoops aren't perfectly straight
        Vector3 offset = player.right * Random.Range(-horizontalVariation, horizontalVariation) + player.up * Random.Range(-verticalVariation, verticalVariation);

        // position is based on last hoop position
        Vector3 spawnPos = lastHoopPosition + player.forward * distanceBetweenHoops + offset;

        // create the Hoop
        GameObject newHoop = Instantiate(hoopPrefab, spawnPos, Quaternion.LookRotation(-player.forward));

        //Let the hoops know who spawned it
        newHoop.GetComponent<Hoop>().spawner = this; // so hoops can tell spawner to make another

        //update the next Spawn
        lastHoopPosition = spawnPos;
    }
}

