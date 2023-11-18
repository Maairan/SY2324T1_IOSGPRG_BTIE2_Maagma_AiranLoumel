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
    [SerializeField] private State _currentState;
    [SerializeField] private State _previousState;
    [SerializeField] private PlayerShoot _weaponHandler;
    //time
    private float _minWaitTime = 3;
    private float _maxWaitTime = 5;
    private float _waitTime;
    private float _timeTick;

    private Vector3 _destPoint = Vector3.zero;
    private float _speed = 3;

    public Transform Target { get; set; }
    private float _chaseDist = 5;
    private float _chaseSpeed = 4.0f;

    private void Start()
    {
        _currentState = State.Idle;

        _waitTime = Random.Range(_minWaitTime, _maxWaitTime);
        _timeTick = 0;

        _weaponHandler = this.GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Idle:    IdleUpdate(); break;
            case State.Move:    MoveUpdate(); break;
            case State.Chase:   ChaseUpdate(); break;
            case State.Attack:  AttackUpdate(); break;
        }
    }

    private void IdleUpdate()
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

    private void MoveUpdate()
    {
        if (Target != null)
        {
            ChangeState(State.Chase);
            return;
        }

        if (_destPoint == Vector3.zero )
        {
            _destPoint = new Vector3(
                Random.Range(-90, 90), // x
                Random.Range(-40, 40), // y
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

    private void ChaseUpdate()
    { 
        if(Target == null) 
        {
            ChangeState(_previousState);
            return;
        }

        RotateTowardsTarget();
        if(Vector2.Distance(this.transform.position, Target.position) > _chaseDist)
        {
            float step = _chaseSpeed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, Target.transform.position, step);
        }
        else 
            ChangeState(State.Attack);
    }

    private void AttackUpdate()
    {
        if(Target == null)
        {
            ChangeState(State.Idle);
            return;
        }

        RotateTowardsTarget();
        if(this.GetComponent<EnemyShoot>()._canFire)
            this.GetComponent<EnemyShoot>().Firing();
    }

    private void ChangeState(State state)
    {
        if(_currentState == state) 
            return;

        _previousState = _currentState;
        _currentState = state;
    }

    private void RotateTowardsTarget()
    {
        Vector2 direction = Target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
