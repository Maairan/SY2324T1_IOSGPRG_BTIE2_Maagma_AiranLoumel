using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Weapon))]

public class Pickup : MonoBehaviour
{
    public virtual void OnPickUp()
    {
        //To be implemented in child
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<Unit>())
        {
            OnPickUp();
            Destroy(this.gameObject);
        }
    }
}
