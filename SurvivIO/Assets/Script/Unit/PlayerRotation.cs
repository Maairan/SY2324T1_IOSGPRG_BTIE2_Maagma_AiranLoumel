using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{ 
    [SerializeField] FixedJoystick _shootingJoystick;

    void Update()
    {   
        float angle = Mathf.Atan2(_shootingJoystick.Horizontal, _shootingJoystick.Vertical) * Mathf.Rad2Deg;

        if (angle >= 0.1f || angle <= -0.1f)
            transform.eulerAngles = new Vector3(0f, 0f, -angle);
    }
}
