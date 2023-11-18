using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponPickups, _ammoPickups, _obstaclePrefabs, _healthPickup;

    private int _numOfObjectsToSpawn; 

    private void Start()
    {
        _numOfObjectsToSpawn = 100;
        for(int i = 0; i < _numOfObjectsToSpawn; i++)
        {
            int objToSpawn = Random.Range(0, 101);
            
            if(objToSpawn <= 10) // 10%
                SpawnObjects(_healthPickup);

            else if (objToSpawn > 10 && objToSpawn <= 75) // 65%
                SpawnObjects(_ammoPickups);

            else
                SpawnObjects(_weaponPickups); // 25%

        }
    }

    private void SpawnObjects(GameObject[] prefabs)
    {
        Vector2 randomPos = new Vector2(Random.Range(-90, 90),
                                        Random.Range(-40, 40));

        int randomValue = Random.Range(0, prefabs.Length);            
        GameObject pickup = Instantiate(prefabs[randomValue], randomPos, prefabs[randomValue].transform.rotation);
    }
}