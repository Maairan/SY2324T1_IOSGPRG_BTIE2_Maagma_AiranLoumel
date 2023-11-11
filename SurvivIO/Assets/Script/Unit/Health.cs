using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Health : MonoBehaviour
{
    public float _currentHealth { get; private set;}
    public float _maxHealth { get; private set;}
    public bool isAlive { get { return _currentHealth > 0;} }
    public void InitHealth(float health)
    {
        _maxHealth = health;
        _currentHealth = _maxHealth;
    }

    public void UpdateHealth(float value, Action<bool> onCallback = null)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            onCallback?.Invoke(true);
        }

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
