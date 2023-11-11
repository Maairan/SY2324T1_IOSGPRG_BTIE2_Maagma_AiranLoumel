using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float _moveSpeed = 10.0f;
    private GameObject _shooter;
    private float _damage;

    public void InitBullet(GameObject owner, float damage)
    {
        this._shooter = owner;
        this._damage = damage;
    }

    void Update()
    {
        this.transform.Translate(new Vector3(0.0f, _moveSpeed, 0.0f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Unit>() && collider.gameObject != _shooter)
        {
            collider.GetComponent<Unit>().TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}