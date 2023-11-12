using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _pickupPrefab;
    [SerializeField] GameObject[] _obstaclePrefabs;

    private void Start()
    {
        while(_numOfSpawnedObj < _numOfObjToSpawn)
        {
            SpawnObjects(_pickupPrefab);
            //SpawnObjects(_obstaclePrefabs);
        }
    }

    Vector2 prevPos, newPos;
    int _numOfSpawnedObj, _numOfObjToSpawn = 20; 

    void SpawnObjects(GameObject[] prefabs)
    {
        Vector2 randomPos = new Vector2(Random.Range(-20, 20),
                                        Random.Range(-20, 20));
        newPos = randomPos;

        if (Vector2.Distance(newPos, prevPos) > 10)
        {
            int randomValue = Random.Range(0, prefabs.Length);            
            GameObject pickup = Instantiate(prefabs[randomValue], newPos, Quaternion.identity);
            prevPos = newPos;
            _numOfSpawnedObj++;
        }
    }
}