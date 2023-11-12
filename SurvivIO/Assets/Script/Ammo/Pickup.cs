using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Weapon))]

public class Pickup : MonoBehaviour
{
    protected int _value;
    protected string _ammoType;
    protected WeaponName _weaponName;

    public virtual void Start()
    {
        
    }

    public virtual void OnPickUp()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Weapon>()._weaponName == _weaponName)
            player.GetComponent<Weapon>().AddAmmo(_value);
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<PlayerMovement>())
        {
            OnPickUp();
            Destroy(this.gameObject);
        }
    }
}
