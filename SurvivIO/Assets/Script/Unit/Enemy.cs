using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected override void Start()
    {
        base.Start();
        
        WeaponShoot _weaponHandler = this.GetComponent<WeaponShoot>();
        int randomWeapon = Random.Range(0, _weaponHandler._weapons.Length);
        this.GetComponent<WeaponShoot>()._activeWeapon = Instantiate(_weaponHandler._weaponPrefabs[randomWeapon], _weaponHandler._weaponSlot);
        _weaponHandler._equippedWeapon = _weaponHandler._weapons[randomWeapon];
    }
}
