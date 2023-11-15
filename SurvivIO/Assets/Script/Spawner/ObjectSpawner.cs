using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _weaponPickups, _ammoPickups, _obstaclePrefabs;
    int _numOfObjectsToSpawn; 

    private void Start()
    {
        _numOfObjectsToSpawn = 20;
        for(int i = 0; i < _numOfObjectsToSpawn; i++)
        {
            int objToSpawn = Random.Range(0, 101);

            if(objToSpawn <= 70)
            {
                SpawnObjects(_ammoPickups);
                Debug.Log("Spawned ammo");
            }
            else
            {
                SpawnObjects(_weaponPickups);
                Debug.Log("Spawned weapon");
            }
        }
    }

    void SpawnObjects(GameObject[] prefabs)
    {
        Vector2 randomPos = new Vector2(Random.Range(-20, 20),
                                        Random.Range(-20, 20));

        int randomValue = Random.Range(0, prefabs.Length);            
        GameObject pickup = Instantiate(prefabs[randomValue], randomPos, prefabs[randomValue].transform.rotation);
    }
}