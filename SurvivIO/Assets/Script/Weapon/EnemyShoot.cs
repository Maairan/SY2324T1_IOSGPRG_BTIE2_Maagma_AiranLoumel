using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponModels;
    [SerializeField] private WeaponBase[] _weapons;
    [SerializeField] private WeaponBase _equippedWeapon;
    public bool _canFire, _isReloading;
    [SerializeField] private GameObject[] _bulletPrefab;
    [SerializeField] private Transform _nozzle, _weapon;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private GameObject _muzzleFlash; 

    private void Start()
    {
        int randomWeapon = Random.Range(0, _weaponModels.Length);
        GameObject activeWeapon = Instantiate(_weaponModels[randomWeapon], _weapon);
        _equippedWeapon = _weapons[randomWeapon];
        _currentAmmo = _equippedWeapon._currentAmmo;
    }

    public void Firing()
    {
        if(_currentAmmo > 0)
            StartCoroutine(Fire());
        else
            if(!_isReloading)
                StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_equippedWeapon._reloadTime * 2);
        _isReloading = false;

        _currentAmmo = _equippedWeapon._magazineSize;
    }

    private IEnumerator Fire()
    {
        if (_equippedWeapon == _weapons[1])
        for (int i = 0; i < 8; i++)
        {
            
            float randomSpread = Random.Range(-2,2);
            SpawnBullet(randomSpread);
        }
        else
            SpawnBullet(0);

        _currentAmmo--;
        if(_currentAmmo <= 0)
            StartCoroutine(Reload());

        _canFire = false;
        yield return new WaitForSeconds(_equippedWeapon._fireRate);
        _canFire = true;
    }

    private void SpawnBullet(float spread)
    {
        GameObject bullet = Instantiate(_bulletPrefab[_equippedWeapon._index], _nozzle.position, _nozzle.rotation);
        SpawnMuzzleFlash();
        bullet.GetComponent<BulletMovement>().InitBullet(this.gameObject, _equippedWeapon._damage, spread);
        Destroy(bullet, _equippedWeapon._range);
    }

    private void SpawnMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(_muzzleFlash, _nozzle.position, _nozzle.rotation);
        Destroy(muzzleFlash, 0.1f);
    }

}
