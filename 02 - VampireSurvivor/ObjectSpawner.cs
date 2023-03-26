using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minRadius = 5f;
    public float radius = 10f;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public float maxDistance = 20f;

    float timer = 0f;
    float timerMax = 0f;

    void Start()
    {
        timerMax = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            // spawn object
            float spawnRadius = Random.Range(minRadius, radius);
            float spawnAngle = Random.Range(0, 2 * Mathf.PI);
            Vector3 spawnPosition = new Vector3(spawnRadius * Mathf.Cos(spawnAngle), spawnRadius * Mathf.Sin(spawnAngle), 0);
            spawnPosition += transform.position;
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, transform);
            // reset timer
            timerMax = Random.Range(minSpawnTime, maxSpawnTime);
            timer = timerMax;
        }
        foreach (Transform child in transform)
        {
            if(Vector3.Distance(child.position, transform.position) > maxDistance)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
