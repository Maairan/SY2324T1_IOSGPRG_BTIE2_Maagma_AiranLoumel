using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : Weapon
{
    protected override void OnTriggerEnter(Collider collider)
    {
        _weaponName = WeaponName.Rifle;
    }

}
