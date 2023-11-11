using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Unit
{
    private Rigidbody _rigidbody;

    //Movement Stats
    private float _moveSpeed = 10.0f;
    private Vector3 _direction;

    protected override void Start()
    {
        base.Start();
        Debug.Log("hello player");
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction.normalized * _moveSpeed * Time.deltaTime);
    }

}
