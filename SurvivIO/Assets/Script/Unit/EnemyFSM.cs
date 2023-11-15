using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Move,
    Attack,
    Chase
}

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] State _currentState;
    [SerializeField] State _previousState;
    [SerializeField] WeaponShoot _weaponHandler;
    //time
    float _minWaitTime = 3;
    float _maxWaitTime = 5;
    float _waitTime;
    float _timeTick;

    Vector3 _destPoint = Vector3.zero;
    float _speed = 3;

    public Transform Target { get; set; }
    float _chaseDist = 5;
    float _chaseSpeed = 4.0f;

    void Start()
    {
        _currentState = State.Idle;

        _waitTime = Random.Range(_minWaitTime, _maxWaitTime);
        _timeTick = 0;

        _weaponHandler = this.GetComponent<WeaponShoot>();
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Idle:    IdleUpdate(); break;
            case State.Move:    MoveUpdate(); break;
            case State.Chase:   ChaseUpdate(); break;
            case State.Attack:  AttackUpdate(); break;
        }
    }

    void IdleUpdate()
    {
        if(Target != null)
        {
            _timeTick = 0;
            ChangeState(State.Chase);
            return;
        }

        _timeTick += Time.deltaTime;
        if( _timeTick > _waitTime )
        {
            _timeTick = 0;
            _waitTime = Random.Range( _minWaitTime, _maxWaitTime );

            ChangeState(State.Move);
            return;
        }
    }

    void MoveUpdate()
    {
        if (Target != null)
        {
            ChangeState(State.Chase);
            return;
        }

        if (_destPoint == Vector3.zero )
        {
            _destPoint = new Vector3(
                Random.Range(-25, 25), // x
                Random.Range(-25, 25), // y
                0);
        }

        float step = _speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                            _destPoint,
                                                            step);
        

        if(Vector3.Distance(this.transform.position, _destPoint) <= 0.1f)
        {
            _destPoint = Vector3.zero;
            ChangeState(State.Idle);
            return;
        }

    }

    void ChaseUpdate()
    {
        if(Target == null) 
        {
            ChangeState(_previousState);
            return;
        }

        if(Vector2.Distance(this.transform.position, Target.position) > _chaseDist)
        {
            float step = _chaseSpeed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, Target.transform.position, step);
        }
        RotateTowardsTarget();
        if (_weaponHandler._canFire)
            StartCoroutine(Fire());
    }

    void AttackUpdate()
    {
        
    }

    void ChangeState(State state)
    {
        if(_currentState == state) 
            return;

        _previousState = _currentState;
        _currentState = state;
        Debug.Log("Change State: " + _currentState);
    }

    void RotateTowardsTarget()
    {
        Vector2 direction = Target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    public IEnumerator Fire()
    {
        if(_weaponHandler._equippedWeapon._name == "Shotgun" )
        {
            for(int i = 0; i < 8; i++)
            {
                GameObject bullet = Instantiate(_weaponHandler._bulletPrefab, _weaponHandler._nozzle.position, _weaponHandler._nozzle.rotation);
                int randomSpread = Random.Range(-2, 2);
                bullet.GetComponent<BulletMovement>().InitBullet(this.gameObject, _weaponHandler._equippedWeapon._damage, randomSpread);
            }
        }
        else
        {
            GameObject bullet = Instantiate(_weaponHandler._bulletPrefab, _weaponHandler._nozzle.position, _weaponHandler._nozzle.rotation);
            bullet.GetComponent<BulletMovement>().InitBullet(this.gameObject, _weaponHandler._equippedWeapon._damage, 0);
        }

        _weaponHandler._canFire = false;
        yield return new WaitForSeconds(_weaponHandler._equippedWeapon._fireRate);
        _weaponHandler._canFire = true;
    }
}
