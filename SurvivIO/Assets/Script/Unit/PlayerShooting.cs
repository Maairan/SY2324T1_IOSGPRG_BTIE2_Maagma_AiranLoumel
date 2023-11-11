using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Weapon
{ 
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        InitWeapon("Pistol", 10, 1.5f, this.gameObject);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Fire();
        
        Vector2 lookDirection = _cam.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90.0f;
        this.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
