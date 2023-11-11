using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : Pickup
{
    public override void OnPickUp()
    {
        // Add shotgun ammo   
        Debug.Log("Picked up shotgun ammo");
    }
}
