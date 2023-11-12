using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : Pickup
{
    public override void Start()
    {
        _value = 5;
        _ammoType = "12gauge";
        _weaponName = WeaponName.Shotgun;
    }
}
