using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : Pickup
{

    public override void Start()
    {
        _value = 10;
        _weaponName = WeaponName.Pistol;
        _ammoType = "9mm";
    }
}
