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

    void Start()
    {
        
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
        Debug.Log("Idle");
    }

    void MoveUpdate()
    {
        Debug.Log("Move");
    }

    void ChaseUpdate()
    {
        Debug.Log("Chase");
    }

    void AttackUpdate()
    {
        Debug.Log("Attack");
    }

}
