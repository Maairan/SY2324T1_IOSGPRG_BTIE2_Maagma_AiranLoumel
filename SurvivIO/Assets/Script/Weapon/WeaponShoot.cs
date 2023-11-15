using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] public WeaponBase[] _weapons;//_pistol, _shotgun, _rifle;
    [SerializeField] public WeaponBase _primaryWeapon, _secondaryWeapon, _equippedWeapon;
    [SerializeField] private AmmoBase[] _ammo;
    public bool _canFire = true, _isHeld = false, _isReloading;
    [SerializeField] public Transform _nozzle, _weaponSlot;
    [SerializeField] public GameObject _bulletPrefab;
    [SerializeField] public GameObject[] _weaponPrefabs;
    [SerializeField] public GameObject _activeWeapon;
    [SerializeField] TextMeshProUGUI _weaponName, _magazineAmmo, _reserved9mm, _reserved556mm, _reserved12gauge;

    private void Start()
    {
        for(int i = 0; i < _weapons.Length; i++)
            ResetStats(_weapons[i]);
        UpdateTexts();
    }

    private void Update()
    {
        if(_isHeld && _canFire && _equippedWeapon == _weapons[2] && !_isReloading)
        {
            if(_equippedWeapon._currentAmmo >= 0)
                StartCoroutine(Fire());
            else 
                StartCoroutine(Reload());
        }
    }

    private void ResetStats(WeaponBase weapon)
    {
        weapon._currentAmmo = weapon._magazineSize;
        weapon._reservedAmmo = weapon._maxCapacity / 2;
    }

    private void AddAmmo(WeaponBase weapon)
    {
        switch(weapon._ammoType)
        {
            case "9mm":
            weapon._reservedAmmo += Random.Range(_ammo[0]._minReplenish, _ammo[0]._maxReplenish);
            break;

            case "556mm":
            weapon._reservedAmmo += Random.Range(_ammo[2]._minReplenish, _ammo[2]._maxReplenish);
            break;

            case "12gauge":
            weapon._reservedAmmo += Random.Range(_ammo[1]._minReplenish, _ammo[1]._maxReplenish);
            break;
        }

        if(weapon._reservedAmmo > weapon._maxCapacity)
            weapon._reservedAmmo = weapon._maxCapacity;
        UpdateTexts();
    }

    private void ChangeWeapon(WeaponBase newWeapon)
    {
        if(newWeapon._type == "Primary")
            _primaryWeapon = newWeapon;
        else
            _secondaryWeapon = newWeapon;
        
        _equippedWeapon = newWeapon;
        _equippedWeapon._currentAmmo = newWeapon._magazineSize;
        UpdateTexts();
    }

    private void SpawnWeapon(int weapon)
    {
        if(_activeWeapon != null)
            Destroy(_activeWeapon);
        _activeWeapon = Instantiate(_weaponPrefabs[weapon], _weaponSlot);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag != "Object")
            return;
        
        switch(collider.name)
        {
            case "Pistol(Clone)":
            ChangeWeapon(_weapons[0]);
            SpawnWeapon(0);
            break;

            case "Shotgun(Clone)":
            ChangeWeapon(_weapons[1]);
            SpawnWeapon(1);
            break;

            case "Rifle(Clone)":
            ChangeWeapon(_weapons[2]);
            SpawnWeapon(2);
            break;

            case "PistolAmmo(Clone)":
            AddAmmo(_weapons[0]);
            break;

            case "ShotgunAmmo(Clone)":
            AddAmmo(_weapons[1]);
            break;

            case "RifleAmmo(Clone)":
            AddAmmo(_weapons[2]);
            break;
        }

        Destroy(collider.gameObject);
    }

    public void OnSwapButtonPressed()
    {
        if (_primaryWeapon == null || _secondaryWeapon == null || _isReloading)
            return;

        if(_equippedWeapon != _primaryWeapon)
        {
            _equippedWeapon = _primaryWeapon;
            if(_equippedWeapon._name == "Shotgun")
                SpawnWeapon(1);
            else
                SpawnWeapon(2);
        }
        else
        {
            SpawnWeapon(0);
            _equippedWeapon = _secondaryWeapon;
        }

        UpdateTexts();
    }

    public void OnFireButtonPressed()
    {
        if(!_canFire || _isReloading || _equippedWeapon == null)
            return;
        StartCoroutine(Fire());
    }

    public void OnFireButtonHeld(bool value)
    {
        _isHeld = value;
    }

    public IEnumerator Reload()
    {
        Debug.Log("Reloading");
        _isReloading = true;
        yield return new WaitForSeconds(_equippedWeapon._reloadTime);
        _isReloading = false;

        _equippedWeapon._currentAmmo = _equippedWeapon._magazineSize;
        _equippedWeapon._reservedAmmo -= _equippedWeapon._magazineSize;

        UpdateTexts();
    }

    public IEnumerator Fire()
    {
        if (_equippedWeapon == _weapons[1])
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _nozzle.position, _nozzle.rotation);
            int randomSpread = Random.Range(-2,2);
            bullet.GetComponent<BulletMovement>().InitBullet(this.gameObject, _equippedWeapon._damage, randomSpread);
        }
        else
        {
            GameObject bullet = Instantiate(_bulletPrefab, _nozzle.position, _nozzle.rotation);
            bullet.GetComponent<BulletMovement>().InitBullet(this.gameObject, _equippedWeapon._damage, 0);
        }

        _equippedWeapon._currentAmmo--;
        if(_equippedWeapon._currentAmmo <= 0)
            StartCoroutine(Reload());

        UpdateTexts();

        _canFire = false;
        yield return new WaitForSeconds(_equippedWeapon._fireRate);
        _canFire = true;
        
    }

    private void UpdateTexts()
    {
        if(_equippedWeapon != null)
        {
            _weaponName.text = _equippedWeapon.name;
            _magazineAmmo.text = _equippedWeapon._currentAmmo.ToString();
        }
        if (_reserved9mm == null || _reserved556mm == null || _reserved12gauge == null)
            return;
         
        _reserved9mm.text = _weapons[0]._reservedAmmo.ToString();
        _reserved12gauge.text = _weapons[1]._reservedAmmo.ToString();
        _reserved556mm.text = _weapons[2]._reservedAmmo.ToString();
    }
}
