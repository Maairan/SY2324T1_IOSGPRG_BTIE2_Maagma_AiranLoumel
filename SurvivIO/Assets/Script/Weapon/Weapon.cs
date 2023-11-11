using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    private string _name;
    private GameObject _owner;
    private float _ammo;
    private float _weaponRange;
    [SerializeField] private Transform _nozzle;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    
    public void InitWeapon(string name, float ammo, float weaponRange, GameObject owner)
    {
        this._owner = owner;
        this._ammo = ammo;
        this._name = name;
        this._weaponRange = 100;
    }

    public void Fire()
    {
        if(_ammo <= 0)
            return;

        GameObject bullet = Instantiate(_bulletPrefab, _nozzle.position, _nozzle.rotation);
        bullet.GetComponent<BulletMovement>().InitBullet(_owner, 10);
        Destroy(bullet.gameObject, _weaponRange);
        _ammo--;
        _ammoCountText.text = "Ammo: " + _ammo;
    }

    public void Reload()
    {
        //TBI
    }
}
