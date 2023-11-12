using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] List<Enemy>  _enemyList = new List<Enemy>();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-20, 20),
                                        UnityEngine.Random.Range(-20, 20));
        GameObject enemy = (GameObject) Instantiate(_enemyPrefab, randomPos, Quaternion.identity);

        _enemyList.Add(enemy.GetComponent<Enemy>());
        enemy.GetComponent<Enemy>().Died += RemoveEnemy;
    }

    void RemoveEnemy(GameObject obj)
    {
        _enemyList.Remove(obj.GetComponent<Enemy>());
        obj.GetComponent<Enemy>().Died -= RemoveEnemy;
        Destroy(obj);
    }

}
