using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Enemy>  _enemyList = new List<Enemy>();
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
            SpawnEnemy(i);
    }

    private void SpawnEnemy(int number)
    {
        GameObject enemy = (GameObject) Instantiate(_enemyPrefab, _spawnPoints[number]);
        enemy.name = "Enemy " + number;

        _enemyList.Add(enemy.GetComponent<Enemy>());
        enemy.GetComponent<Enemy>().Died += RemoveEnemy;
        ScoreTracker.instance.UpdateEnemiesLeft(_enemyList.Count);
    }

    private void RemoveEnemy(GameObject obj)
    {
        _enemyList.Remove(obj.GetComponent<Enemy>());
        obj.GetComponent<Enemy>().Died -= RemoveEnemy;
        Destroy(obj);
        ScoreTracker.instance.UpdateEnemiesLeft(_enemyList.Count);
        if (_enemyList.Count == 0)
        {
            GameManager.instance.player._isWinner = true;
            StartCoroutine(UIHandler.instance.GoToEndMenu());
        }
    }
    
}
