using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmo : Pickup
{
    public override void Start()
    {
       _value = 30;
       _weaponName = WeaponName.Rifle;
       _ammoType = "5.56m";
    }
}
