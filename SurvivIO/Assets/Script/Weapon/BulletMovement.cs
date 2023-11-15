using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float _moveSpeed = 10.0f;
    private GameObject _shooter;
    private float _damage;
    private float _bulletSpread;

    public void InitBullet(GameObject owner, float damage, float bulletSpread)
    {
        this._shooter = owner;
        this._damage = damage;
        this._bulletSpread = bulletSpread;
    }

    void Update()
    {
        this.transform.Translate(new Vector3(_bulletSpread, _moveSpeed, 0.0f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Unit>() && collider.gameObject != _shooter)
        {
            collider.GetComponent<Unit>().TakeDamage(_damage);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
}