using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    protected override void OnTriggerEnter(Collider collider)
    {
        _weaponName = WeaponName.Shotgun;
    }
}
