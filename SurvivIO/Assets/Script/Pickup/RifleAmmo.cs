using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmo : Pickup
{
    public override void OnPickUp()
    {
        // Add rifle ammo
        Debug.Log("Picked up rifle ammo");
    }
}
