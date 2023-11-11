using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : Pickup
{
    public override void OnPickUp()
    {
        // Add pistol ammo 
        Debug.Log("Picked up pistol ammo");
    }
}
