using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Unit
{
    private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _movementJoystick;

    //Movement Stats
    private float _moveSpeed = 10.0f;
    private Vector3 _direction;

    protected override void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _direction.x = _movementJoystick.Horizontal * _moveSpeed;
        _direction.y = _movementJoystick.Vertical * _moveSpeed;
        Camera.main.transform.position = new Vector3(this.transform.position.x, 
                                                     this.transform.position.y, 
                                                    -10);
    }

    void FixedUpdate()
    {
        if (_direction.magnitude >= 0.1f)
            _rigidbody.MovePosition(_rigidbody.position + _direction.normalized * _moveSpeed * Time.deltaTime);
    }

}
