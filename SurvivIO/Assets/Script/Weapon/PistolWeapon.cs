using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Weapon
{
    protected override void OnTriggerEnter(Collider collider)
    {
        _weaponName = WeaponName.Pistol;
    }

}
