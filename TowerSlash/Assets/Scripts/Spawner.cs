using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyMaxSpawn = 10;
    GameObject lastEnemy;
    public static Spawner instance;

    private void Start()
    {
        Spawner.instance = this;

        Vector3 newPos = new Vector3 (0, 0, 0);
        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab, newPos, Quaternion.identity);

        for (int i = 0 ; i < enemyMaxSpawn; i++)
        {
            enemyObj = SpawnEnemy(enemyObj);
            lastEnemy = enemyObj;
        }
    }

    public void RemoveEnemy(GameObject enemyObj)
    {
        Destroy(enemyObj);
        lastEnemy = SpawnEnemy(lastEnemy);
    }

    private GameObject SpawnEnemy(GameObject nextEnemy)
    {
        float randomDistance = Random.Range(5.0f, 10.0f);
        Vector3 newPos = new Vector3 (0, nextEnemy.transform.position.y + randomDistance, 0);

        GameObject enemyObj = (GameObject)Instantiate(enemyPrefab, newPos, Quaternion.identity);

        return enemyObj;
    }

}
