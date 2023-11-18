using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Destructibles : MonoBehaviour
{
    private Health _health;
    private void Start()
    {
        _health = GetComponent<Health>();
        _health.InitHealth(100);
    }

    public void OnHit()
    {
        
    }
}
