using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class SpawnEntities : MonoBehaviour
{
    [SerializeField] private GameObject[] rocks;
    [SerializeField] private GameObject[] enemies;
    private const float SPAWN_DELAY = 2f;
    private float spawnTime;
    private Random _random;
    private float planeSizeX;
    private float planeSizeZ;


    void Start()
    {
        Mesh planeMesh = GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        _random = new Random(0x6E624EB7u);
        spawnTime = Time.time;
        planeSizeX = bounds.size.x;
        planeSizeZ = bounds.size.z;
    }

    void FixedUpdate()
    {
        if (spawnTime < Time.time)
        {
            Spawn();
            spawnTime = Time.time + SPAWN_DELAY;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < 2; i++)
        {
            int index = _random.NextInt(rocks.Length);
            (float randomPointX, float randomPointZ) = GetRandomCoords();
            GameObject lastEntity = Instantiate(rocks[index],
                new Vector3(randomPointX, transform.position.y, randomPointZ), Quaternion.identity);
            Destroy(lastEntity, 20f);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = _random.NextInt(enemies.Length);
            (float randomPointX, float randomPointZ) = GetRandomCoords();
            GameObject lastEntity = Instantiate(enemies[index],
                new Vector3(randomPointX, transform.position.y, randomPointZ), Quaternion.identity);
            Destroy(lastEntity, 20f);
        }
    }

    private (float randomPointX, float randomPointZ) GetRandomCoords()
    {
        float randomPointX =
            _random.NextFloat(transform.position.x - planeSizeX, transform.position.x + planeSizeX);
        float randomPointZ =
            _random.NextFloat(transform.position.z - planeSizeZ, transform.position.z + planeSizeZ);
        return (randomPointX, randomPointZ);
    }
}