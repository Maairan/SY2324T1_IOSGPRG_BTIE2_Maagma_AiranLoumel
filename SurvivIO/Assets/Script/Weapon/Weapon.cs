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
    private GameObject _owner;
    [SerializeField] private Transform _nozzle;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private TextMeshProUGUI _ammoMagText;
    [SerializeField] private TextMeshProUGUI _ammoBagText;
    [SerializeField] private TextMeshProUGUI _weaponNameText;

    // Weapon attributes
    public WeaponName _weaponName = WeaponName.Null;
    protected float _magazineSize; // Mag size
    protected float _currentAmmo; // Ammo in mag
    protected float _reservedAmmo; // Ammo in bag
    protected float _weaponRange;
    private bool isReloading;
    private float reloadTime;
    
    public virtual void Start()
    {

    }

    public void InitWeapon(WeaponName name, float ammo, float weaponRange)
    {
        this._owner = GameObject.FindGameObjectWithTag("Player");
        this._weaponName = name;
        this._currentAmmo = ammo;
        this._magazineSize = ammo;
        this._reservedAmmo = 30;
        this._weaponRange = weaponRange;
        _weaponNameText.text = name.ToString(); 
        _ammoBagText.text = _reservedAmmo.ToString();
        _ammoMagText.text = _currentAmmo.ToString();
    }

    public void Fire()
    {
        if (isReloading) 
            return;

        if (_currentAmmo <= 0)
        {
            if (_reservedAmmo <= 0)
                return;
            else   
                StartCoroutine(Reload(2));
        }
        else
        {
            GameObject bullet = Instantiate(_bulletPrefab, _nozzle.position, _nozzle.rotation);
            bullet.GetComponent<BulletMovement>().InitBullet(_owner, 10);
            Destroy(bullet.gameObject, _weaponRange);
            _currentAmmo--;
            _ammoMagText.text = _currentAmmo.ToString();
        }
    }

    IEnumerator Reload(int waitTime)
    {
        isReloading = true;
        yield return new WaitForSeconds(waitTime);
        if (_reservedAmmo <= _magazineSize && _reservedAmmo > 0)
        {
            _currentAmmo = _reservedAmmo;
            _reservedAmmo = 0;
        }
        else
        {
            _currentAmmo = _magazineSize;
            _reservedAmmo -= _magazineSize;
        }

        _ammoBagText.text = _reservedAmmo.ToString();
        _ammoMagText.text = _currentAmmo.ToString();
        isReloading = false;
    }

    public void AddAmmo(float value)
    {
        _reservedAmmo += value;
       _ammoBagText.text = _reservedAmmo.ToString();
    }

    public void SwitchWeapon(WeaponName weapon)
    {   
        switch(weapon)
        {
            case WeaponName.Pistol:
            InitWeapon(weapon, 10, 1.5f);
            break;

            case WeaponName.Rifle:
            InitWeapon(weapon, 30, 1.5f);

            break;

            case WeaponName.Shotgun:
            InitWeapon(weapon, 5, 1.5f);
            
            break;
        }
        _weaponNameText.text = weapon.ToString(); 
    }
    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Weapon>()) 
        {
            if (collider.GetComponent<Weapon>()._weaponName == this._weaponName)
                AddAmmo(10);
            else
                SwitchWeapon(collider.GetComponent<Weapon>()._weaponName);

            Destroy(collider.gameObject);
        }
    }
}
