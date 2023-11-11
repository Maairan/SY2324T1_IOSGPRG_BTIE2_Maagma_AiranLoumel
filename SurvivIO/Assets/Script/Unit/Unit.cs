using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Health health;

    protected virtual void Start()
    {
        Debug.Log("hello world");
    }

    
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        // if (!health.isAlive)
        //     return;
         
        // health.UpdateHealth(damage, (bool isDying)=>
        // {
        //     if(isDying)
        //     {
        //         OnDied();
        //     }
        // });
    }

    public void OnDied()
    {

    }
}
