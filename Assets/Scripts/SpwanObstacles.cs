using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanObstacles : MonoBehaviour
{
    public GameObject[] rockPrefabs;
    public Vector3 spawnPosition = new Vector3(0, 0, 35);
    private float xRange = 15f;
    private float yRange = 15f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 2, 0.1f);// tarda 2 segundos al empezar y luego 1 
    }

    //Posicion Random en X y Y
    public Vector3 RandomSpawnPosition()
    {
        float randomX = Random.Range(-xRange, xRange);
        

        float randomY = Random.Range(-yRange, yRange);
        return new Vector3(randomX, randomY, transform.position.z);
    }

    //Instancia los obstaculos
    public void SpawnAnimal()
    {
        int randomIndex = Random.Range(0, rockPrefabs.Length);

        spawnPosition = RandomSpawnPosition();
        Instantiate(rockPrefabs[randomIndex], spawnPosition, rockPrefabs[randomIndex].transform.rotation);
    }

}



