using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] private AmmoBase _ammo;
    public string _name;

    private void Start()
    {
        _name = _ammo._name;   
    }
}
