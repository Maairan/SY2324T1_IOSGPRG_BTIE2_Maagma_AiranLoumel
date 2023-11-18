using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponBase _weapon;
    public string _name;
    
    private void Start()
    {
        _name = _weapon._name;
    }
}
