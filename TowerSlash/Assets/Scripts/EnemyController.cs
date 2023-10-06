using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Green, //0
    Red, //1
    Yellow, //2
    Null //3
}

public class EnemyController : MonoBehaviour
{
    Renderer _ren;
    private EnemyType _enemyType;
    private SwipeDirection _arrowDirection;
    private int _directionValue;
    private Vector3[] _cardinalDirections = new Vector3[4];
    private bool _isRotating = false;
    private PlayerController _player;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerController>();
        _ren = GetComponent<Renderer>();
        _enemyType = (EnemyType)Random.Range(0, (int)EnemyType.Null);
        if (_enemyType == EnemyType.Yellow)
        {
            _isRotating = true;
            StartCoroutine(RandomArrowDirections());
        }

        SetArrowColor(_enemyType);

        _cardinalDirections[0] = new Vector3(0, 0, 0);
        _cardinalDirections[1] = new Vector3(0, 0, 180);
        _cardinalDirections[2] = new Vector3(0, 0, 90);
        _cardinalDirections[3] = new Vector3(0, 0, -90);

        _directionValue = Random.Range(0, (int)SwipeDirection.Null);
        _arrowDirection = (SwipeDirection)_directionValue;
        SetArrowDirection(_arrowDirection);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isRotating = false;
        }
    }

    private void SetArrowDirection(SwipeDirection arrowDirection)
    {
        switch((int)arrowDirection)
        {
            case 0:
            gameObject.transform.eulerAngles = _cardinalDirections[0];
            break;

            case 1:
            gameObject.transform.eulerAngles = _cardinalDirections[1];
            break;

            case 2:
            gameObject.transform.eulerAngles = _cardinalDirections[2];
            break;

            case 3: 
            gameObject.transform.eulerAngles = _cardinalDirections[3];
            break;
        }
    }

    private void SetArrowColor(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Green:
            _ren.material.SetColor("_Color", Color.green);
            break;

            case EnemyType.Red:
            _ren.material.SetColor("_Color", Color.red);
            break;

            case EnemyType.Yellow:
            _ren.material.SetColor("_Color", Color.yellow);
            break; 
        }
    }

    private IEnumerator RandomArrowDirections()
    {
        while(_isRotating)
        {
            int randomDirection = Random.Range(0, _cardinalDirections.Length);
            gameObject.transform.eulerAngles = _cardinalDirections[randomDirection];

            yield return new WaitForSeconds(0.1f);

            _directionValue = randomDirection;
        }
    }

    public void Die()
    {
        Spawner.instance.RemoveEnemy(this.gameObject);
    }

    #region Get/Set
    public int GetArrowDirection()
    {
        return _directionValue;
    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }

    #endregion
}
