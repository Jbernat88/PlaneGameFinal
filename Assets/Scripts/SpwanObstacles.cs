using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanObstacles : MonoBehaviour
{
    //Array of rocks
    public GameObject[] rockPrefabs;
    public Vector3 spawnPosition = new Vector3(0, 0, 35);
    private float xRange = 25f;
    private float yRange = 25f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 2, 0.1f);// wait 2 seconds to start and 0.1 seconds to repeat the instantiationwait 2 seconds to start and 0.1 seconds to repeat the instantiation
    }

    // Random position in X y Y
    public Vector3 RandomSpawnPosition()
    {
        float randomX = Random.Range(-xRange, xRange);

        float randomY = Random.Range(-yRange, yRange);
        return new Vector3(randomX, randomY, transform.position.z);
    }

    //Instancie the obstacle
    public void SpawnAnimal()
    {
        int randomIndex = Random.Range(0, rockPrefabs.Length);

        spawnPosition = RandomSpawnPosition();
        Instantiate(rockPrefabs[randomIndex], spawnPosition, rockPrefabs[randomIndex].transform.rotation);
    }

}



