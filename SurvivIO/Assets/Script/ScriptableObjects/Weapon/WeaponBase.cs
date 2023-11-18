using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponBase : ScriptableObject
{
    public GameObject _prefab;
    public float _damage;
    public string _name;
    public float _fireRate;
    public float _reloadTime;
    public string _type; // primary or secondary
    public string _ammoType;
    public int _currentAmmo;
    public int _reservedAmmo;
    public int _magazineSize;
    public int _maxCapacity;
    public int _index;
    public float _range;
}
