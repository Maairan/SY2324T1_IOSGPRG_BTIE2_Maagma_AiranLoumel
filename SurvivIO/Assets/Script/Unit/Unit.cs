using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    protected Health health;
    public Action<GameObject> Died;

    protected virtual void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        if (!health.isAlive)
            return;
         
        health.UpdateHealth(damage, (bool isDying)=>
        {
            if(isDying)
            {
                OnDied();
            }
        });
    }

    public void OnDied()
    {
        Died?.Invoke(this.gameObject);
    }
}
