using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Weapon
{ 
    private Camera _cam;
    [SerializeField] FixedJoystick _shootingJoystick;
    
    void Start()
    {
        _cam = Camera.main;
        InitWeapon("Pistol", 10, 1.5f, this.gameObject);
    }

    void Update()
    {   
        float angle = Mathf.Atan2(_shootingJoystick.Horizontal, _shootingJoystick.Vertical) * Mathf.Rad2Deg;

        if (angle >= 0.1f || angle <= -0.1f)
            transform.eulerAngles = new Vector3(0f, 0f, -angle);
    }
}
