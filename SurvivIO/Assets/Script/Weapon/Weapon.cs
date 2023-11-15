using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum WeaponName
{
    Pistol,
    Shotgun,
    Rifle,
    Null
}

public class Weapon : MonoBehaviour
{
    protected GameObject _owner;
    [SerializeField] protected Transform _nozzleTransform;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] private TextMeshProUGUI _ammoMagText;
    [SerializeField] private TextMeshProUGUI _ammoBagText;
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] public GameObject[] _weaponModel;
    public GameObject _activeWeapon;
    [SerializeField] public Transform _weaponTransform;

    // Weapon attributes
    public WeaponName _weaponName = WeaponName.Null;
    protected float _magazineSize; // Mag size
    protected float _currentAmmo; // Ammo in mag
    protected float _currentReservedAmmo; // Ammo in bag
    protected float _maxReservedAmmo; // Max capacity of ammo in bag
    protected float _weaponRange;
    private bool _isReloading;
    private float _reloadTime;
    private bool _isHeld;
    protected float _fireRate;
    [SerializeField] protected bool _canFire = true;
    protected int _damage;

    public virtual void Start()
    {

    }

    public void InitWeapon(WeaponName name, float ammo, float weaponRange, float reloadTime, float fireRate, int damage)
    {
        this._owner = GameObject.FindGameObjectWithTag("Player");
        this._weaponName = name;
        this._currentAmmo = ammo;
        this._magazineSize = ammo;
        this._currentReservedAmmo = 30;
        this._weaponRange = weaponRange;
        _weaponNameText.text = name.ToString(); 
        _ammoBagText.text = _currentReservedAmmo.ToString();
        _ammoMagText.text = _currentAmmo.ToString();
        _reloadTime = reloadTime;
        _fireRate = fireRate;
        _damage = damage;
    }

    private void Update()
    {
        if(_isHeld && _weaponName == WeaponName.Rifle && _canFire)
            StartCoroutine(Fire());
        
    }

    protected virtual void OnFireButtonClicked()
    {
        if(!_canFire || _isReloading)
            return;

        if (_currentAmmo <= 0 && _currentReservedAmmo <= 0)
            return;

        else
            StartCoroutine(Fire());
    }

    protected virtual IEnumerator Fire()
    {   
        _canFire = false;

        if(this._weaponName == WeaponName.Shotgun)
        {
            for(int i = 0; i < 8; i++)
            {
            GameObject bullet = Instantiate(_bulletPrefab, _nozzleTransform.position, _nozzleTransform.rotation);
            float randDir = Random.Range(-1.0f, 1.0f);
            bullet.GetComponent<BulletMovement>().InitBullet(_owner, _damage, randDir);
            Destroy(bullet.gameObject, _weaponRange);
            }
        }
        else
        {
            GameObject bullet = Instantiate(_bulletPrefab, _nozzleTransform.position, _nozzleTransform.rotation);
            bullet.GetComponent<BulletMovement>().InitBullet(_owner, _damage, 0.0f);
            Destroy(bullet.gameObject, _weaponRange);
        }

        _currentAmmo--;
        _ammoMagText.text = _currentAmmo.ToString();

        yield return new WaitForSeconds(_fireRate);

        if (_currentAmmo <= 0)
            StartCoroutine(Reload(_reloadTime));
        else
            _canFire = true;
    }

    IEnumerator Reload(float waitTime)
    {
        _isReloading = true;
        yield return new WaitForSeconds(waitTime);
        if (_currentReservedAmmo <= _magazineSize && _currentReservedAmmo > 0)
        {
            _currentAmmo = _currentReservedAmmo;
            _currentReservedAmmo = 0;
        }
        else
        {
            _currentAmmo = _magazineSize;
            _currentReservedAmmo -= _magazineSize;
        }

        _ammoBagText.text = _currentReservedAmmo.ToString();
        _ammoMagText.text = _currentAmmo.ToString();
        _isReloading = false;
        _canFire = true;
    }

    public void AddAmmo(float value)
    {
        _currentReservedAmmo += value;
       _ammoBagText.text = _currentReservedAmmo.ToString();
    }

    public void SwitchWeapon(WeaponName weapon)
    {   
        switch(weapon)
        {
            case WeaponName.Pistol:
            InitWeapon(weapon, 10, 1.5f, 2.0f, 2.16f, 10);
            break;

            case WeaponName.Rifle:
            InitWeapon(weapon, 30, 1.5f, 2.3f, 0.35f, 15);
            break;

            case WeaponName.Shotgun:
            InitWeapon(weapon, 5, 1.5f, 2.7f, 0.6f, 10);
            break;
        }
        _activeWeapon = Instantiate(_weaponModel[(int)weapon], _weaponTransform);
        StopAllCoroutines();
        _weaponNameText.text = weapon.ToString(); 
    }
    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Weapon>() && !_isReloading) // Can only switch weapons when not reloading. 
        {
            if (collider.GetComponent<Weapon>()._weaponName == this._weaponName)
                AddAmmo(10);
            else
            {
                if(_activeWeapon != null)
                    Destroy(_activeWeapon);
                SwitchWeapon(collider.GetComponent<Weapon>()._weaponName);
            }
            Destroy(collider.gameObject);
        }
    }

    public void OnFireButtonHeld(bool value)
    {
        _isHeld = value;
    }

    public void OnSwitchWeaponButtonPressed()
    {
        // TBI
    }
}
