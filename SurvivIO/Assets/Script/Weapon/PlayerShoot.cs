using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(500)]
public class PlayerShoot : MonoBehaviour
{
    public WeaponBase[] _weapons; //pistol, shotgun, rifle, null;
    public WeaponBase _primaryWeapon, _secondaryWeapon, _equippedWeapon;
    [SerializeField] private AmmoBase[] _ammo;
    public bool _canFire = true, _isHeld = false, _isReloading;
    [SerializeField] private Transform _nozzle, _weaponSlot;
    [SerializeField] private GameObject[] _bulletPrefab;
    [SerializeField] private GameObject[] _weaponPrefabs;
    [SerializeField] private GameObject _activeWeapon, _muzzleFlash;
    public int _kills;

    private void Start()
    {
        for(int i = 0; i < _weapons.Length; i++)
            ResetStats(_weapons[i]);
        UIHandler.instance.UpdateBagText();
    }

    private void Update()
    {
        if (!_isHeld)
            return;

        if (_equippedWeapon == _weapons[2])
            OnFireButtonPressed();
    }

    private void ResetStats(WeaponBase weapon)
    {
        weapon._currentAmmo = weapon._magazineSize;
        weapon._reservedAmmo = weapon._maxCapacity / 2;
    }

    private void AddAmmo(WeaponBase weapon, int value)
    {
        weapon._reservedAmmo += Random.Range(_ammo[value]._minReplenish, _ammo[value]._maxReplenish);

        if(weapon._reservedAmmo > weapon._maxCapacity)
                weapon._reservedAmmo = weapon._maxCapacity;
        
        UIHandler.instance.UpdateBagText();
    }

    private void ChangeWeapon(WeaponBase newWeapon)
    {
        if(newWeapon._type == "Primary")
            _primaryWeapon = newWeapon;
        else if (newWeapon._type == "Secondary")
            _secondaryWeapon = newWeapon;
        
        _equippedWeapon = newWeapon;
        _equippedWeapon._currentAmmo = newWeapon._magazineSize;

        UIHandler.instance.UpdateWeaponText();
        UIHandler.instance.UpdateBagText();
    }

    private void SpawnWeapon(int weapon)
    {
        if(_activeWeapon != null)
            Destroy(_activeWeapon);
        _activeWeapon = Instantiate(_weaponPrefabs[weapon], _weaponSlot);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(!collider.GetComponent<WeaponHandler>() && !collider.GetComponent<AmmoHandler>() && !collider.CompareTag("Medkit"))
            return;
        
        if(collider.GetComponent<WeaponHandler>())
        {
            switch(collider.GetComponent<WeaponHandler>()._name)
            {
                case "Pistol":
                ChangeWeapon(_weapons[0]);
                SpawnWeapon(0);
                break;

                case "Shotgun":
                ChangeWeapon(_weapons[1]);
                SpawnWeapon(1);
                break;

                case "Rifle":
                ChangeWeapon(_weapons[2]);
                SpawnWeapon(2);
                break;
            }
        }
        else if (collider.GetComponent<AmmoHandler>())
        {
            switch(collider.GetComponent<AmmoHandler>()._name)
            {
                case "9mm":
                AddAmmo(_weapons[0], 1);
                break;

                case "12gauge":
                AddAmmo(_weapons[1], 1);
                break;

                case "556mm":
                AddAmmo(_weapons[2], 1);
                break;  
            }
        }
        else
        {
            GetComponent<Health>().AddMedkit();
            UIHandler.instance.UpdateMedkitText();
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

        UIHandler.instance.UpdateWeaponText();
        UIHandler.instance.UpdateBagText();
    }

    public void OnFireButtonPressed()
    {
        if(!_canFire || _isReloading || _equippedWeapon == null || (_equippedWeapon._reservedAmmo <= 0 && _equippedWeapon._currentAmmo <= 0))
            return;
        StartCoroutine(Fire());

        if(this.GetComponent<Health>()._isHealing)
            this.GetComponent<Health>().StopHealing();
    }

    public void OnFireButtonHeld(bool value)
    {
        _isHeld = value;
    }

    public IEnumerator Reload()
    {
        _isReloading = true;
        UIHandler.instance._magazineAmmo.text = "Reloading...";
        yield return new WaitForSeconds(_equippedWeapon._reloadTime);
        UIHandler.instance._reloadingText.text = "";
        _isReloading = false;

        if (_equippedWeapon._reservedAmmo >= _equippedWeapon._magazineSize)
        {
            _equippedWeapon._currentAmmo = _equippedWeapon._magazineSize;
            _equippedWeapon._reservedAmmo -= _equippedWeapon._magazineSize;
        }
        else 
        {
            _equippedWeapon._currentAmmo = _equippedWeapon._reservedAmmo;
            _equippedWeapon._reservedAmmo = 0;
        }

        UIHandler.instance.UpdateWeaponText();
        UIHandler.instance.UpdateBagText();
    }

    public IEnumerator Fire()
    {
        if (_equippedWeapon == _weapons[1])
        for (int i = 0; i < 8; i++)
        {
            float randomSpread = Random.Range(-2,2);
            SpawnBullet(randomSpread);
        }
        else
            SpawnBullet(0);
        
        _equippedWeapon._currentAmmo--;
        if(_equippedWeapon._currentAmmo <= 0)
            StartCoroutine(Reload());

        UIHandler.instance.UpdateWeaponText();
        UIHandler.instance.UpdateBagText();

        _canFire = false;
        yield return new WaitForSeconds(_equippedWeapon._fireRate);
        _canFire = true;
    }

    public void SpawnBullet(float spread)
    {
        GameObject bullet = Instantiate(_bulletPrefab[_equippedWeapon._index], _nozzle.position, _nozzle.rotation);
        SpawnMuzzleFlash();
        bullet.GetComponent<BulletMovement>().InitBullet(this.gameObject, _equippedWeapon._damage, spread);
        Destroy(bullet, _equippedWeapon._range);
    }

    public void SpawnMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(_muzzleFlash, _nozzle.position, _nozzle.rotation);
        Destroy(muzzleFlash, 0.1f);
    }

}
